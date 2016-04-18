using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// play the answer
    /// </summary>
    class PlayAnswer :IAnswer 
    {
        public string Name { get; set; }
        public string Move { get; set; }

        /// <summary>
        /// empty constructor
        /// </summary>
        public PlayAnswer(){}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="game"> the game to play </param>
        public PlayAnswer(Game game)
        {
            this.Name = game.Name;
            this.Move = game.Move;
        }
    }
}
