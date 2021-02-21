using n.work.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class OrderDetail
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Index(IsUnique = true)]
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int WorkerId { get; set; }

    public string Address { get; set; }

    public DateTime CreatedOn { get; set; }

    public string Status { get; set; }

    public decimal Price { get; set; }

    public virtual ItemOrder ItemOrder { get; set; }

    public virtual Account Customer { get; set; }

    public virtual WorkerAccount Worker { get; set; }
  }
}
