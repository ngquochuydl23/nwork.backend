using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using n.work.DataContext;
using n.work.Hubs;
using n.work.IUserId;
using n.work.Models;
using System;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace n.work
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
      services.AddControllers();
      services.AddSignalR();

      services.Configure<ForwardedHeadersOptions>(options =>
      {
        options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
      });

      services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

      services.AddControllersWithViews().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment()) 
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseRouting();


      //app.UseAuthentication();
      //app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHub<TrackingServiceHub>("/trackingServiceHub");
        endpoints.MapControllers();
      });

      
    }

    
  }
}
