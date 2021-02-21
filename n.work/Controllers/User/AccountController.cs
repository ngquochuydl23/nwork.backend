using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Interface;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;


namespace n.work.Controllers
{
  public class AccountController : BaseApiController
  {

    private UserService userService;

    private readonly DatabaseContext context;

    public AccountController(DatabaseContext _context, IOptions<AppSettings> appSettings)
    {
      context = _context;
      userService = new UserService(_context, appSettings);
    }

    [HttpPost("RequestOtp")]
    public IActionResult RequestOtp(OtpRequest otpRequest)
    {
      var otpResponse = userService.SendOtpToUser(otpRequest);
      return Ok(otpResponse);
    }

    [HttpPost("Verification")]
    public IActionResult VerifyCode(VerificationRequest verificationRequest) {
      string authorization = AuthorizationString();
      var verificationResponse = userService.VerificationOTP(verificationRequest, authorization);
      return Ok(verificationResponse);
    }

    [HttpPost("Authenticate")]
    public IActionResult AuthenticateResponse(AuthenticateRequest authenticateRequest)
    {
      string authorization = AuthorizationString();
      var user = userService.Authenticate(authenticateRequest);
      return Ok(user);
    }

    [HttpPost("AddUserInformation")]
    public IActionResult AddUserInformation([FromBody] Profile profile)
    {
      string authorization = AuthorizationString();
      var user = userService.AddUserInformation(authorization, profile);
      return Ok(user.Result);
    }

    [HttpDelete("DeleteAccount")]
    public IActionResult Delete()
    {
      string authorization = AuthorizationString();
      var user = userService.DeleteAccount(authorization);
      return Ok(user);
    }
  }
}
