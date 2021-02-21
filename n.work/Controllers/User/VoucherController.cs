using Microsoft.AspNetCore.Mvc;

namespace n.work.Controllers
{
  public class VoucherController : BaseApiController
  {
    [HttpGet]
    public IActionResult Get()
    {
      return Ok("abc");
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      return Ok("abc");
    }
  }
}
