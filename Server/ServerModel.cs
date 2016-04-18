using Server.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public delegate void taskUpdate(int taskId,string s);
    /// <summary>
    /// the model, handle the logic
    /// </summary>
    public class ServerModel : IModel
    {
        public event Modle.updateModel ModelChanged;
        private CommandSet commandSet;

        /// <summary>
        /// constructor
        /// create new command set
        /// </summary>
        public ServerModel()
        {
            this.commandSet = new CommandSet();
        }

        /// <summary>
        /// Do work given the command and id of client
        /// </summary>
        /// <param name="id"> id of client </param>
        /// <param name="massege"> the message </param>
        public void DoWork(int id, string massege)
        {
            string[] array = massege.Split(' ');
            string option = array[0];
            if (commandSet.isCommand(option))
            {
                ICommand hendler = this.commandSet.GetCommand(option);
                hendler = hendler.NewCommand();
                hendler.SetArgs(array);
                hendler.setClientId(id);
                hendler.commendChange += delegate(int clientId,string s)
                {
                    ModelChanged(clientId, s);

                };
                Task.Factory.StartNew(hendler.handle);
            }
            else
            {
                string s = "Command not found";
                ModelChanged(id, s);
            }
        }
    }
}
