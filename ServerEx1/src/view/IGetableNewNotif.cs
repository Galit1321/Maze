using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.view
{
    /// <summary>
    /// Add listen
    /// </summary>
    /// <param name="who"></param>
    /// <param name="args"></param>
    delegate void AddNewToListen(IGetableNewNotif who, EventArgs args);
    
    /// <summary>
    /// Interface for new notifier
    /// </summary>
    interface IGetableNewNotif
    {
        /// <summary>
        /// the event
        /// </summary>
        event AddNewToListen newListen;

        /// <summary>
        /// get to the new event class for the command event
        /// </summary>
        /// <param name="getable"></param>
        void GetNewNotif(out IGetableCommand getable);
    }
}
