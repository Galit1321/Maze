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
    /// the play command
    /// </summary>
    class PlayCommand : ICommandable, IHandlerUpdate
    {
        private IHandleOutput<string> handler;
        private IModel model;

        /// <summary>
        /// c'tor of the play command
        /// </summary>
        /// <param name="handler">the handler</param>
        /// <param name="model">the model</param>
        public PlayCommand(IHandleOutput<string> handler, IModel model)
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
            this.model.PlayerMoved(args[0], sender);
        }

        /// <summary>
        /// update the view
        /// </summary>
        /// <param name="theUpdate">the update</param>
        /// <param name="view">who to update</param>
        public void UpdateView(object theUpdate, ISendableView view)
        {
            this.handler.HandleOutput(theUpdate as string, view);
        }
    }
}
