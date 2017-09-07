using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BudgetThis2017.Models;
using BudgetThis2017.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BudgetThis2017.Proxy;

namespace BudgetThis2017.Controllers.api
{
  public class CalController : Controller
  {

    private UserManager<BudgetUser> _userManager;
    private IBudgetThisRepository _repository;
    private ILogger<BillsController> _logger;
    private const string _urlBase = "http://localhost:2247/";

    public CalController(IBudgetThisRepository repository, ILogger<BillsController> logger, UserManager<BudgetUser> userManager)
    {
      _repository = repository;
      _logger = logger;
      _userManager = userManager;
    }

    [HttpGet("api/home")]
    public IActionResult Get()
    {
      try
      {
        PBProxy pb = new PBProxy();
        Guid userGuid = new Guid(_userManager.GetUserId(HttpContext.User));
        //get Settings
        var settings = _repository.GetSettingsByUser(userGuid);

        //get list of all bills
        var bills = _repository.GetBillsByUser(userGuid);
        var AllBills = _repository.GetListBillsViewModelByUser(userGuid);

        //get payday seed
        var paydayseed =_repository.GetPaydaySeedByUser(userGuid);
        SetSeed(ref paydayseed, settings.PreviousPeriodsToShow);

        //get paycheck list
        var payChecks = GetPaydays(paydayseed.Paydate);

        //get firstday based on payday seed
        var firstDay = FindFirstDay(paydayseed);

        //prepare for foreach day
        var currentMonth = -1;
        var currentCheckIndex = 0;
        var currentMonthIndex = -1;
        string[] monthColors = { "C4FFEA", "D1D7FF", "FFCCF9", "FFF6D8" };
        var CurrentMonth = 0;
        var daysShowing = (settings.ChecksToShow + 1) * 14;
        var showData = false;

        PaycheckSummaryViewModel summary = null;
        var paycheckdetails = new PaycheckDetailsViewModel();
        var home = new HomePageViewModel();






        //foreach day
        for (var dayIndex = 0; dayIndex < daysShowing; dayIndex++)
        {
          #region For Each Day In Pay Period
          var tempdate = firstDay.AddDays(dayIndex);
          if (tempdate.Month != currentMonth)
          {
            currentMonth = tempdate.Month;
            currentMonthIndex++;
          }
          var day = new DayViewModel { Date = tempdate, DayOfWeek = (int)tempdate.DayOfWeek, Index = dayIndex, MonthColor = monthColors[currentMonthIndex] };
          var paycheckType = tempdate.Day <= 14 ? "A" : "B";

          if (payChecks.Any(a => a == tempdate))
          {
           home.DayIndexOfCurrentPeriod = tempdate > DateTime.Now ? home.DayIndexOfCurrentPeriod : dayIndex;
            var inLastPaycheck = (daysShowing - dayIndex) < 13;
            showData = !inLastPaycheck;
            if (currentCheckIndex > 0)
            {
              paycheckdetails = new PaycheckDetailsViewModel { Summary = summary };
              home.Days.FirstOrDefault(a => a.Index == currentCheckIndex).Paycheck = paycheckdetails;
              summary = new PaycheckSummaryViewModel();
            }
            currentCheckIndex = dayIndex;
            summary = new PaycheckSummaryViewModel
            {
              Payday = tempdate,
              Credits = _repository.GetPaycheck(userGuid, paycheckType).Amount 
            };

            #region Tithe

            if (showData && settings.AddTithe)
            {
              var tithe = new BillViewModel
              {
                Name = "",
                Icon = "fa-heart-o",
                Amount = summary.Credits * settings.TitheMultiplier,
                BackgroundColor = settings.TitheBGColor,
                ForeColor = settings.TitheForeColor,
                DueDay = tempdate.Day
              };
              day.Bills.Add(tithe);

              #endregion
            }
          }
          day.PaydayIndex = currentCheckIndex;
          //day.Bills.Add(new BillViewModel() { Amount = 0, Name = dayIndex.ToString()});

          var WillMonthChange = false;
          if (CurrentMonth == 0 | tempdate.Day == 31 | tempdate.Day < 28)
            WillMonthChange = false;
          else
            WillMonthChange = tempdate.AddDays(1).Month != CurrentMonth;
          CurrentMonth = tempdate.Month;

          if (showData)
          {
            if (tempdate.ToShortDateString() == DateTime.Now.ToShortDateString())
            {
              if (summary != null) summary.CurrentClass = "list-group-item-info";
            }
            if (AllBills.Bills.Any(b => b.DueDay == tempdate.Day))
            {
              var todaysbills = AllBills.Bills.Where(b => b.DueDay == tempdate.Day).ToList();
              foreach (var todaysbill in todaysbills)
              {
                var CustomBills = _repository.GetCustomBills(todaysbill.Id);
                var thiscustombill =
                  CustomBills.FirstOrDefault(
                    cb => cb.BillDate.Year == tempdate.Year && cb.BillDate.Month == tempdate.Month);

                if (thiscustombill != null)
                {
                  var billcopy = new BillViewModel(todaysbill);
                  billcopy.Amount = thiscustombill.Amount;
                  day.Bills.Add(billcopy);
                  if (summary != null) summary.Debits += billcopy.Amount;
                }
                else
                {
                  day.Bills.Add(todaysbill);
                  if (summary != null) summary.Debits += todaysbill.Amount;
                }
              }
            }

            #region Month Changed

            if (WillMonthChange)
            {
              for (var d = tempdate.Day + 1; d < 32; d++)
              {
                if (AllBills.Bills.Any(b => b.DueDay == d))
                {
                  var todaysbills = AllBills.Bills.Where(b => b.DueDay == d).ToList();
                  foreach (BillViewModel todaysbill in todaysbills)
                  {
                    var CustomBills = _repository.GetCustomBills(todaysbill.Id);
                    var thiscustombill = CustomBills.FirstOrDefault(cb => cb.BillDate.Month == CurrentMonth);

                    if (thiscustombill != null)
                    {
                      var billcopy = new BillViewModel(todaysbill);
                      billcopy.Amount = thiscustombill.Amount;
                      day.Bills.Add(billcopy);
                    }
                    else
                    {
                      day.Bills.Add(todaysbill);
                    }
                  }
                }

              }
            }

            #endregion
          }
          home.Days.Add(day);
          #endregion
        }



















        return Ok(Mapper.Map<HomePageViewModel>(home));
      }
      catch (Exception ex)
      {
        _logger.LogError("Error: " + ex.Message);
        return BadRequest("Error happened: " + ex.Message);
      }
    }

    internal void SetSeed(ref PayDay seed, int PreviousPeriodsToShow)
    {
      DateTime today = DateTime.Now;
      bool FoundCurrent = false;
      while (!FoundCurrent)
      {
        //this will find the current pay period
        seed.Paydate = seed.Paydate.AddDays(14);
        FoundCurrent = (today > seed.Paydate && today < seed.Paydate.AddDays(14));
      }
      seed.Paydate = seed.Paydate.AddDays((PreviousPeriodsToShow * 14) * -1);
    }
    private List<DateTime> GetPaydays(DateTime seed)
    {
      var list = new List<DateTime>();

      for (var i = 0; i < (14 * 10); i = i + 14)
      {
        list.Add(seed.AddDays(i));
      }

      return list;
    }

    private DateTime FindFirstDay(PayDay payDaySeed)
    {
      var dowIndex = (int)payDaySeed.Paydate.DayOfWeek;

      return payDaySeed.Paydate.AddDays(0 - dowIndex);
    }
  }
}
