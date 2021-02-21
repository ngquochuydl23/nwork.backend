using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class Account
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Index(IsUnique = true)]
    public int Id { get; set; }

    public string Phonenumber { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime LastLogin { get; set; }

    public virtual Profile Profile { get; set; }

    public virtual ICollection<OrderDetail> OrderDetail { get; set; }
  }
}
