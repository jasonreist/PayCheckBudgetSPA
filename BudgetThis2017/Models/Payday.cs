using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BudgetThis2017.Models
{
  public class PayDay
  {
    public int Id { get; set; }
    public Guid UserId { get; set; }

    [DisplayName("Start Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public System.DateTime Paydate { get; set; }
  }
}
