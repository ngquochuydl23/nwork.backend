using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class Profile
  {
    public int AccountId { get; set; }

    public string Fullname { get; set; }

    public string Email { get; set; }

    public string Avatar { get; set; }

    public bool Active { get; set; }

    public virtual Account Account { get; set; }
  }
}
