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
    /// the main controller
    /// </summary>
    class MainPresentor : IMainPresentor
    {
        private IMainView view;
        private IModel model;

        private IConvertableView convert;

        /// <summary>
        /// start the operation of the server
        /// </summary>
        public void Start()
        {
            view.StartGetCommands();
        }


        /// <summary>
        /// c'tor of the command and the handlers
        /// </summary>
        /// <param name="convert">the view that convert to spesific format to view</param>
        /// <param name="model">the model</param>
        public MainPresentor(IConvertableView convert, IMainView view, IModel model)
        {
            this.view = view;
            view.newListen += this.AddListens;
            this.model = model;
            this.convert = convert;


        }

        /// <summary>
        /// listen to new event
        /// </summary>
        /// <param name="who">to who to listen</param>
        /// <param name="args">args</param>
        public void AddListens(IGetableNewNotif who, EventArgs args)
        {
            IGetableCommand get;
            who.GetNewNotif(out get);
            get.theResv += this.GetNextMsg;

        }

        /// <summary>
        /// handle command
        /// </summary>
        /// <param name="who">who send and get form it</param>
        /// <param name="args">none</param>
        public void GetNextMsg(IGetableCommand who, EventArgs args)
        {
            string msg;
            ISendableView sendable;
            who.GetCommand(out msg, out sendable);
            CommandHandler c = new CommandHandler(this.convert, this.model, msg, sendable);
            Task.Factory.StartNew(c.Handle);
        }
    }
}
