using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using n.work.DataContext;
using n.work.Entity;
using n.work.Interface;
using n.work.Models;
using System;
using System.Collections.Generic;

namespace n.work.Controllers
{
  public class ProfileController : BaseApiController
  {
    private ProfileService profileService;

    private readonly DatabaseContext context;

    public ProfileController(DatabaseContext _context, IOptions<AppSettings> appSettings,IWebHostEnvironment IHostingEnvironment)
    {
      context = _context;
      profileService = new ProfileService(_context, appSettings, IHostingEnvironment);
    }

    [HttpPut]
    public IActionResult Put([FromBody] UpdateProfile profile)
    {
      var authorization = AuthorizationString();
      if (authorization == null)
        return Unauthorized();
      var updateResult = profileService.UpdateProfile(authorization, profile).Result;
      return Ok(updateResult);
    }

    [HttpGet]
    public IActionResult Get()
    {
      var authorization = AuthorizationString();
      if (authorization == null)
        return Unauthorized();
      var getProfile = profileService.GetProfile(authorization);
      return Ok(getProfile);
    }
  }
}
