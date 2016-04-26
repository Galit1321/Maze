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
    /// the multiplayer command
    /// </summary>
    class MultiplayerCommand : ICommandable, IHandlerUpdate 
    {
        private IHandleOutput<Tuple<string, Tuple<IMaze, IMaze>>> handler;
        private IHandlerUpdate msgHandler;
        private IModel model;

        /// <summary>
        /// c'tor of the command
        /// </summary>
        /// <param name="handler">the handler of this command</param>
        /// <param name="msgHandler">the handler of the play command handler of the game</param>
        /// <param name="model">the model</param>
        public MultiplayerCommand(IHandleOutput <Tuple<string, Tuple<IMaze, IMaze>>> handler,
            IHandlerUpdate msgHandler, IModel model)
        {
            this.handler = handler;
            this.msgHandler = msgHandler;
            this.model = model;
        }

        /// <summary>
        /// do the command
        /// </summary>
        /// <param name="args">the args of the command</param>
        /// <param name="sender">who send the command and to who send back</param>
        public void Execute(List<string> args, ISendableView view)
        {
            this.model.AddPlayerToGame(args[0], this, this.msgHandler, view);
        }

        /// <summary>
        /// update the view according the handler and the view
        /// </summary>
        /// <param name="theUpdate">what need to update</param>
        /// <param name="view">who need to update</param>
        public void UpdateView(object theUpdate ,ISendableView view)
        {
            this.handler.HandleOutput(theUpdate as Tuple<string, Tuple<IMaze, IMaze>>, view);
        }
    }
}
