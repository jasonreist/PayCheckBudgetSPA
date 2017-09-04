using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetThis2017.ViewModels
{
  public class CustomBillViewModel
  {
    public int Id { get; set; }

    public int BillId { get; set; }

    [DisplayName("Due Date")]
    public DateTime BillDate { get; set; }

    public decimal Amount { get; set; }

    public bool Exists { get; set; }
  }
}
