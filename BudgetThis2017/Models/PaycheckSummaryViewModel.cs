using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
  public class PaycheckSummaryViewModel
  {
    public string CurrentClass { get; set; }
    public DateTime Payday { get; set; }
    public decimal Credits { get; set; }
    public decimal Debits { get; set; }
    public decimal Remaining { get { return this.Credits - this.Debits; } set { } }
  }
}
