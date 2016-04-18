using Server.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.ViewS
{
    /// <summary>
    /// connection details and init
    /// </summary>
    class Connect
    {
        private string port;

        /// <summary>
        /// constructor
        /// </summary>
        public Connect()
        {
            this.port = ConfigurationManager.AppSettings["Port"];
        }

        /// <summary>
        /// initialize the connection
        /// </summary>
        /// <returns> returns the socket to connect </returns>
        public Socket InitConnect()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, Int32.Parse(this.port));
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            return newsock;
        }
    }
}
