using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using System.Web.Caching;

namespace AdminLTE
{
    public class SignalRHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        public void SendNotification(string userName, string message, string icon)
        {
            if (!Context.User.IsInRole("Admin"))
                return;

            string name = Context.User.Identity.Name;

            foreach (var connectionId in _connections.GetConnections(userName))
            {
                Clients.Client(connectionId).pushNotification(name, message, icon);
            }
        }

        public void SendAllNotification(string message, string icon)
        {
            if (!Context.User.IsInRole("Admin"))
                return;

            Clients.All.pushNotification(message, icon);
        }

        public void SendMessage(string userName, string message)
        {
            if (!Context.User.IsInRole("Admin"))
                return;

            string name = Context.User.Identity.Name;

            foreach (var connectionId in _connections.GetConnections(userName))
            {
                Clients.Client(connectionId).pushMessage(name, message);
            }
        }

        public void SendAllMessage(string message)
        {
            if (!Context.User.IsInRole("Admin"))
                return;

            Clients.All.pushMessage(message);
        }

        public void SendClearTranslateCache()
        {
            if (!Context.User.IsInRole("Admin"))
                return;

            Clients.All.clearTranslateCache();
        }

        public void LogoffUser(string userName)
        {
            if (!Context.User.IsInRole("Admin"))
                return;

            foreach (var connectionId in _connections.GetConnections(userName))
            {
                Clients.Client(connectionId).logOff();
            }
        }

        public override Task OnConnected()
        {
            try
            {
                string name = Context.User.Identity.Name;

                _connections.Add(name, Context.ConnectionId);
            }
            catch (Exception)
            {

            }

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            try
            {
                string name = Context.User.Identity.Name;

                _connections.Remove(name, Context.ConnectionId);
            }
            catch (Exception)
            {

            }


            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            try
            {
                string name = Context.User.Identity.Name;

                if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
                {
                    _connections.Add(name, Context.ConnectionId);
                }
            }
            catch (Exception)
            {

            }

            return base.OnReconnected();
        }
    }

    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}