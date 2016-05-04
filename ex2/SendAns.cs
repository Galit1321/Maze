using System.Net.Sockets;
using System.Text;

namespace ex2
{
    public class SendAns
    {
        
        private Socket Sock;
        private bool NeedUpdate;
       volatile bool StopSend;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sock">socket to send msn to</param>
        public SendAns(Socket sock)
        {
           Sock = sock;
        }
        /// <summary>
        /// answer prop 
        /// </summary>
        private string answer;
        public string Answer
        {
            set
            {
                answer = value;
                NeedUpdate = true;
            }
        }

        /// <summary>
        /// change flag to thread 
        /// for it to close
        /// </summary>
        public void End()
        {
            StopSend = true;
        }


        public void DoWork()
        {
            StopSend = false;
            while (!StopSend)//whlie flag is false
            {
                if (NeedUpdate)//Thread safe to 
                {
                    Sock.Send(Encoding.ASCII.GetBytes(answer));
                    NeedUpdate = false;
                }
            }
        }
    }
}
   