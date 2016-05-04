using System;
using System.Net;
using System.Net.Sockets;
using System.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ex2
{
    
    /// <summary>
    /// client with TCP way of communication
    /// </summary>
    class TCPClient : ICommuntable
    {
       
        Socket Sock;
        ReceiveAns Receive;
        SendAns Send;
        List<Task> MyTasks;
        public event UpdateData UpdateModel;

        public TCPClient()
        {

        }
        ~TCPClient()
        {
            
            if (Sock != null)
            {
                Sock.Shutdown(SocketShutdown.Both);
                Sock.Close();
            }
        }

        /// <summary>
        /// Connecting to server 
        /// </summary>
        /// <param name="IP">ip of the server</param>
        /// <param name="port">port number of the server</param>
        public bool Connect(string ip, int port)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), port);
            this.Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Sock.Connect(ipep);
            }
            catch (SocketException e)
            {
                //didn't make connection so we need to show lostConnection Win
                return false;
            }

            Receive = new ReceiveAns(Sock);
            Receive.UpdateAnswer+= delegate ()
            {
                UpdateModel();
            };
            Send = new SendAns(Sock);

            return true;
        }

        public void Start()
        {

            List<Task> MyTasks = new List<Task>();
            MyTasks.Add(Task.Factory.StartNew(Send.DoWork));
            MyTasks.Add(Task.Factory.StartNew(Receive.DoWork));
            Task.WaitAll(MyTasks.ToArray());
        }

       public void SendMsg(string ans)
        {
            Send.Answer = ans;
        }

        public string ReceiveData()
        {
            return this.Receive.Answer;
        }

        public void Disconnect()
        {
            Send.End();//close thread
            Receive.End();//close thread
            Thread.Sleep(1000); //wait for thread to close
            if (Sock != null)
            {
                Sock.Shutdown(SocketShutdown.Both);
                Sock.Close();
            }
        }
    }
}
