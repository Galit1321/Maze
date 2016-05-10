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
        public event UpdateData LostConn;
        SendAns Send;
        List<Task> MyTasks;
        public event UpdateData UpdateModel;

        public TCPClient()
        {

        }
        ~TCPClient()
        {
            
         
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
                LostConn();
                return false;
            }

            Receive = new ReceiveAns(Sock);
            Receive.UpdateAnswer+= delegate ()
            {
                UpdateModel();
            };
            Receive.FailToRead += delegate ()
             {
                 LostConn();
             };
            Send = new SendAns(Sock);

            return true;
        }
    /// <summary>
    /// start commution with server
    /// </summary>
        public void Start()
        {
           MyTasks = new List<Task>();
            MyTasks.Add(Task.Factory.StartNew(Send.DoWork));
            MyTasks.Add(Task.Factory.StartNew(Receive.DoWork));
            Task.WaitAll(MyTasks.ToArray());
        }
        /// <summary>
        /// send msg to server
        /// </summary>
        /// <param name="ans">what we want to send</param>
       public void SendMsg(string ans)
        {
            Send.Answer = ans;
        }
        /// <summary>
        /// receuve data/response from client
        /// and set it as the answer in this.recevier
        /// </summary>
        /// <returns></returns>
        public string ReceiveData()
        {
            return this.Receive.Answer;
        }
        /// <summary>
        /// end connection withe server 
        /// and close thread
        /// </summary>
        public void Disconnect()
        {
            Send.End();//close thread
            Receive.End();//close thread
            if (Sock != null&& (Sock.Connected))
            {
                Sock.Shutdown(SocketShutdown.Both);
                Sock.Close();
            }
        }
    }
}
