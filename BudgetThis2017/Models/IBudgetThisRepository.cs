using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetThis2017.ViewModels;

namespace BudgetThis2017.Models
{
    public interface IBudgetThisRepository
  {
    Task<bool> SaveChangesAsync();
    IEnumerable<Bill> GetBillsByUser(Guid identity);
    ListBillsViewModel GetListBillsViewModelByUser(Guid identity);
    PaycheckViewModel GetPaycheckViewModel(Guid identity, string paycheckType);
    Paycheck GetPaycheck(Guid identity, string paycheckType);
    IEnumerable<PayDay> GetPaydaysByUser(Guid identity);
    PayDay GetPaydaySeedByUser(Guid identity);
    CustomBill GetCustomBill(int id);
    IEnumerable<CustomBill> GetCustomBills(int billId);
    void AddBill(Bill bill);
    void DeleteBill(Bill bill);
    void UpdateBill(Bill bill);

    void AddCustomBill(CustomBill cbill);
    void DeleteCustomBill(CustomBill cbill);
    void UpdateCustomBill(CustomBill cbill);

    Setting GetSettingsByUser(Guid identity);

    void UpdateSettings(Setting settings);
  }
}
