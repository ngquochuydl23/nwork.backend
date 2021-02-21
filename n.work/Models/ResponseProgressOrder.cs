using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class ResponseProgressOrder
  {
    public DateTime EstimatedTime { get; set; }

    public string SubProgress { get; set; }

    public string EstimatedTimeMax { get; set; }
  }
}
