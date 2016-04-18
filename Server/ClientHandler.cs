using Server.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    /// <summary>
    /// Handle a specific client
    /// </summary>
    public class ClientHandler
    {
        public event change clientRecive;
        private Socket client;
        public int id { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="sock"> socket </param>
        /// <param name="id"> id </param>
        public ClientHandler(Socket sock, int id)
        {
            this.client = sock;
            this.id = id;
        }

        /// <summary>
        /// handle the client
        /// </summary>
        public void handle()
        {
            while (true) {
                byte[] data = new byte[1024];
                int recv = this.client.Receive(data);
                if (recv == 0) {
                    break;
                }
                clientRecive(Encoding.ASCII.GetString(data, 0, recv));
            }
            client.Close();
        }

        /// <summary>
        /// send a given string
        /// </summary>
        /// <param name="str"> the string to send </param>
        public void Send(string str)
        {
            byte[] toSend = System.Text.Encoding.ASCII.GetBytes(str);
            client.Send(toSend);
        }
    }
}
