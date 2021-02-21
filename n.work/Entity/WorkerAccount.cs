using n.work.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Entity
{
  public class WorkerAccount
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Index(IsUnique = true)]
    public int Id { get; set; }

    public string Phonenumber { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime LastLogin { get; set; }

    public virtual WorkerProfile WorkerProfile { get; set; }

    public virtual ICollection<OrderDetail> OrderDetail { get; set; }
  }
}
