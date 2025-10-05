using Desafio_BackEnd.Application.Mappings;

namespace Desafio_BackEnd.Api.Configurations;

public static class AutoMapperSetup
{
    public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));
        return services;
    }
}
