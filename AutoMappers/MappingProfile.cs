using AutoMapper;

namespace MyApp.Namespace
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BeerInsertDto, Beer>();
            CreateMap<Beer, BeerDto>()
                .ForMember(dto => dto.Id,
                    m => m.MapFrom(b => b.BeerID));
            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}