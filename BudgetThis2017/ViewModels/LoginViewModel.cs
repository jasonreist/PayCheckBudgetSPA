using System.ComponentModel.DataAnnotations;

namespace BudgetThis2017.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}