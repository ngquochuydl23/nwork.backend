using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace n.work.Controllers
{
  public class RestaurantController : BaseApiController
  {

    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }
  }
}
