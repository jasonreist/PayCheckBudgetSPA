using System.Threading.Tasks;
using BudgetThis2017.Models;
using BudgetThis2017.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetThis2017.Controllers
{
  public class AuthController : Controller
  {
    private SignInManager<BudgetUser> _signinManager;

    public AuthController(SignInManager<BudgetUser> signInManager)
    {
      _signinManager = signInManager;
    }

    public IActionResult Login()
    {
      if (User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Index", "Home");
      }

      return View();
    }

    public async Task<IActionResult> Logout()
    {
      if (User.Identity.IsAuthenticated)
      {
        await _signinManager.SignOutAsync();
      }
      return RedirectToAction("Index", "home");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        var signInResult = await _signinManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
        if (signInResult.Succeeded)
        {
          if (string.IsNullOrWhiteSpace(returnUrl))
            return RedirectToAction("Index", "home");
          else
            return Redirect(returnUrl);
        }
        else
        {
          ModelState.AddModelError("", "Username or Password incorrect.");
        }
      }

      return View();
    }
  }
}
