using HookStream.API.Middlewares;

namespace HookStream.API.Installers;

public static class WebSocketInstaller
{
    public static void UseWebSocketManager(this WebApplication app)
    {
        app.UseWebSockets();
        app.UseMiddleware<WebSocketMiddleware>();
    }
}
