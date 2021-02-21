using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Interface
{
  public interface INotificationService
  {
    bool SendPushNotification(string[] deviceTokens, string title, string body, object data);
  }

  public class NotificationService : INotificationService
  {
    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    public NotificationService(DatabaseContext context)
    {
      this.context = context;
    }

    public NotificationService(DatabaseContext context, IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
      this.context = context;
    }

    public bool SendPushNotification(string[] deviceTokens, string title, string body, object data)
    {
      return false;
    }
  }

}
