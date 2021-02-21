using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Interface;
using n.work.Models;
using System.Collections.Generic;

namespace n.work.Controllers
{
  public class HomeController : BaseApiController
  {

    private HomeService homeService;

    private readonly DatabaseContext context;

    public HomeController(DatabaseContext _context, IOptions<AppSettings> appSettings)
    {
      context = _context;
      homeService = new HomeService(_context, appSettings);
    }

    [HttpGet("GetStarted")]
    public IActionResult GetStarted()
    {
      var authorization = AuthorizationString();
      if (authorization == null)
        return Unauthorized();
      var responseHome = homeService.GetStarted(authorization);
      return Ok(responseHome);
    }
    
    [HttpPost("GetHomeContent")]
    public IActionResult GetHomeContent(RequestLocation location)
    {
      var authorization = AuthorizationString();
      if (location == null)
        return BadRequest();
      var contentHome = homeService.GetHomeContent(authorization);
      return Ok(contentHome);
    }
  }
}
