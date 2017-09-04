using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
    public class PaycheckDetailsViewModel
  {
    public PaycheckSummaryViewModel Summary { get; set; }
    public List<DayViewModel> Days { get; set; }
  }
}
