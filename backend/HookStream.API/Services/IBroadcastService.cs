namespace HookStream.API.Services.Impl;

public interface IBroadcastService
{
    Task BroadcastAsync(string message);
}