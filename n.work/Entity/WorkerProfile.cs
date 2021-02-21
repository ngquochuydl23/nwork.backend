using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Entity
{
  public class WorkerProfile
  {
    public int AccountId { get; set; }

    public string Phonenumber { get; set; }

    public string Fullname { get; set; }

    public string Email { get; set; }

    public bool Active { get; set; }

    public bool Rated { get; set; }

    public virtual WorkerAccount WorkerAccount { get; set; }
  }
}
