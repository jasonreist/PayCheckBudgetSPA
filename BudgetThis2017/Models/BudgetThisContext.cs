using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BudgetThis2017.Models
{
  public class BudgetThisContext : IdentityDbContext<BudgetUser>
  {
    private IConfiguration _config;
    public BudgetThisContext(IConfiguration config, DbContextOptions options) : base(options)
      {
      _config = config;
    }

    public DbSet<Setting> Settings { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<CustomBill> CustomBills { get; set; }
    public DbSet<Paycheck> Paychecks { get; set; }
    public DbSet<PayDay> Paydays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      optionsBuilder.UseSqlServer(_config["ConnectionStrings:BudgetContextConnection"]);
    }
  }
}
