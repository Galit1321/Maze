using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// Play implements ICommand
    /// </summary>
    class Play :ICommand
    {
        private string[] args;
        private string result;
        private int clientId;
        public event Model.taskUpdate commendChange;

        /// <summary>
        /// handling
        /// </summary>
        public void handle()
        {
            DataBase db = DataBase.Instance;
            Dictionary<string, Game> games = db.Games;
            bool notInGame = true;
            foreach (Game item in games.Values)
            {
                if (item.Client1 == this.clientId || item.Client2 == this.clientId)
                {
                    notInGame = false;
                    item.Move = args[1];
                    PlayAnswer answer = new PlayAnswer(item);
                    SerializeAnswer ser = new SerializeAnswer(4, answer);
                    string s = ser.Serialize();
                    if (item.Client1 == this.clientId) {
                        commendChange(item.Client2, s);
                    }
                    else
                    {
                        commendChange(item.Client1, s);
                    }
                    break;
                }
            }
            if (notInGame)
            {
                commendChange(this.clientId, "you are not in any game");
            }
        }

        /// <summary>
        /// Set arguments
        /// </summary>
        /// <param name="args"> the arguments to set </param>
        public void SetArgs(string[] args)
        {
            this.args = args;
        }

        /// <summary>
        /// create this command
        /// </summary>
        /// <returns> returns new play command </returns>
        public ICommand NewCommand()
        {
            return new Play();
        }

        /// <summary>
        /// Set client id
        /// </summary>
        /// <param name="id"> the given id to set</param>
        public void setClientId(int id)
        {
            this.clientId = id;
        }

       
        
    }
}
