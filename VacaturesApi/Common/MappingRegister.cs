using Mapster;
using VacaturesApi.Domain;
using VacaturesApi.Features.Vacatures;
using VacaturesApi.Features.Vacatures.Update;

namespace VacaturesApi.Common;

/// <summary>
/// Mapster register used to map Entities to Dtos.
/// </summary>

public class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Vacature, VacatureDto>();
        config.NewConfig<UpdateVacatureDto, Vacature>()
            .IgnoreNullValues(true); // PUT passes null for unchanged values
    }
}