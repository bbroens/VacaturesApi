using AutoMapper;
using VacaturesApi.Domain;

namespace VacaturesApi.Features.Vacatures;

public class VacatureMappingProfile : Profile
{
    public VacatureMappingProfile()
    {
        CreateMap<Vacature, VacatureDto>();
    }
}