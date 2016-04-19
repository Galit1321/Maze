using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class SingleMaze
    {
        public Pair Start;
        public Pair End;
        public string Name;
        public string Maze;

        public SingleMaze(Pair start, Pair end,string maze,string name)
        {
            this.Start = start;
            this.End = end;
            this.Maze = maze;
            this.Name = name;
        }
        public string GetMaze()
        {
            return this.Maze;
        }
        public Pair GetStart()
        {
            return this.Start;
        }
    }
}
