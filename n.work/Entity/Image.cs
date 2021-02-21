using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Entity
{
  public class Image
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImageId { get; set; }

    public string Title { get; set; }

    public string ImageName { get; set; }

    public DateTime CreateAt { get; set; }

    [NotMapped]
    public IFormFile ImageFile { get; set; }
  }
}
