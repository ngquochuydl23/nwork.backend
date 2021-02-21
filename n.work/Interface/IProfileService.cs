using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Entity;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace n.work.Interface
{
  public interface IProfileService
  {
    Account GetProfile(string authorization);
    Task<Account> UpdateProfile(string authorization, UpdateProfile newProfile);

    string SaveImage(string base64image);

    void DeleteImage(string imageName);
  }
  public class ProfileService : BaseService,IProfileService
  {

    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    private readonly IWebHostEnvironment _environment;

    public ProfileService(DatabaseContext context, IOptions<AppSettings> appSettings, IWebHostEnvironment IHostingEnvironment) : base(appSettings)
    {
      _appSettings = appSettings.Value;
      _environment = IHostingEnvironment;
      this.context = context;
    }

    public void DeleteImage(string imageName)
    {
      
    }

    public Account GetProfile(string authorization)
    {
      var authorizationString = getTokenFromAuthorization(authorization);
      var tokenContent = readTokenAuthenticate(authorizationString);
      var userID = tokenContent.userId;
      var account = context.Account.Include(user => user.Profile).SingleOrDefault(account => account.Id == userID);
      return account;
    }

    public string SaveImage(string base64image)
    {
      if (string.IsNullOrEmpty(base64image))
        return null;

      string newFileName = "nwork-avatar-" + DateTime.Now.ToString("yyyyMMddHHmmss");

      string imageName = newFileName + ".jpg";
      string path = Path.Combine(_environment.WebRootPath + "/home/ubuntu/Image", imageName);

      byte[] imageBytes = Convert.FromBase64String(base64image);

      File.WriteAllBytes(path, imageBytes);

      return "http://52.187.117.17/nwork-api/image/" + imageName;
    }



    public async Task<Account> UpdateProfile(string authorization, UpdateProfile newProfile)
    {
      var authorizationString = getTokenFromAuthorization(authorization);
      var tokenContent = readTokenAuthenticate(authorizationString);

      var userID = tokenContent.userId;
      var account = context.Account.Include(user => user.Profile).SingleOrDefault(account => account.Id == userID);

      var currentProfile = account.Profile;

      if (newProfile != null)
      {
        account.Phonenumber = newProfile.Phonenumber;
        account.Profile.Fullname = newProfile.Fullname;
        account.Profile.Email = newProfile.Email;
      }

      if (newProfile.Avatar != null)
        account.Profile.Avatar = SaveImage(newProfile.Avatar);

      await context.SaveChangesAsync();
      return account;
    }
  }
}
