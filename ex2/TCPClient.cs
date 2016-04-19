using System;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Text;
using System.Threading;

namespace View
{
    /// <summary>
    /// client with TCP way of communication
    /// </summary>
    class TCPClient : ICommuntable
    {
        private Socket Sock;
        public bool run { get; set; }
        public TCPClient()
        {
            run = true;
            
            
            
        }
        /// <summary>
        /// deconstructor of class
        /// </summary>
        ~TCPClient()
        {
            run = false;
            Sock.Shutdown(SocketShutdown.Both);
            Sock.Close();
        }
        public void Connect(string ip,int port)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Sock.Connect(ipep);
            
        }
        /// <summary>
        /// recived a message from the server 
        /// </summary>
        /// <returns>message from server</returns>
        public string ReceviveMsg()
        {

            byte[] data = new byte[1024];
            int recv = Sock.Receive(data);
            return Encoding.ASCII.GetString(data, 0, recv);
        }
        /// <summary>
        /// while run is true meaning that
        /// we didn't get "close" from the server
        /// meanig that we send a close of a game we play
        /// </summary>
        public void SendMsg(string msn)
        {
            while (run)
            {
                
                try
                {
                    Sock.Send(Encoding.ASCII.GetBytes(msn));
                }
                catch
                {

                }
            }
            
        }

        public void SendMsg()
        {
            throw new NotImplementedException();
        }
    }
}
