using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
    public class CustomBill
  {
    public int Id { get; set; }
    public int BillId { get; set; }
    public DateTime BillDate { get; set; }
    public decimal Amount { get; set; }
  }
}
