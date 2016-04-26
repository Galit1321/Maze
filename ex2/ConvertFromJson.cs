using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ex2
{
    class ConvertFromJson
    {
        public Dictionary<string, string> Serlize;

        public ConvertFromJson(Dictionary<string,string> dict)
        {
            this.Serlize = dict;
        }
        /// <summary>
        /// constructor that get serlize string and turn it to a dictionary 
        /// </summary>
        /// <param name="json">selize dict</param>
        public ConvertFromJson(string json)
        {
            Dictionary < string, string> s= JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            this.Serlize = JsonConvert.DeserializeObject<Dictionary<string, string>>(s["Content"]);
        }
        /// <summary>
        /// create a single game by the value of serlize dictionary 
        /// </summary>
        /// <returns>that single maze this serlize repersent</returns>
        public SingleMaze CreateMaze()
        {
            string maze = this.Serlize["Maze"];
            string n = this.Serlize["Name"];
            Pair start = CreatePair(this.Serlize["Start"]);
            Pair end = CreatePair(this.Serlize["End"]);
            SingleMaze m = new SingleMaze(start,end,maze,n);
            return m;
        }

        /// <summary>
        /// create coordinate of start and end of serlize sting in this.Serlize
        /// </summary>
        /// <param name="pair">selize pair</param>
        /// <returns>deselize of pair</returns>
        public Pair CreatePair(string pair)
        {
            Dictionary<string, string> ser = new Dictionary<string, string>();
            ser = JsonConvert.DeserializeObject<Dictionary<string, string>>(pair);
            int row = int.Parse(ser["Row"]);
            int col= int.Parse(ser["Col"]);
            return new Pair(row, col);
        }
         
        public SingleMaze WithoutName(string game )
        {
            Dictionary<string, string> ser = new Dictionary<string, string>();
            ser = JsonConvert.DeserializeObject<Dictionary<string, string>>(game);
            string maze = this.Serlize["Maze"];
            Pair start = CreatePair(this.Serlize["Start"]);
            Pair end = CreatePair(this.Serlize["End"]);
            SingleMaze sm = new SingleMaze(start, end, maze);
            return sm;
        }
        public Game ConvertStartGame()
        {
            string name=this.Serlize["Name"];
            string mazename=this.Serlize["MazeName"];
            SingleMaze u=WithoutName(this.Serlize["You"]);
            SingleMaze other= WithoutName(this.Serlize["Other"]);
            Game g = new Game(name, mazename, u, other);
            return g;
        }
    }
}
