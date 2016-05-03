using System.Net.Sockets;
using System.Text;

namespace ex2
{
    public class TCPSendHandler
    {
        // <summary>
        /// socket of the server
        /// </summary>
        private Socket server;
        /// <summary>
        /// the string to send to server
        /// </summary>
        private string commandToSend;
        /// <summary>
        /// ****************************
        /// </summary>
        private bool changed;
        /// <summary>
        /// ******************************
        /// </summary>
        private bool stopFlag;


        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="sock">the socket of the server</param>
        public TCPSendHandler(Socket sock)
        {
            server = sock;
        }

        /// <summary>
        /// send masseges to the server, when "exit" massege sent we close the connection.
        /// </summary>
        public void handle()
        {
            stopFlag = false;
            while (!stopFlag)
            {
                if (changed)
                {
                    server.Send(Encoding.ASCII.GetBytes(commandToSend));
                    changed = false;
                }
            }
        }

        public string CommandToSend
        {
            set
            {
                commandToSend = value;
                changed = true;
            }
        }

        public void stop()
        {
            stopFlag = true;
        }
    }
}
   