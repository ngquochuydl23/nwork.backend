using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n.work.DataContext;
using n.work.Entity;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace n.work.Controllers
{
  public class ImageController : BaseApiController
  {

    private readonly IWebHostEnvironment _environment;

    private readonly DatabaseContext context;

    public ImageController(DatabaseContext _context, IWebHostEnvironment IHostingEnvironment)
    {
      context = _context;
      _environment = IHostingEnvironment;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync()
    {
      var file = HttpContext.Request.Form;
      if (file.Files.Count > 0)
      {
        var image = new Image();

        var files = file.Files[0];
        var fileName = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
        var myUniqueFileName = Convert.ToString(Guid.NewGuid());
        var FileExtension = Path.GetExtension(fileName);
        var newFileName = myUniqueFileName + FileExtension;

        image.ImageName = newFileName;
        string path = Path.Combine(_environment.WebRootPath + "/home/ubuntu/Image", newFileName);
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
          await files.CopyToAsync(fileStream);
        }

        context.Images.Add(image);
        await context.SaveChangesAsync();

        return Ok(new
        {
          image = "http://52.187.117.17/nwork-api/image/" + newFileName
        });
      }
      return BadRequest();
    }

    [HttpGet("{filename}")]
    public async Task<IActionResult> GetImage(string filename)
    {
      var file = System.IO.File.ReadAllBytes(_environment.WebRootPath + "/home/ubuntu/Image/" + filename);
      return File(file, "image/jpeg");
    }
  }
}
