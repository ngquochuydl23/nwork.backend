using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class OtpRequest
  {
    [Required]
    public string phonenumber { get; set; }
    [Required]
    public string countryCode { get; set; }
  }
}
