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
        /// <summary>
        ///  deconstructor
        /// </summary>

        ~ReceiveAns()
        {
            StopRec = true;
        }
        /// <summary>
        /// what we recived from server
        /// </summary>
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                UpdateAnswer();//update the we got new answer
            }
        } 
        /// <summary>
        /// function to do work in a diff
        /// thread from thread pool
        /// </summary>
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
                    if (StopRec)//if a diff thread close connection while we waiting from data 
                    {
                        return;
                    }else
                    {
                        throw socketEx;
                    }
                }
                
            }
        }
        /// <summary>
        /// end connection by closing 
        /// the while in doWork 
        /// </summary>
        public void End()
        {
            StopRec = true;
        }
    }
}
    