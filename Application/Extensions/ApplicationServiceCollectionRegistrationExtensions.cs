using Application.Services;
using Application.UseCases.DeleteUser;
using Application.UseCases.RegisterUser;
using Application.UseCases.SignIn;
using Application.UseCases.UpdateUserProfile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Extensions;

public static class ApplicationServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<RegisterUserHandler>();
        services.AddScoped<SignInHandler>();
        services.AddScoped<UpdateUserProfileHandler>();
        services.AddScoped<DeleteUserHandler>();

        return services;
    }

}
