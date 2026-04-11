using Infrastructure.Persistence.Contexts.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.Repositories;

public static class RepositoryRegistrationExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddDbContexts(configuration, env);
        //services.AddRepositories(configuration, env);

        return services;
    }
}
