using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetThis2017.Models
{
    public class Payday
  {
    public int Id { get; set; }
    public Guid UserId { get; set; }

    [DisplayName("Start Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public System.DateTime Paydate { get; set; }
  }
}
