using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// Multiplayer answer, implements IAnswer
    /// </summary>
    public class MultiplayerAnswer : IAnswer
    {
        public string Name { get; set; }
        public string MazeName { get; set; }
        public GeneratorAnswer You { get; set; }
        public GeneratorAnswer Other { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="game"> the game </param>
        /// <param name="mazeName"> the name of maze </param>
        public MultiplayerAnswer(Game game, string mazeName)
        {
            this.Name = game.Name;
            this.MazeName = mazeName;
            this.You = new GeneratorAnswer(game.Maze1);
            this.Other = new GeneratorAnswer(game.Maze2);
        }

        /// <summary>
        /// empty constructor
        /// </summary>
        public MultiplayerAnswer()
        {}
    }
}
