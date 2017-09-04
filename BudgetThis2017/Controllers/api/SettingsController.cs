using AutoMapper;
using BudgetThis2017.Models;
using BudgetThis2017.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BudgetThis2017.Controllers.api
{
  public class SettingsController : Controller
  {
    private UserManager<BudgetUser> _userManager;
    private IBudgetThisRepository _repository;
    private ILogger<SettingsController> _logger;

    public SettingsController(IBudgetThisRepository repository, ILogger<SettingsController> logger, UserManager<BudgetUser> userManager)
    {
      _repository = repository;
      _logger = logger;
      _userManager = userManager;
    }


    [HttpGet("api/settings")]
    public IActionResult Get()
    {
      try
      {
        var results = _repository.GetSettingsByUser(new Guid(_userManager.GetUserId(HttpContext.User)));
        return Ok(Mapper.Map<SettingsViewModel>(results));
      }
      catch (Exception ex)
      {
        _logger.LogError("Error: " + ex.Message);
        return BadRequest("Error happened: " + ex.Message);
      }
    }

    //update
    [HttpPut("api/settings/update/{id}")]
    public async Task<IActionResult> Put([FromBody]SettingsViewModel settings)
    {
      if (ModelState.IsValid)
      {
        var thisSetting = Mapper.Map<Setting>(settings);
        thisSetting.UserID = new Guid(_userManager.GetUserId(HttpContext.User));
        _repository.UpdateSettings(thisSetting);

        if (await _repository.SaveChangesAsync())
        {
          return Created($"api/settings/{thisSetting.UserID}", Mapper.Map<SettingsViewModel>(thisSetting));
        }
      }
      return BadRequest("Failed to save the Settings");
    }
  }
}
