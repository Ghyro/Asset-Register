using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Command.API.Controllers
{
  using Command.API.Infrastructure.Interfaces;

  [Route("api/c/[controller]")]
  [ApiController]
  public class PlatformController : ControllerBase
  {
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public PlatformController(IRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }       

    [HttpPost]
    public ActionResult TestConnection()
    {
      Console.WriteLine("It works POST Command API");

      return Ok("OK, it works POST Command API on Platform Controller");
    }
  }
}
