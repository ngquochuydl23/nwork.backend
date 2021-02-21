using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using n.work.DataContext;
using n.work.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace n.work.Interface
{
  public interface IUserService
  {
    Account Authenticate(AuthenticateRequest model);

    VerificationResponse VerificationOTP(VerificationRequest verificationRequest,string authorization);

    OtpResponse SendOtpToUser(OtpRequest requestOTP);
    List<Account> GetAll();
    Account GetByPhoneNumber(string phonenumber);

    Account GetByToken(string token);

    Account GetByID(int id);

    Task<Account> UpdateAccount(int userID, UpdateProfile profile);

    Task<Account> AddUserInformation(string authorization, Profile profile);

    Task<Account> CreateAccount(Account account);

    Account DeleteAccount(string authorization);
  }

  public class UserService : BaseService, IUserService
  {
    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    public UserService(DatabaseContext context, IOptions<AppSettings> appSettings) : base(appSettings)
    {
      _appSettings = appSettings.Value;
      this.context = context;
    }
    public Account Authenticate(AuthenticateRequest authenticateRequest)
    {
      var user = GetByToken(authenticateRequest.Token);
      return updateLastLogin(user.Id, DateTime.Now).Result;
    }

    public List<Account> GetAll()
    {
      return context.Account.ToList();
    }

    public Account GetByPhoneNumber(string phonenumber)
    {
      return context.Account.FirstOrDefault(account => account.Phonenumber == phonenumber);
    }

    public async Task<Account> CreateAccount(Account account)
    {
      var nowDateTime = DateTime.Now;
      var newAccount = new Account()
      {
        Phonenumber = account.Phonenumber,
        CreatedOn = nowDateTime,
      };
      await context.Account.AddAsync(newAccount);
      await context.SaveChangesAsync();
      return newAccount;
    }

    public void SendOtp(OtpRequest requestOTP, string OTPCode)
    {
      string accountSid = "AC04da7ce1e879190ca577ca8d198cfafc";
      string authToken = "af95a7f404a6591238080deaba00f9a2";

      TwilioClient.Init(accountSid, authToken);

      MessageResource.Create(
          body: $"Your verification code is : {OTPCode}",
          from: new Twilio.Types.PhoneNumber("+13342139746"),
          to: new Twilio.Types.PhoneNumber("+84868684961")
      );
    }

    public string createOtpCode()
    {
      Random generator = new Random();
      return generator.Next(0, 999999).ToString("D6");
    }

    public VerificationResponse VerificationOTP(VerificationRequest verificationRequest,string authorization)
    {
      var token = getTokenFromAuthorization(authorization);
      var tokenContent = readTokenVerifyCode(token);
      var otpFromUser = verificationRequest.OtpCode;
      var otpFromToken = tokenContent.otpCode;
      if (otpFromUser == otpFromToken && !isExpired(tokenContent.expire))
      {
        var user = GetByID(tokenContent.userId);
        if (user.Profile == null)
          user.Profile = new Profile();

        user.Profile.Active = true;
        context.Account.Update(user);
        context.SaveChanges();
        user = updateLastLogin(tokenContent.userId, DateTime.Now).Result;
        var authToken = generateJwtTokeAuthResponse(user);
        return new VerificationResponse(user, authToken);
      }
      return null;
    }

    public OtpResponse SendOtpToUser(OtpRequest requestOTP)
    {
      var resendTime = 60;
      var otpCode = createOtpCode();
      var user = GetByPhoneNumber(requestOTP.phonenumber);
      if (user == null)
      {
        var newUser = CreateAccount(new Account()
        {
          Phonenumber = requestOTP.phonenumber
        });
        user = newUser.Result;
      }
      SendOtp(requestOTP, otpCode);
      var token = generateJwtTokenRequestOTP(user, otpCode);
      return new OtpResponse(resendTime, requestOTP, token);
    }

    private string generateJwtTokenRequestOTP(Account account, String OtpCode)
    {
      var token = generateJwtToken(new[] {
          new Claim("user_id", account.Id.ToString()),
          new Claim("otp_code", OtpCode)
        }, DateTime.UtcNow.AddSeconds(60));
      return token;
    }

    private string generateJwtTokeAuthResponse(Account account)
    {
      if (account != null)
      {
        var token = generateJwtToken(new[] {
          new Claim("user_id", account.Id.ToString()),
        }, DateTime.UtcNow.AddDays(10)); ;
        return token;
      }
      return null;
    }

    private Token readTokenVerifyCode(string token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var parsedJwt = tokenHandler.ReadToken(token) as JwtSecurityToken;
      var userId = parsedJwt
        .Claims
        .Where(column => column.Type == "user_id")
        .Select(column => column.Value).SingleOrDefault();
      var OtpCode = parsedJwt
        .Claims
        .Where(column => column.Type == "otp_code")
        .Select(column => column.Value).SingleOrDefault();
      var expires = parsedJwt
        .Claims
        .Where(column => column.Type == "exp")
        .Select(column => column.Value).SingleOrDefault();

      return new Token(Convert.ToInt32(userId),OtpCode,expires);
    }

    private bool isExpired(string expired)
    {
      var currentTime = DateTime.Now.Second;
      return currentTime > Convert.ToInt32(expired);
    }

    public string generateJwtToken(Claim[] listClaim, DateTime Expires)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(listClaim),
        Expires = Expires,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }

    public async Task<Account> UpdateAccount(int userID,UpdateProfile profile)
    {
      var currentAccount = GetByID(userID);
      if (profile.Phonenumber != null)
        currentAccount.Phonenumber = profile.Phonenumber;
      currentAccount.Profile.Email = profile.Email;
      currentAccount.Profile.Fullname = profile.Fullname;
      await context.SaveChangesAsync();
      return currentAccount;
    }

    public Account GetByID(int id)
    {
      if (id != 0)
        return context.Account.Include(user => user.Profile).SingleOrDefault(account => account.Id == id);
      return null;
    }

    private async Task<Account> updateLastLogin(int id,DateTime lastLogin)
    {
      var user = GetByID(id);
      user.LastLogin = lastLogin;
      await context.SaveChangesAsync();
      return user;
    }

    public Account GetByToken(string authToken)
    {
      var tokenContent = readTokenAuthenticate(authToken);
      if (!isExpired(tokenContent.expire))
      {
        var user = GetByID(tokenContent.userId);
        return user;
      }
      return null; 
    }

    public async Task<Account> AddUserInformation(string authorization, Profile profile)
    {
      var token = getTokenFromAuthorization(authorization);
      var currentAccount = GetByToken(token);
      currentAccount.Profile.Email = profile.Email;
      currentAccount.Profile.Fullname = profile.Fullname;
      await context.SaveChangesAsync();
      return currentAccount;
    }

    public Account DeleteAccount(string authorization)
    {
      var token = getTokenFromAuthorization(authorization);
      var account = GetByToken(token);
      context.Account.Remove(account);
      context.SaveChanges();
      return account;
    }
  }
}
