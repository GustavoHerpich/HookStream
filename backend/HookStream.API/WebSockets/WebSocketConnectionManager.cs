using System.Net.WebSockets;
using System.Collections.Concurrent;

namespace HookStream.API.WebSockets;

public class WebSocketConnectionManager
{
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

    public void AddSocket(string id, WebSocket socket)
    {
        if (!_sockets.TryAdd(id, socket))
        {
            throw new InvalidOperationException($"Socket ID {id} já está registrado.");
        }
    }

    public void RemoveSocket(string id)
    {
        _sockets.TryRemove(id, out _);
    }

    public IEnumerable<WebSocket> GetAll() => _sockets.Values;
}
