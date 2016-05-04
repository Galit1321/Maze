using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    public delegate void UpdateData();
    /// <summary>
    /// interface of the methods of
    /// communication that are separate 
    /// to allow to send message even if we didnt 
    /// recived a anwser to prev send
    /// </summary>
    interface ICommuntable
    {
        event UpdateData UpdateModel;
        bool Connect(string IP, int port);
        void Start();
        void SendMsg(string ans);
        string ReceiveData();
        void Disconnect();

    }
}
