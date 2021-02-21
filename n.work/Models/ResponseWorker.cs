using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class ResponseWorker
  {
    public int WorkerId { get; set; }

    public string Name { get; set; }
    public string Avatar { get; set; }

    public double Rate { get; set; }

    public string Motor { get; set; }
  }
}
