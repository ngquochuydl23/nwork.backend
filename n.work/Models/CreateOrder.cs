using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class CreateOrder
  {
    public int CustomerId { get; set; }

    public int WorkerId { get; set; }

    public string Address { get; set; }
    
    public double Price { get; set; }
  }
}
