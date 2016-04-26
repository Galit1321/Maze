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
        public ConvertFromJson(string json)
        {
            Dictionary < string, string> s= JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            this.Serlize = JsonConvert.DeserializeObject<Dictionary<string, string>>(s["Content"]);
        }

        public SingleMaze CreateMaze()
        {
            string maze = this.Serlize["Maze"];
            string n = this.Serlize["Name"];
            Pair start = CreatePair(this.Serlize["Start"]);
            Pair end = CreatePair(this.Serlize["End"]);
            SingleMaze m = new SingleMaze(start,end,maze,n );
            return m;
        }
        public Pair CreatePair(string pair)
        {
            Dictionary<string, string> ser = new Dictionary<string, string>();
            ser = JsonConvert.DeserializeObject<Dictionary<string, string>>(pair);
            int row = int.Parse(ser["Row"]);
            int col= int.Parse(ser["Col"]);
            return new Pair(row, col);
        }
    }
}
