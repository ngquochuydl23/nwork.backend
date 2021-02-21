using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace n.work.Interface
{
  public interface IOrderService
  {
    Task<OrderDetail> CreateOrder(CreateOrder order);

    List<OrderDetail> GetListOrder(string authorization);
  }
  public class OrderService : BaseService, IOrderService
  {

    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    public OrderService(DatabaseContext context, IOptions<AppSettings> appSettings) : base(appSettings)
    {
      _appSettings = appSettings.Value;
      this.context = context;
    }

    public async Task<OrderDetail> CreateOrder(CreateOrder order)
    {
      var newOrder = new OrderDetail()
      {
        Address = order.Address,
        WorkerId = order.WorkerId,
        CustomerId = order.CustomerId,
        CreatedOn = DateTime.Now
        
      };

      newOrder.Customer = context.Account.SingleOrDefault(account => account.Id == order.CustomerId);
      newOrder.Worker = context.WorkerAccount.SingleOrDefault(account => account.Id == order.WorkerId);


      context.OrderDetail.Add(newOrder);
      context.SaveChanges();

      newOrder.ItemOrder = new ItemOrder();

      context.OrderDetail.Update(newOrder);
      await context.SaveChangesAsync();

      return newOrder;
    }

    public List<OrderDetail> GetListOrder(string authorization)
    {
      var authorizationString = getTokenFromAuthorization(authorization);
      var tokenContent = readTokenAuthenticate(authorizationString);

      Console.WriteLine(tokenContent.userId);
      var order = context.OrderDetail
        .Include(order => order.ItemOrder)
        .Include(order => order.Customer)
        .Where(order => order.CustomerId == tokenContent.userId).ToList();
      return order;
    }
  }
}
