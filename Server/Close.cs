using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// this command is for close the game
    /// </summary>
    class Close : ICommand
    {
        private string[] args;
        private string result;
        private int clientId;
        public event Model.taskUpdate commendChange;
        /// <summary>
        /// this function fing the game and removr it from the list. 
        /// </summary>
        public void handle()
        {
            DataBase db = DataBase.Instance;
            foreach (Game item in db.Games.Values)
            {
                if (item.Name == args[1])
                {
                    db.Games.Remove(item.Name);
                    break;
                }
            }
        }

        /// <summary>
        /// save the args in memmber
        /// </summary>
        /// <param name="args">the name of the game to close</param>
        public void SetArgs(string[] args)
        {
            this.args = args;
        }

        /// <summary>
        /// this func create new same class
        /// </summary>
        /// <returns>new close class</returns>
        public ICommand NewCommand()
        {
            return new Close();
        }

        /// <summary>
        /// set the clientId
        /// </summary>
        /// <param name="id">the id of the client</param>
        public void setClientId(int id)
        {
            this.clientId = id;
        }
        
    }
}
