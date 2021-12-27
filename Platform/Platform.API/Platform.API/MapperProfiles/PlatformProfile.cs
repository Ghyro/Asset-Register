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

            //TODO: Should be removed after database is introduced
            CreateMap<PlatformModelCreateDto, PlatformModel>()
              .ConstructUsing(x => new PlatformModel(new Random().Next(0, 100), x.Title, x.Publisher, x.Cost));
            //CreateMap<PlatformModelCreateDto, PlatformModel>()
            //    .ForMember(d => d.Id, opts => opts.Ignore())
            //    .ForMember(d => d.CreatedAt, opts => opts.MapFrom(s => DateTime.UtcNow))
            //    .ForMember(d => d.ModifiedAt, opts => opts.MapFrom(s => DateTime.UtcNow));

            CreateMap<PlatformModelUpdateDto, PlatformModel>()
               .ForMember(d => d.CreatedAt, opts => opts.Ignore())
               .ForMember(d => d.ModifiedAt, opts => opts.MapFrom(s => DateTime.UtcNow));
        }
    }
}
