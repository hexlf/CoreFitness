using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Contexts.Extensions;

namespace Infrastructure.Persistence;

public static class PersistenceRegistrationExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddDbContexts(configuration, env);
        services.AddRepositories(configuration, env);

        return services;
    }
}
