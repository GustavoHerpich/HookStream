using System.Text;
using System.Net.WebSockets;
using HookStream.API.WebSockets;

namespace HookStream.API.Services.Impl;
public class BroadcastService(WebSocketConnectionManager _manager)
{
    public async Task BroadcastAsync(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        var segment = new ArraySegment<byte>(buffer);

        foreach (var socket in _manager.GetAll())
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
