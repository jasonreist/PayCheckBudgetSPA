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
  public class CustomBillsController : Controller
  {
    private UserManager<BudgetUser> _userManager;
    private IBudgetThisRepository _repository;
    private ILogger<BillsController> _logger;

    public CustomBillsController(IBudgetThisRepository repository, ILogger<BillsController> logger, UserManager<BudgetUser> userManager)
    {
      _repository = repository;
      _logger = logger;
      _userManager = userManager;
    }


    [HttpPost("api/custombills")]
    public async Task<IActionResult> Post([FromBody]CustomBillViewModel bill)
    {
      if (ModelState.IsValid)
      {
        var newcBill = Mapper.Map<CustomBill>(bill);
        _repository.AddCustomBill(newcBill);

        if (await _repository.SaveChangesAsync())
        {
          return Created($"api/custombills/{newcBill.Id}", Mapper.Map<CustomBillViewModel>(newcBill));
        }
      }
      return BadRequest("Failed to save the bill");
    }

    [HttpPut("api/bills/{id}")]
    public async Task<IActionResult> Put([FromBody]BillViewModel bill)
    {
      if (ModelState.IsValid)
      {
        var thiscBill = Mapper.Map<CustomBill>(bill);
        _repository.UpdateCustomBill(thiscBill);

        if (await _repository.SaveChangesAsync())
        {
          return Created($"api/custombills/{thiscBill.Id}", Mapper.Map<CustomBillViewModel>(thiscBill));
        }
      }
      return BadRequest("Failed to save the bill");
    }


    [HttpDelete("api/custombills/delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      if (ModelState.IsValid)
      {
        var cbill = _repository.GetCustomBill(id);
        if (cbill == null)
        {
          return BadRequest("Custom bill does not exist.");
        }
        _repository.DeleteCustomBill(cbill);

        if (await _repository.SaveChangesAsync())
        {
          return Ok($"api/custombills/");
        }
      }
      return BadRequest("Failed to delete the custom bill.");
    }

  }
}
