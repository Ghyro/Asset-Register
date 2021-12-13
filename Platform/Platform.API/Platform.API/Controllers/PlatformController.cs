using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Platform.API.Controllers
{
    using Platform.API.Infrastructure;
    using Platform.API.Infrastructure.Dtos;
    using Platform.API.Infrastructure.Interfaces;

    [Route("api/[controller]")]
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
            var entities = await _repository.GetAll();
            return new ActionResult<IEnumerable<PlatformModelReadDto>>(_mapper.Map<IEnumerable<PlatformModelReadDto>>(entities));
        }

        [HttpGet]
        [Route("{id}", Name = "GetPlatform")]
        public async Task<ActionResult<PlatformModelReadDto>> GetPlatform(int id)
        {
            var entity = await _repository.GetById(id);
            return new ActionResult<PlatformModelReadDto>(_mapper.Map<PlatformModelReadDto>(entity));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformModelReadDto>> CreatePlatform(PlatformModelCreateDto platformModelCreateDto)
        {
            if (platformModelCreateDto == null)
            {
                return BadRequest();
            }

            // createdat and modifiedat

            var newEntityId = await _repository.CreateAsync(_mapper.Map<PlatformModel>(platformModelCreateDto));
            return CreatedAtRoute(nameof(GetPlatform), new { id = newEntityId }, platformModelCreateDto);
        }

        [HttpPut]
        public async Task<ActionResult<PlatformModelReadDto>> UpdatePlatform(PlatformModelUpdateDto platformModelUpdateDto)
        {
            var entity = await _repository.GetById(platformModelUpdateDto.Id);

            if (entity == null)
            {
                return NotFound();
            }

            //modifiedat

            await _repository.Update(_mapper.Map<PlatformModel>(platformModelUpdateDto));
            return CreatedAtRoute(nameof(GetPlatform), new { id = platformModelUpdateDto.Id }, platformModelUpdateDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<PlatformModelReadDto>> DeletePlatform(int id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null)
            {
                return NotFound();
            }

            await _repository.Remove(id);

            return Ok();
        }
    }
}
