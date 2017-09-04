using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
    public class Setting
  {
    [Key]
    public Guid UserID { get; set; }

    public int PreviousPeriodsToShow { get; set; }
    public int ChecksToShow { get; set; }
    public int WeekStartDay { get; set; }
    public int DaysInPeriod { get; set; }
    public bool AddTithe { get; set; }
    public decimal TitheMultiplier { get; set; }
    public string TitheBGColor { get; set; }
    public string TitheForeColor { get; set; }
  }
}
