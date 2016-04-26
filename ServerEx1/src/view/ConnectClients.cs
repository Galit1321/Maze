using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.Presentor;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using ServerExe1;

namespace ServerExe1.src.view
{
    /// <summary>
    /// main view connect to the clients
    /// </summary>
    class ConnectClients : IMainView 
    {
        public event AddNewToListen newListen;
        private HandleClient clientHandle;

        /// <summary>
        /// c'tor and get the controller
        /// </summary>
        /// <param name="controller">the controller to handle the commands</param>
        public ConnectClients()
        {
        }

        /// <summary>
        /// start run the socket accept and put them in new thread
        /// </summary>
        public void StartGetCommands()
        {
            //create the socket
            String port  = ConfigurationManager.AppSettings["Port"];            
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, Int32.Parse(port));
            Socket newsock = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            while (true)
            {
                //new client connection
                Socket client =newsock.Accept();
                HandleClient handle = new HandleClient(client);
                this.clientHandle = handle;
                Task.Factory.StartNew(handle.StartResv);
                this.newListen(this, EventArgs.Empty);
            }
        }


        /// <summary>
        /// get who to notupy
        /// </summary>
        /// <param name="getable">getable</param>
        public void GetNewNotif(out IGetableCommand getable)
        {
            getable = this.clientHandle as IGetableCommand;
        }
    }
}
