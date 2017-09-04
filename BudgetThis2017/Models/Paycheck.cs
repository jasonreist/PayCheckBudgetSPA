using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
    public class Paycheck
  {
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }

  }
}
