using BudgetThis2017.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
  public class ListBillsViewModel
  {
    public int Id { get; set; }

    public List<BillViewModel> Bills { get; set; }

    public ListBillsViewModel()
    {

    }
  }
}
