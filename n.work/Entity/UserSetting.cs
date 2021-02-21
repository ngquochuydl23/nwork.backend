using n.work.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Entity
{
  public class UserSetting
  {
    public int SettingId { get; set; }

    public int UserId { get; set; }

    public bool IsPushNotification { get; set; }

    public bool IsPromotions { get; set; }

    public virtual Account User { get; set; }
  }
}
