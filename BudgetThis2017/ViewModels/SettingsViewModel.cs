using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetThis2017.ViewModels
{
  public class SettingsViewModel
  {
    public Guid UserID { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayName("Previous Periods To Show")]
    [Range(1, 4, ErrorMessage = "The value must be between {1} and {2}")]
    public int PreviousPeriodsToShow { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayName("Checks To Show")]
    [Range(1, 8, ErrorMessage = "The value must be between {1} and {2}")]
    public int ChecksToShow { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayName("Week Start Day")]
    [Range(0, 6, ErrorMessage = "The value must be between {1} and {2}")]
    public int WeekStartDay { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayName("Days In Period")]
    [Range(1, 31, ErrorMessage = "The value must be between {1} and {2}")]
    public int DaysInPeriod { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayName("Add Tithe")]
    public bool AddTithe { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayName("Tithe Multiplier")]
    [Range(1, 2, ErrorMessage = "The value must be between {1} and {2}")]
    public decimal TitheMultiplier { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayName("Tithe Background Color")]
    public string TitheBGColor { get; set; }

    [Required(ErrorMessage = "Required")]
    [DisplayName("Tithe Fore Color")]
    public string TitheForeColor { get; set; }

    public decimal PaycheckAmount { get; set; }

    public SettingsViewModel(Guid userid)
    {
    }

    public static SettingsViewModel GetSettings(Guid userid)
    {
      //PBProxy p = new PBProxy();
      //Setting temp = p.GetSettings(userid);
      //Paycheck pc = p.GetPaychecks(userid).FirstOrDefault();
      //p = null;

      SettingsViewModel Return = null;// Mapper.Map<SettingsViewModel>(temp);

      Return.PaycheckAmount = 0;// pc == null ? 0 : pc.Amount;

      return Return;
    }
  }
}
