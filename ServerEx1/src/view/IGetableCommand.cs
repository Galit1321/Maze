using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.view
{
    /// <summary>
    /// delegete for resving
    /// </summary>
    /// <param name="who">get the commands event </param>
    /// <param name="args">the args</param>
    delegate void NewResv(IGetableCommand who, EventArgs args);
    interface IGetableCommand
    {

        /// <summary>
        /// the event
        /// </summary>
        event NewResv theResv;

        /// <summary>
        /// get the next command
        /// </summary>
        /// <param name="command">command</param>
        /// <param name="whoSend">the sender</param>
        void GetCommand(out string command, out ISendableView whoSend);
    }
}
