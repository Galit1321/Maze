using System.Net.Sockets;
using System.Text;

namespace ex2
{
    public class ReceiveAns
    {
        public event UpdateData UpdateAnswer;
        private Socket Sock;
        volatile bool StopRec;
        private string answer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sock"></param>
        public ReceiveAns(Socket sock)
        {
            this.Sock = sock;
            StopRec = false;
        }
        ~ReceiveAns()
        {
            StopRec = true;
        }
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                UpdateAnswer();
            }
        } 

        public void DoWork()
        {
           
            while (!StopRec)//while we want the thread to work
            {
                try
                {
                    byte[] data = new byte[5000];
                    int recv = Sock.Receive(data);
                    this.Answer = Encoding.ASCII.GetString(data, 0, recv);
                }
                catch (SocketException socketEx)
                {
                    if (StopRec)
                    {
                        return;
                    }else
                    {
                        throw socketEx;
                    }
                }
                
            }
        }
        public void End()
        {
            StopRec = true;
        }
    }
}
    