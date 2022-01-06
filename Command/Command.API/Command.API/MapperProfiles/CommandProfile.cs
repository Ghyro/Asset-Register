using AutoMapper;
using System;


namespace Platform.API.MapperProfiles
{
  using Command.API.Infrastructure.Dtos;
  using Command.API.Infrastructure.Models;

  public class CommandProfile : Profile
  {
    public CommandProfile()
    {
      CreateMap<CommandModelCreateDto, CommandModelReadDto>()
          .ForMember(d => d.Id, opts => opts.Ignore());
      CreateMap<CommandModel, CommandModelReadDto>().ReverseMap();

      CreateMap<CommandModelCreateDto, CommandModel>()
          .ForMember(d => d.Id, opts => opts.Ignore())
          .ForMember(d => d.CreatedAt, opts => opts.MapFrom(s => DateTime.UtcNow))
          .ForMember(d => d.ModifiedAt, opts => opts.MapFrom(s => DateTime.UtcNow));
      CreateMap<CommandModelCreateDto, CommandModel>()
          .ForMember(d => d.Id, opts => opts.Ignore())
          .ForMember(d => d.CreatedAt, opts => opts.MapFrom(s => DateTime.UtcNow))
          .ForMember(d => d.ModifiedAt, opts => opts.MapFrom(s => DateTime.UtcNow));
    }
  }
}
