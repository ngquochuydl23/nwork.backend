using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class ResultAction
  {
    public int statusCode { get; set; }

    public object result { get; set; }

    public object error { get; set; }

    public object Result(int statusCode, object result)
    {
      return new
      {
        statusCode = statusCode,
        result = new
        {
          result
        }
      };
    }

    public object Error(int statusCode, object error)
    {
      return new
      {
        statusCode = statusCode,
        error
      };
    }

  }
}
