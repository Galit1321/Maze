using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.Presentor;

namespace ServerExe1.src.view
{
    /// <summary>
    /// handle client input
    /// </summary>
    class HandleClient: ISendableView , IGetableCommand
    {

        public event NewResv theResv;

        private Socket client;
        private static int idCount = 0;
        private int myId;

        private string lastResv;

        /// <summary>
        /// c'tor of the handle
        /// </summary>
        /// <param name="clientSocket">the socket of the client</param>
        /// <param name="handle">the handle of the commant controller</param>
        public HandleClient(Socket clientSocket)
        {
            myId = idCount;
            idCount++;
            this.client = clientSocket;
        }

        /// <summary>
        /// start resv commands
        /// </summary>
        public void StartResv()
        {
            try
            {
                while (true)
                {
                    //resv
                    byte[] data = new byte[1024];
                    int recv = client.Receive(data);
                    if (recv == 0) break;
                    string str = Encoding.ASCII.GetString(data, 0, recv);
                    str = str.Remove(str.IndexOf("\r\n"), "\r\n".Length);
                    this.lastResv = str;
                    this.theResv(this, EventArgs.Empty);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("client exit" + e.ToString());
            }
        }
        
        /// <summary>
        /// send the msg to the client
        /// </summary>
        /// <param name="msg">the msg to send</param>
        public void SendMessage(string msg)
        {
            byte[] s = Encoding.ASCII.GetBytes(msg.ToCharArray());
            this.client.Send(s);
        }

        /// <summary>
        /// check if the sender is belong to the same clients
        /// </summary>
        /// <param name="obj">the other client</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            HandleClient h = obj as HandleClient;
            if (h.myId == this.myId)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// hash code
        /// </summary>
        /// <returns>hashcode</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        /// <summary>
        /// get the command
        /// </summary>
        /// <param name="command">the command </param>
        /// <param name="whoSend">me</param>
        public void GetCommand(out string command, out ISendableView whoSend)
        {
            command = this.lastResv;
            whoSend = this as ISendableView;
        }
    }
}
