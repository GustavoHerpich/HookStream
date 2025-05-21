using HookStream.API.Services.Impl;
using HookStream.API.Utils.Converters;
using HookStream.API.Utils.SignatureValidator;
using HookStream.API.Utils.SignatureValidator.Impl;
using HookStream.API.WebSockets;

namespace HookStream.API.Installers;

public static class DependencyInjectionInstaller
{
    public static void InstallServices(this IServiceCollection services)
    {
        services.AddScoped<IBroadcastService, BroadcastService>();
        services.AddSingleton<WebSocketConnectionManager>();
        services.AddSingleton<ISignatureValidator, HmacSha256SignatureValidator>();
        services.AddSingleton<GitHubPayloadParser>();
    }
}
