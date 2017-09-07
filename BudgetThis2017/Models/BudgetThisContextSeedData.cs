using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BudgetThis2017.Models
{
  public class BudgetThisContextSeedData
  {
    private BudgetThisContext _context;
    private UserManager<BudgetUser> _userManager;
    public BudgetThisContextSeedData(BudgetThisContext context, UserManager<BudgetUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public async Task EnsuresSeedData()
    {
      Guid newuserid;
      if (await _userManager.FindByEmailAsync("demo1@demo.com") == null)
      {
        var user = new BudgetUser()
        {
          UserName = "demo1",
          Email = "demo1@demo.com",
          Name = "Demo",
          EmailConfirmed = true
        };
        var a = await _userManager.CreateAsync(user, "1Demo!");
        newuserid = new Guid(user.Id);


        _context.Settings.Add(new Setting()
        {
          UserID = newuserid,
          AddTithe = true,
          TitheMultiplier = .1291m,
          TitheBGColor = "#000000",
          TitheForeColor = "#ffffff",
          ChecksToShow = 4,
          DaysInPeriod = 14,
          PreviousPeriodsToShow = 1,
          WeekStartDay = 0
        });
        _context.Paydays.Add(new PayDay()
        {
          UserId = newuserid,
          Paydate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        });
        _context.Paychecks.Add(new Paycheck() { UserId = newuserid, Amount = 1857.14m, Type = "A" });
        _context.Paychecks.Add(new Paycheck() { UserId = newuserid, Amount = 1897.14m, Type = "B" });



        //CREATE SOME BILLS
        //NOTE: If you alter these make sure you update the custom bills below.
        var bills = new List<Bill>
      {
        new Bill
        {
          UserId = newuserid,
          Name = "Mortgage/Rent",
          Amount = 1542.24m,
          DueDay = 1,
          ForeColor = "#ff00ff",
          BackgroundColor = "#000000",
          Icon = "fa-asterisk"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Cell Phone",
          Amount = 75.57m,
          DueDay = 3,
          ForeColor = "#cccccc",
          BackgroundColor = "#ff00ff",
          Icon = "fa-asterisk"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Cable/Internet",
          Amount = 110.33m,
          DueDay = 7,
          ForeColor = "#bababa",
          BackgroundColor = "#00ff00",
          Icon = "fa-asterisk"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Car Insurance",
          Amount = 99.94m,
          DueDay = 11,
          ForeColor = "#ff0000",
          BackgroundColor = "#000000",
          Icon = "fa-asterisk"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Electricity",
          Amount = 150.15m,
          DueDay = 16,
          ForeColor = "#0000ff",
          BackgroundColor = "#bababa",
          Icon = "fa-asterisk"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Water/Sewer",
          Amount = 40.44m,
          DueDay = 19,
          ForeColor = "#00ff00",
          BackgroundColor = "#000000",
          Icon = "fa-asterisk"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Car Payment",
          Amount = 346.01m,
          DueDay = 22,
          ForeColor = "#eeeeee",
          BackgroundColor = "#cacaca",
          Icon = "fa-asterisk"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "Visa Credit Card",
          Amount = 45.00m,
          DueDay = 28,
          ForeColor = "#cecece",
          BackgroundColor = "#000000",
          Icon = "fa-asterisk"
        },
        new Bill
        {
          UserId = newuserid,
          Name = "31st Bill",
          Amount = 90.31m,
          DueDay = 31,
          ForeColor = "#000000",
          BackgroundColor = "#00FFFF",
          Icon = "fa-asterisk"
        }
      };
        bills.ForEach(s => _context.Bills.Add(s));

        //CREATE A FEW CUSTOM BILLS
        var cbills = new List<CustomBill>
      {
        new CustomBill()
        {
          BillId = 2,
          Amount = 95.55m,
          BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        },
        new CustomBill()
        {
          BillId = 2,
          Amount = 79.38m,
          BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1)
        },
        new CustomBill() {BillId = 3, Amount = 125m, BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)},
        new CustomBill() {BillId = 7, Amount = 400m, BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)},
        new CustomBill()
        {
          BillId = 7,
          Amount = 450m,
          BillDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1)
        }
      };
        cbills.ForEach(s => _context.CustomBills.Add(s));

      }


      await _context.SaveChangesAsync();
    }
  }
}
