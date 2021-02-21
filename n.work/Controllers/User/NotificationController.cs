using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace n.work.Controllers
{
  public class NotificationController : BaseApiController
  {
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }


    [HttpPost("{id}")]
    public void Post([FromBody] string value)
    {
    }


  }
}
