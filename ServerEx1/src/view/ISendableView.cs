using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.model;

namespace ServerExe1.src.view
{
    /// <summary>
    /// sendble
    /// </summary>
    interface ISendableView
    {
        /// <summary>
        /// send the msg to the
        /// </summary>
        /// <param name="msg">the msg</param>
        void SendMessage(string msg);
        
        /// <summary>
        /// check if equals
        /// </summary>
        /// <param name="obj">the obj to check</param>
        /// <returns>true if equals false otherwise</returns>
        bool Equals(object obj);
    }
}
