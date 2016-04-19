using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// creating a client and 
    /// creating a thread of sending and
    /// reciving msg
    /// to and from the server
    /// </summary>
    class Program
    {
        public enum direction { up, down, left, right };
        static void Main(string[] args)
        {
            string msn = "";
            TCPClient Tcp = new TCPClient();
            Thread t = new Thread(Tcp.SendMsg);
            t.Start();
           while (!(msn.Equals("close"))) {
                msn = Tcp.ReceviveMsg();
                if (msn != "wait" )
                    Console.Write(msn);    
            }
            Tcp.run = false;//closing the tread for sendMsg
            
        }
    }
}
