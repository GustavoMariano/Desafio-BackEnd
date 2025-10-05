using Desafio_BackEnd.Application.Services;
using Desafio_BackEnd.Domain.Interfaces;
using Desafio_BackEnd.Infrastructure.Messaging;
using Desafio_BackEnd.Infrastructure.Persistence;
using Desafio_BackEnd.Infrastructure.Persistence.Repositories;
using Desafio_BackEnd.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace Desafio_BackEnd.Api.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<ICourierRepository, CourierRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();

        services.AddScoped<MotorcycleService>();
        services.AddScoped<CourierService>();
        services.AddScoped<RentalService>();

        services.AddSingleton<IMessagePublisher>(sp =>
            new RabbitMqMessagePublisher(
                hostname: config["RabbitMQ:Host"] ?? "localhost",
                username: config["RabbitMQ:User"] ?? "guest",
                password: config["RabbitMQ:Password"] ?? "guest"));

        services.AddSingleton<IStorageService>(sp =>
            new LocalStorageService("Uploads/CnhImages"));

        return services;
    }
}
