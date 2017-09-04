using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BudgetThis2017.Models
{
    public class BudgetThisRepository: IBudgetThisRepository
  {
    private BudgetThisContext _context;
    private ILogger<BudgetThisRepository> _logger;
    public BudgetThisRepository(BudgetThisContext context, ILogger<BudgetThisRepository> logger)
    {
      _context = context;
      _logger = logger;
    }
    public async Task<bool> SaveChangesAsync()
    {
      return (await _context.SaveChangesAsync() > 0);
    }

    public IEnumerable<Bill> GetBillsByUser(Guid identity)
    {
      var temp = _context.Bills.Include(t => t.CustomBills).Where(bill => bill.UserId == identity).ToList();
      temp.ForEach(x => x.CustomBills = x.CustomBills.OrderBy(y => y.BillDate).ToList());
      return temp;
    }
    public void AddBill(Bill bill)
    {
      _context.Add(bill);
    }
    public void DeleteBill(Bill bill)
    {
      _context.Remove(bill);
    }

    public void UpdateBill(Bill bill)
    {
      _context.Update(bill);
    }

    public CustomBill GetCustomBill(int id)
    {
      return _context.CustomBills.Where(c => c.Id == id).FirstOrDefault();
    }

    public void AddCustomBill(CustomBill cbill)
    {
      _context.Add(cbill);
    }

    public void DeleteCustomBill(CustomBill cbill)
    {
      _context.Remove(cbill);
    }

    public void UpdateCustomBill(CustomBill cbill)
    {
      _context.Update(cbill);
    }

    public Setting GetSettingsByUser(Guid identity)
    {
      return _context.Settings.FirstOrDefault(setting => setting.UserID == identity);
    }

    public void UpdateSettings(Setting settings)
    {
      _context.Update(settings);
    }
  }
}
