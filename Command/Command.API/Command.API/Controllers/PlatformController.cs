using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Command.API.Controllers
{
  [Route("api/c/[controller]")]
  [ApiController]
  public class PlatformController : ControllerBase
  {
    public PlatformController()
    {

    }

    [HttpPost]
    public ActionResult TestConnection()
    {
      Console.WriteLine("It works POST Command API");

      return Ok("OK, it works POST Command API on Platform Controller");
    }
  }
}
