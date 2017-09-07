using System.Collections.Generic;

namespace BudgetThis2017.ViewModels
{
  public class CustomBillsViewModel
  {
    public BillViewModel Bill { get; set; }
    public List<CustomBillViewModel> CustomBills { get; set; }

    public CustomBillsViewModel(int id)
    {
      this.CustomBills = new List<CustomBillViewModel>();

      //PBProxy p = new PBProxy();
      //Bill b = p.GetBill(id);
      //this.Bill = Mapper.Map<Bill, BillViewModel>(b);
      //this.Bill.DueDaySuffix = Utility.IntSuffix(this.Bill.DueDay);

      //IEnumerable<CustomBill> cbills = p.GetCustomBills(b.Id);
      //if (cbills != null)
      //{
      //  this.CustomBills = Mapper.Map<List<CustomBillViewModel>>(cbills);
      //}

      //p = null;
    }
  }
}
