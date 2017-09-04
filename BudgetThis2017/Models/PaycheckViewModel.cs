using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
  public class PaycheckViewModel
  {
    public Int32 ID { get; set; }
    public Guid UserID { get; set; }
    public string Type { get; set; }
    public Paycheck PayCheck { get; set; }
    public bool Exists { get; set; }
  }
}
