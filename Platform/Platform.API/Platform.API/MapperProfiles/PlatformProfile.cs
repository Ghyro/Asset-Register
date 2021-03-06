using AutoMapper;
using System;


namespace Platform.API.MapperProfiles
{
    using Platform.API.Infrastructure;
    using Platform.API.Infrastructure.Dtos;    

    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<PlatformModelCreateDto, PlatformModelReadDto>()
                .ForMember(d => d.Id, opts => opts.Ignore());
            CreateMap<PlatformModel, PlatformModelReadDto>().ReverseMap();

            CreateMap<PlatformModelCreateDto, PlatformModel>()
                .ForMember(d => d.Id, opts => opts.Ignore())
                .ForMember(d => d.CreatedAt, opts => opts.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.ModifiedAt, opts => opts.MapFrom(s => DateTime.UtcNow));

            CreateMap<PlatformModelUpdateDto, PlatformModel>()
               .ForMember(d => d.CreatedAt, opts => opts.Ignore())
               .ForMember(d => d.ModifiedAt, opts => opts.MapFrom(s => DateTime.UtcNow));

            CreateMap<PlatformModelReadDto, PlatformPublishedDto>()
               .ForMember(d => d.Event, opts => opts.MapFrom(s => "Platform_Published"));
        }
    }
}
