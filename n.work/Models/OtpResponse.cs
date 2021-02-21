using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class OtpResponse
  {
    public int resendTime { get; set; }

    public string message { get; set; }

    public string token { get; set; }

    public OtpResponse(int resendTime, OtpRequest otpRequest,string token)
    {
      this.resendTime = resendTime;
      this.message = $"Enter the OTP code we sent via SMS to your registed phone number {otpRequest.phonenumber}";
      this.token = token;
    }
  }
}
