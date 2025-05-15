using HookStream.API.Configurations;

namespace HookStream.API.Installers;
public static class ServiceInstaller
{
    public static void AddServiceConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.InstallCors();
        services.InstallServices();

        services.Configure<GitHubOptions>(configuration.GetSection("GitHub"));

        services.AddControllers();
    }
}