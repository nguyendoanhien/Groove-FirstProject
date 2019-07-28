using System.Collections.Generic;
using System.Linq;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public class HubConnectionStorage
    {
        private readonly Dictionary<string, HashSet<string>> _connections =
            new Dictionary<string, HashSet<string>>();

        public int Count => _connections.Count;

        public void Add(string topic, string key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue($"{topic}_{key}", out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add($"{topic}_{key}", connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(string topic, IEnumerable<string> keys)
        {
            var result = new List<string>();
            foreach (var key in keys)
                if (_connections.TryGetValue($"{topic}_{key}", out var connections))
                    result.AddRange(connections);
            return result;
        }

        public IEnumerable<string> GetConnections(string topic, string key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue($"{topic}_{key}", out connections)) return connections;

            return Enumerable.Empty<string>();
        }

        public void Remove(string topic, string key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue($"{topic}_{key}", out connections)) return;

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0) _connections.Remove($"{topic}_{key}");
                }
            }
        }
    }
}