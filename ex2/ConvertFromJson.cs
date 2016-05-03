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
        public SingleMaze maze;
        public Game g;
        public Play move;
        public string Type;
        
        /// <summary>
        /// constructor that get serlize string and turn it to a dictionary 
        /// </summary>
        /// <param name="json">selize dict</param>
        public ConvertFromJson(string json)
        {

            Dictionary<string,string> dic= JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            this.Type = dic["Type"];
            this.Serlize = JsonConvert.DeserializeObject<Dictionary<string, string>>(dic["Content"]);
        }
       
        public void FindType(string type)
        {
            switch (type)
            {
                case "1":
                    CreateMaze();
                    break;
                case "2":
                    CreateMaze();
                    break;
                case "3":
                    ConvertStartGame();
                    break;
                case "4":
                    ConvertPlay();
                    break;
                
            }
        }
        /// <summary>
        /// create a single game by the value of serlize dictionary 
        /// </summary>
        /// <returns>that single maze this serlize repersent</returns>
        public void CreateMaze()
        {
            string maze = this.Serlize["Maze"];
            string n = this.Serlize["Name"];
            Pair start = CreatePair(this.Serlize["Start"]);
            Pair end = CreatePair(this.Serlize["End"]);
            SingleMaze m = new SingleMaze(start,end,maze,n);
            this.maze = m;
        }

        /// <summary>
        /// create coordinate of start and end of serlize sting in this.Serlize
        /// </summary>
        /// <param name="pair">selize pair</param>
        /// <returns>deselize of pair</returns>
        public Pair CreatePair(string pair)
        {
            string[] des = pair.Split('@');
            int r = int.Parse(des[0]);
            int c = int.Parse(des[1]);
            return new Pair(r, c);
        }
         
        public SingleMaze WithoutName(string game )
        {
            Dictionary<string, string> ser = new Dictionary<string, string>();
            ser = JsonConvert.DeserializeObject<Dictionary<string, string>>(game);
            string maze = ser["Maze"];
            Pair start = CreatePair(ser["Start"]);
            Pair end = CreatePair(ser["End"]);
            SingleMaze sm = new SingleMaze(start, end, maze);
            return sm;
        }
        public void ConvertStartGame()
        {
            string name=this.Serlize["Name"];
            string mazename=this.Serlize["MazeName"];
            SingleMaze u=WithoutName(this.Serlize["You"]);
            SingleMaze other= WithoutName(this.Serlize["Other"]);
            Game g = new Game(name, mazename, u, other);
            this.g=g;
        }
        public void ConvertPlay()
        {
            string name=this.Serlize["Name"];
            string move = this.Serlize["Move"];

           this.move= new Play(name, move);
        }
    }
}
