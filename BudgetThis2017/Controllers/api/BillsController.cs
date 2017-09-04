using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BudgetThis2017.Models;
using BudgetThis2017.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BudgetThis2017.Controllers.api
{
  [Authorize]
  public class BillsController : Controller
  {
    private UserManager<BudgetUser> _userManager;
    private IBudgetThisRepository _repository;
    private ILogger<BillsController> _logger;

    public BillsController(IBudgetThisRepository repository, ILogger<BillsController> logger, UserManager<BudgetUser> userManager)
    {
      _repository = repository;
      _logger = logger;
      _userManager = userManager;
    }

    [HttpGet("api/bills")]
    public IActionResult Get()
    {
      try
      {
        var results = _repository.GetBillsByUser(new Guid(_userManager.GetUserId(HttpContext.User)));
        return Ok(Mapper.Map<IEnumerable<BillViewModel>>(results).OrderBy(b => b.DueDay).ToList());
      }
      catch (Exception ex)
      {
        _logger.LogError("Error: " + ex.Message);
        return BadRequest("Error happened: " + ex.Message);
      }
    }

    [HttpGet("api/bills/{id}")]
    public IActionResult Get(int id)
    {
      try
      {
        var result = _repository.GetBillsByUser(new Guid(_userManager.GetUserId(HttpContext.User))).FirstOrDefault(b => b.Id == id);
        return Ok(Mapper.Map<BillViewModel>(result));
      }
      catch (Exception ex)
      {
        _logger.LogError("Error: " + ex.Message);
        return BadRequest("Error getting bill " + id + ": " + ex.Message);
      }
    }

    [HttpPost("api/bills")]
    public async Task<IActionResult> Post([FromBody]BillViewModel bill)
    {
      if (ModelState.IsValid)
      {
        var newBill = Mapper.Map<Bill>(bill);
        newBill.UserId = new Guid(_userManager.GetUserId(HttpContext.User));
        newBill.BackgroundColor = "#000000";
        newBill.ForeColor = "#00ff00";
        newBill.Icon = "fa fa-asterisk";
        _repository.AddBill(newBill);

        if (await _repository.SaveChangesAsync())
        {
          return Created($"api/bills/{newBill.Id}", Mapper.Map<BillViewModel>(newBill));
        }
      }
      return BadRequest("Failed to save the bill");
    }

    [HttpPut("api/bills/update/{id}")]
    public async Task<IActionResult> Put([FromBody]BillViewModel bill)
    {
      if (ModelState.IsValid)
      {
        var thisBill = Mapper.Map<Bill>(bill);
        thisBill.UserId = new Guid(_userManager.GetUserId(HttpContext.User));
        _repository.UpdateBill(thisBill);
        
        if (await _repository.SaveChangesAsync())
        {
          return Created($"api/bills/{thisBill.Id}", Mapper.Map<BillViewModel>(thisBill));
        }
      }
      return BadRequest("Failed to save the bill");
    }
    
    [HttpDelete("api/bills/delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      if (ModelState.IsValid)
      {
        var bill = _repository.GetBillsByUser(new Guid(_userManager.GetUserId(HttpContext.User))).FirstOrDefault(b => b.Id == id);
        if (bill == null)
        {
          return BadRequest("You cannot delete another users bill.");
        }
        _repository.DeleteBill(bill);
        
        if (await _repository.SaveChangesAsync())
        {
          return Ok($"api/bills/");
        }
      }
      return BadRequest("Failed to delete the bill.");
    }

  }
}
