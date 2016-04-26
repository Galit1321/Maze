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
    /// the generante command
    /// </summary>
    class GenerateCommand : ICommandable
    {
        private IHandleOutput<IMaze> handler;
        private IModel model;

        /// <summary>
        /// c'tor of the generate
        /// </summary>
        /// <param name="handler">the handler</param>
        /// <param name="model">the model</param>
        public GenerateCommand(IHandleOutput<IMaze> handler, IModel model)
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
            IMaze maze = this.model.GetMaze(args[0], Int32.Parse(args[1]));
            this.handler.HandleOutput(maze, sender);
        }
    }
}
