using Microsoft.AspNetCore.Mvc;
using n.work.Models;
using System;
using System.Collections.Generic;

namespace n.work.Controllers
{
  public class SettingController : BaseApiController
  {
    [HttpGet("Notification")]
    public string Get()
    {
      return "value";
    }

    [HttpPost("Notification/PushNotification")]
    public IActionResult SwitchPushNotification([FromBody] RequestSwitch requestBody)
    {
      return Ok(requestBody);
    }

    [HttpPost("Notification/Promotions")]
    public IActionResult SwitchPromotions([FromBody] RequestSwitch requestBody)
    {
      return Ok(requestBody);
    }
  }
}
