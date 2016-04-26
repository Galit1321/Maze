using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.view
{
    /// <summary>
    /// the general output handle of the all commands
    /// </summary>
    /// <typeparam name="T">the type of the output</typeparam>
    abstract class GeneralHandleOutput<T> : IHandleOutput<T>
    {
        protected IConvertableView convert;
        /// <summary>
        /// the c'tor
        /// </summary>
        /// <param name="convert">the convert to the output</param>
        public GeneralHandleOutput(IConvertableView convert)
        {
            this.convert = convert;
        }

        /// <summary>
        /// handle the output usign the abstract method
        /// </summary>
        /// <param name="output">the output that need to send</param>
        /// <param name="send">where send to</param>
        public void HandleOutput(T output, ISendableView send)
        {
            //convert
            string toSend = new ConvertJsonFormat().ConvertOutput(
                this.GetNumberCommandHandle(),this.Handle(output));
            //send
            send.SendMessage(toSend);
        }
        
        /// <summary>
        /// handle the output
        /// </summary>
        /// <param name="msg">the output</param>
        /// <returns>the string after the msg is handled</returns>
        protected abstract string Handle(T msg);

        /// <summary>
        /// get the num of the command
        /// </summary>
        /// <returns>the number of the command</returns>
        protected abstract string GetNumberCommandHandle();
    }
}
