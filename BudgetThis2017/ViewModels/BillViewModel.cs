using System;
using System.Collections.Generic;
using System.ComponentModel;
using BudgetThis2017.Models;

namespace BudgetThis2017.ViewModels
{
  public class BillViewModel
  {
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public decimal Amount { get; set; }

    [DisplayName("Due Day")]
    public int DueDay { get; set; }

    public string DueDaySuffix { get; set; }
    public int CustomBillCount { get; set; }

    [DisplayName("Background Color")]
    public string BackgroundColor { get; set; }

    [DisplayName("Fore Color")]
    public string ForeColor { get; set; }

    public ICollection<CustomBill> CustomBills { get; set; }
    //public PaycheckDetailsViewModel Paycheck { get; set; }
  }
}
