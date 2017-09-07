using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetThis2017.ViewModels;
using BudgetThis2017.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;

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
    public ListBillsViewModel GetListBillsViewModelByUser(Guid identity)
    {
      var Return = new ListBillsViewModel();
      Return.Bills = new List<BillViewModel>();

      foreach (Bill b in GetBillsByUser(identity))
      {
        BillViewModel bill = Mapper.Map<BillViewModel>(b);
        bill.DueDaySuffix = Utility.IntSuffix(bill.DueDay);

        IEnumerable<CustomBill> cbills = GetCustomBills(b.Id).OrderBy(a => a.BillDate);
        if (cbills != null)
          bill.CustomBillCount = cbills.Count();

        Return.Bills.Add(bill);
      }
      return Return;
    }
    public IEnumerable<PayDay> GetPaydaysByUser(Guid identity)
    {
      return _context.Paydays.Where(p => p.UserId == identity).ToList();
    }
    public PayDay GetPaydaySeedByUser(Guid identity)
    {
      return _context.Paydays.Where(p => p.UserId == identity).ToList().FirstOrDefault();
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
    public IEnumerable<CustomBill> GetCustomBills(int billId)
    {
      return _context.CustomBills.Where(c => c.BillId == billId).ToList();
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

    public PaycheckViewModel GetPaycheckViewModel(Guid identity, string paycheckType)
    {
      var temp = _context.Paychecks.FirstOrDefault(p => p.UserId == identity && p.Type == paycheckType);
      return Mapper.Map<PaycheckViewModel>(temp);
    }
    public Paycheck GetPaycheck(Guid identity, string paycheckType)
    {
      return _context.Paychecks.FirstOrDefault(p => p.UserId == identity && p.Type == paycheckType);
    }
  }
}
