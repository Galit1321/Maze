using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.view;
using ServerExe1.src.model;
namespace ServerExe1.src.Presentor
{
    /// <summary>
    /// the command to close
    /// </summary>
    class CloseCommand : ICommandable
    {

        private IHandleOutput<string> handler;
        private IModel model;

        /// <summary>
        /// c'tor of the command
        /// </summary>
        /// <param name="handler">the handler of the the command output</param>
        /// <param name="model">the model</param>
        public CloseCommand(IHandleOutput<string> handler, IModel model)
        {
            this.handler = handler;
            this.model = model;
        }

        /// <summary>
        /// do the command
        /// </summary>
        /// <param name="args">the args of the command</param>
        /// <param name="sender">who send the command and to who send back</param>
        public void Execute(List<string> args, ISendableView sender)
        {
            this.model.ColseGame(args[0], sender);
        }

    }
}
