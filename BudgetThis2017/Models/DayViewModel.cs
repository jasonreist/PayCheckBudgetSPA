using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetThis2017.ViewModels;

namespace BudgetThis2017.Models
{
  public class DayViewModel
  {
    public int Index { get; set; }
    public DateTime Date { get; set; }
    public List<BillViewModel> Bills { get; set; }
    public PaycheckDetailsViewModel Paycheck { get; set; }
    public int PaydayIndex { get; set; }
    public DayViewModel() { this.Bills = new List<BillViewModel>(); }
    public string MonthColor { get; set; }
  }
}
