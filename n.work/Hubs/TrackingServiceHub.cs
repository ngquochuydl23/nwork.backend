using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Interface;
using n.work.Models;
using Twilio.Rest.Api.V2010.Account;

namespace n.work.Hubs
{
  public class TrackingServiceHub : Hub
  {
    private OrderService orderService;

    private readonly DatabaseContext dataContext;
    private static readonly HashSet<string> ConnectedIds = new HashSet<string>();
    string ON_INCOMING_TRIP = "ON_INCOMING_TRIP";
    string HAVE_FOUND_DRIVER = "HAVE_FOUND_DRIVER";
    string ON_COMING_ORDER = "ON_COMING_ORDER";
    string ON_DRIVER_CANCEL = "ON_DRIVER_CANCEL";
    string ON_CANCEL_ORDER = "ON_CANCEL_ORDER";
    string ON_ARRIVED_RESTAURANT = "ON_ARRIVED_RESTAURANT";


    public TrackingServiceHub(DatabaseContext _context, IOptions<AppSettings> appSettings)
    {
      dataContext = _context;
      orderService = new OrderService(_context, appSettings);
    }

    public async Task OrderAction(string user)
    {
      await Clients.All.SendAsync(ON_INCOMING_TRIP, user);
    }

    public async Task AcceptTrip(string user)
    {
      Console.Write(user);
   
    }
    public async Task DeclineTrip(string user)
    {
      Console.WriteLine("DeclineTrip");
    
    }

    public async Task UpdateLocation(Location newLocation)
    {
      Console.WriteLine($"Latitude : {newLocation.Latitude}");
      Console.WriteLine($"Longitude : {newLocation.Longitude}");
      Console.WriteLine($"Bearing : {newLocation.Bearing}");
      Console.WriteLine("\n");
    }
    public async Task CancelOrder(string user)
    {
      await Clients.All.SendAsync(ON_CANCEL_ORDER, user);
    }

    public async Task ArrivedRestaurant(string user)
    {
      await Clients.All.SendAsync(ON_ARRIVED_RESTAURANT, user);
    }

    public async Task IsComingRestaurant(string user)
    {
      Console.WriteLine("In Coming");
    }
  }
}
