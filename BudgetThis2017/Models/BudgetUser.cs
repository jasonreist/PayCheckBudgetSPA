using Microsoft.AspNetCore.Identity;

namespace BudgetThis2017.Models
{
  public class BudgetUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
