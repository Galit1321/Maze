using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.view;

namespace ServerExe1.src.Presentor
{
    /// <summary>
    /// the handler 
    /// </summary>
    /// <typeparam name="T">the type of the handelr, what it will handle</typeparam>
    interface IHandlerUpdate
    {
        /// <summary>
        /// update according the T and send
        /// </summary>
        /// <param name="theUpdate">the update</param>
        /// <param name="view">to who to send</param>
        void UpdateView(object theUpdate, ISendableView view);
    }
}
