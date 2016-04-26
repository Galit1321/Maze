using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.view;

namespace ServerExe1.src.Presentor
{
    /// <summary>
    /// interface for the commands
    /// </summary>
    interface ICommandable
    {
        /// <summary>
        /// do the commands 
        /// </summary>
        /// <param name="args">the args according to the input</param>
        /// <param name="sender">who send it</param>
        void Execute(List<string> args, ISendableView sender);
    }
}
