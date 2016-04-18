using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// details of game
    /// </summary>
    public class Game
    {
        public MazeSolved Maze1 { get; set; }
        public MazeSolved Maze2 { get; set; }
        public string Name { get; set; }
        public int Client1 { get; set; }
        public int Client2 { get; set; }
        public string Move { get; set; }

        /// <summary>
        /// empty constructor
        /// </summary>
        public Game() {}
    }
}
