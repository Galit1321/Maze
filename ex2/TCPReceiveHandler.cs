using System.Net.Sockets;
using System.Text;

namespace ex2
{
    public class TCPReceiveHandler
    {
        /// <summary>
        /// socket of the server
        /// </summary>
        private Socket server;
        /// <summary>
        /// ******************************
        /// </summary>
        private bool stopFlag;
        /// <summary>
        /// ***************************
        /// </summary>
       // public event voidFunc recievedFromServer;
        /// <summary>
        /// *********************
        /// </summary>
        private string stringFromServer;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="sock">the socket of the server</param>
        public TCPReceiveHandler(Socket sock)
        {
            server = sock;
        }

        /// <summary>
        /// receive masseges from the server
        /// </summary>
        public void handle()
        {
            stopFlag = false;
            while (!stopFlag)
            {
                byte[] data = new byte[65536];
                int recv = server.Receive(data);
                stringFromServer = Encoding.ASCII.GetString(data, 0, recv);
                //recievedFromServer();
            }
        }

        public void stop()
        {
            stopFlag = true;
        }

        public string StringFromServer
        {
            get
            {
                return stringFromServer;
            }
        }

    }
}
    