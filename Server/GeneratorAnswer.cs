using Mazelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// Generate answer implements IAnswer
    /// </summary>
    public class GeneratorAnswer : IAnswer
    {
        public string Name { get; set; }
        public string Maze { get; set; }
        public Location Start { get; set; }
        public Location End { get; set; }

        /// <summary>
        /// empty constructor
        /// </summary>
        public GeneratorAnswer()
        {

        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="maze"> maze </param>
        public GeneratorAnswer(MazeSolved maze)
        {
            this.Name = maze.Name;
            this.Maze = maze.Maze;
            this.Start = maze.Start;
            this.End = maze.End;
        }
    }
}
