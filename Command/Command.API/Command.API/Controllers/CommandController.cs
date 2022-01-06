using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Command.API.Controllers
{
  using Command.API.Infrastructure.Dtos;
  using Command.API.Infrastructure.Interfaces;
  using Command.API.Infrastructure.Models;

  [Route("api/c/platforms/{platformId}/[controller]")]
  [ApiController]
  public class CommandController : ControllerBase
  {
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public CommandController(IRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommandModelReadDto>>> GetCommandsForPlatform(int platformId)
    {
      var entity = await _repository.GetAllCommandsForPlatform(platformId);
      return new ActionResult<IEnumerable<CommandModelReadDto>>(_mapper.Map<IEnumerable<CommandModelReadDto>>(entity));
    }

    [HttpGet]
    [Route("{commandId}", Name = "GetCommandForPlatform")]
    public async Task<ActionResult<CommandModelReadDto>> GetCommandForPlatform(int platformId, int commandId)
    {
      var entity = await _repository.GetCommandForPlatform(platformId, commandId);
      return new ActionResult<CommandModelReadDto>(_mapper.Map<CommandModelReadDto>(entity));
    }

    [HttpPost]
    [Route("{platformId}", Name = "CreateCommand")]
    public async Task<ActionResult<CommandModelReadDto>> CreateCommand(int platformId, CommandModelCreateDto commandModelReadDto)
    {
      if (commandModelReadDto == null)
      {
        return BadRequest();
      }

      var newEntityId = await _repository.CreateCommand(platformId, _mapper.Map<CommandModel>(commandModelReadDto));

      var platformReadDto = _mapper.Map<CommandModelReadDto>(commandModelReadDto, opts => opts.AfterMap((src, dest) => dest.Id = 1));

      return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = newEntityId }, commandModelReadDto);
    }
  }
}
