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

    public ListBillsViewModel(Guid userid)
    {
      this.Bills = new List<BillViewModel>();

      //PBProxy p = new PBProxy();
      //foreach (Bill b in p.GetBills(userid).OrderBy(a => a.DueDay))
      //{
      //  BillViewModel bill = Mapper.Map<BillViewModel>(b);
      //  bill.DueDaySuffix = Utility.IntSuffix(bill.DueDay);

      //  IEnumerable<CustomBill> cbills = p.GetCustomBills(b.Id).OrderBy(a => a.BillDate);
      //  if (cbills != null)
      //    bill.CustomBillCount = cbills.Count();

      //  this.Bills.Add(bill);
      //}
      p = null;
    }
  }
}
