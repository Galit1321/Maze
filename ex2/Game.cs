using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    class Game
    {
        public string Name { get; set; }
        public string MazeName { get; set; }
        public SingleMaze You { get; set; }
        public SingleMaze Other { get; set; }

        public Game(string name, string mazename, SingleMaze u, SingleMaze other)
        {
            this.Name = name;
            this.MazeName = mazename;
            this.You = u;
            this.Other = other;
        }


    }
}
