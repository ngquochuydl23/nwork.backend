using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace n.work.Interface
{
  public class BaseService
  {

    private readonly AppSettings _appSettings;

    public BaseService(IOptions<AppSettings> appSettings)
    {
      _appSettings = appSettings.Value;
    }

    public string getTokenFromAuthorization(string authorization)
    {
      return authorization.Substring("Bearer ".Length).Trim();
    }

    public Token readTokenAuthenticate(string token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var parsedJwt = tokenHandler.ReadToken(token) as JwtSecurityToken;
      var userId = parsedJwt
        .Claims
        .Where(column => column.Type == "user_id")
        .Select(column => column.Value).SingleOrDefault();
      var expires = parsedJwt
        .Claims
        .Where(column => column.Type == "exp")
        .Select(column => column.Value).SingleOrDefault();
      return new Token(Convert.ToInt32(userId), expires);
    }
  }
}
