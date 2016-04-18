using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// Contains the set of commands
    /// </summary>
    public class CommandSet
    {
        private Dictionary<string, ICommand> commendSet;

        /// <summary>
        /// constructor
        /// </summary>
        public CommandSet()
        {
            this.commendSet = new Dictionary<string, ICommand>();
            commendSet.Add("generate", new Generate());
            commendSet.Add("solve", new Solve());
            commendSet.Add("multiplayer", new Multiplayer());
            commendSet.Add("play", new Play());
            commendSet.Add("close", new Close());
        }

        /// <summary>
        /// check if the given string is a legal command
        /// </summary>
        /// <param name="str"> string to check </param>
        /// <returns> returns true if the command is legal </returns>
        public bool isCommand(string str) {
            return commendSet.ContainsKey(str);
        }

        /// <summary>
        /// get a specific command
        /// </summary>
        /// <param name="key"> the key of command </param>
        /// <returns> returns the relevant command </returns>
        public ICommand GetCommand(string key)
        {
            return this.commendSet[key];
        }
    }
}
