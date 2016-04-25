using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ex2
{
    public class SingleMaze
    {
        public Pair Start;
        public Pair End;
        public string Name;
        public string Maze;
     
        [JsonConstructor]
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
