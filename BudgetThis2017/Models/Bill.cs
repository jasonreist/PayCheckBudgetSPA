using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
  public class Bill
  {
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public decimal Amount { get; set; }
    public int DueDay { get; set; }
    public string BackgroundColor { get; set; }
    public string ForeColor { get; set; }

    public ICollection<CustomBill> CustomBills { get; set; }
  }
}
