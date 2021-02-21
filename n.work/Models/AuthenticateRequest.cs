using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class AuthenticateRequest
  {
    [Required]
    public string Token { get; set; }
  }
}
