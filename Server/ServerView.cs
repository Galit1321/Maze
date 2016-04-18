using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    /// <summary>
    ///  the view, connection with client
    /// </summary>
    /// <param name="massege"></param>
    public delegate void change(string massege);
    public class ServerView : IView
    {
        public event Modle.updateView ViewChanged;
        private Dictionary<int, ClientHandler> clients;

        /// <summary>
        /// constructor
        /// </summary>
        public ServerView()
        {
            this.clients = new Dictionary<int, ClientHandler>();
        }

        /// <summary>
        /// connection to client
        /// </summary>
        /// <param name="socket"> socket to connect to </param>
        public void ConnectClient(Socket socket) {
             while (true)
            {
                Socket client = socket.Accept();
                int id = GetId();
                ClientHandler handler = new ClientHandler(client, id);
                handler.clientRecive += delegate(string massege)
                {
                    ViewChanged(handler.id, massege);
                };
                this.clients.Add(id, handler);
                Task.Factory.StartNew(handler.handle);

            }
        }

        /// <summary>
        /// Get a random Id
        /// </summary>
        /// <returns> returns the id </returns>
        private int GetId()
        {
            Random rand = new Random();
            int id = rand.Next(400);
            while (clients.ContainsKey(id))
            {
                id = rand.Next(400);
            }
            return id;

        }

        /// <summary>
        /// Display data
        /// </summary>
        /// <param name="id"> the id </param>
        /// <param name="output"> the output </param>
        public void DisplayData(int id , string output)
        {
            ClientHandler client = clients[id];
            client.Send(output);
        }
    }
}
