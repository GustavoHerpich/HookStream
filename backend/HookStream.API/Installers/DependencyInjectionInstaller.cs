using HookStream.API.Services.Impl;
using HookStream.API.WebSockets;

namespace HookStream.API.Installers;

public static class DependencyInjectionInstaller
{
    public static void InstallServices(this IServiceCollection services)
    {
        services.AddSingleton<WebSocketConnectionManager>();
        services.AddSingleton<BroadcastService>();
    }
}
