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
    ///  the solve command
    /// </summary>
    class SolveCommand : ICommandable
    {
        private IHandleOutput<IMaze> handler;
        private IModel model;

        /// <summary>
        /// c'tor of the solve command
        /// </summary>
        /// <param name="handler">the handler of the view</param>
        /// <param name="model">the model</param>
        public SolveCommand(IHandleOutput<IMaze> handler, IModel model)
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
            IMaze maze = this.model.GetSolutionMaze(args[0],Int32.Parse(args[1]));
            this.handler.HandleOutput(maze, sender);
        }
    }
}
