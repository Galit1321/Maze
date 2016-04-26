using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.view
{
    /// <summary>
    /// handle output
    /// </summary>
    /// <typeparam name="T">the output type</typeparam>
    interface IHandleOutput<T>
    {
        /// <summary>
        /// handle the output and send it
        /// </summary>
        /// <param name="output">the output</param>
        /// <param name="send">where to send</param>
        void HandleOutput(T output, ISendableView send);
    }
}
