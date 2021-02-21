using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class Token
  {
    public int userId { get; set; }

    public string otpCode { get; set; }

    public string expire { get; set; }

    public Token(int userId,string otpCode,string expire)
    {
      this.userId = userId;
      this.otpCode = otpCode;
      this.expire = expire;
    }

    public Token(int userId, string expire)
    {
      this.userId = userId;
      this.expire = expire;
    }
  }
}
