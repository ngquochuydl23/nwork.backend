using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Interface;
using n.work.Models;
using System;
using System.Collections.Generic;

namespace n.work.Controllers
{
  public class OrderController : BaseApiController
  {
    private OrderService orderService;

    private readonly DatabaseContext context;

    public OrderController(DatabaseContext _context, IOptions<AppSettings> appSettings)
    {
      context = _context;
      orderService = new OrderService(_context, appSettings);
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    [HttpGet]
    public IActionResult GetListOrder()
    {
      var authorization = AuthorizationString();
      if (authorization == null)
        return Unauthorized();
      var listOrder = orderService.GetListOrder(authorization);
      return Ok(listOrder);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrder newOrder)
    {
      var result = orderService.CreateOrder(newOrder).Result;
      return Ok(result);
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {

    }
  }
}
