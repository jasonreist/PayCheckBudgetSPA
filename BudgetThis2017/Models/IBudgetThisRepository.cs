using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
    public interface IBudgetThisRepository
  {
    Task<bool> SaveChangesAsync();
    IEnumerable<Bill> GetBillsByUser(Guid identity);
    CustomBill GetCustomBill(int id);
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
