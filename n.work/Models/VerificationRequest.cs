using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class VerificationRequest
  {
    [Required]
    public string OtpCode { get; set; }
  }
}
