using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.view
{
    /// <summary>
    /// the main view
    /// </summary>
    interface IMainView : IGetableNewNotif
    {
        /// <summary>
        /// start get the commands
        /// </summary>
        void StartGetCommands();
    }
}
