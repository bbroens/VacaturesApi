using AutoMapper;
using VacaturesApi.Domain;
using VacaturesApi.Features.Vacatures.Update;

namespace VacaturesApi.Features.Vacatures;

public class VacatureMappingProfile : Profile
{
    public VacatureMappingProfile()
    {
        // Mapping for fetching vacatures
        CreateMap<Vacature, VacatureDto>();
        
        // Mapping for (partial) updates
        CreateMap<UpdateVacatureDto, Vacature>()
            .ForAllMembers(opts => 
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}