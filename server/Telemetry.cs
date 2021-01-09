using Microsoft.AspNetCore.SignalR;
using server.Shared;
using System;
using System.Threading.Tasks;

namespace server
{
    internal class Telemetry : Hub
    {
        /// <summary>
        /// Sends a single triple to one client or a group of clients.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public Task SendTriple(Triple t, string group = null)
        {
            if (string.IsNullOrEmpty(group))
            {
                return Clients.Caller.SendAsync("ReceiveTriple", t);
            }
            else
            {
                return Clients.Group(group).SendAsync("ReceiveTriple", t);
            }
        }

        public Task SendTime(){
            return Clients.Caller.SendAsync("setTime", DateTime.Now.ToShortDateString());
        }

        public void SendConnectionId(string connectionId)
        {
            SendTime();
        }
    }
}