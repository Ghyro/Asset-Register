using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Command.API.Controllers
{
  using Command.API.Infrastructure.Dtos;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformModelReadDto>>> GetPlatforms()
    {
      var platformItems = await _repository.GetAllPlatforms();

      return new ActionResult<IEnumerable<PlatformModelReadDto>>(_mapper.Map<IEnumerable<PlatformModelReadDto>>(platformItems));
    }

    [HttpPost]
    public ActionResult TestConnection()
    {
      Console.WriteLine("It works POST Command API");

      return Ok("OK, it works POST Command API on Platform Controller");
    }
  }
}
