using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Configuration;

namespace ex2
{
    public class SingleMaze
    {
        public Pair Start;
        public Pair End;
        public string Name;
        public string Maze;
        private int Width;
        private int Height;
        public string solv;
        public List<int> lastClue;


        public SingleMaze(Pair start, Pair end, string maze)
        {
            this.Width = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Height = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            this.Start = start;
            this.End = end;
            this.Maze = maze;
            this.Name = "You";
            this.lastClue = new List<int>();
            this.lastClue.Add(start.GetPos(Width));
        }
        public SingleMaze(Pair start, Pair end,string maze,string name)
        {
            this.Width = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Height = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            this.Start = start;
            this.End = end;
            this.Maze = maze;
            this.Name = name;
            this.lastClue = new List<int>();
            this.lastClue.Add(start.GetPos(Width));
        }
        public string GetMaze()
        {
            return this.Maze;
        }
        public Pair GetStart()
        {
            return this.Start;
        }
        public Pair move(string direction, int row, int col)
        {
            int pos = (2 * Width - 1) * (row) + col;//the place of cor in maze string
            switch (direction)
            {
                case "up"://up
                    if ((row - 2 >= 0) && (Maze[pos - (2 * Width - 1)] != '1'))
                    {
                        row = row - 2;
                    }
                    break;
                case "down"://down
                    if ((row + 2 < 2 * Height - 1) && (Maze[pos + (2 * Width - 1)] != '1'))
                    {
                        row = row+ 2;
                    }
                    break;
                case "right"://right
                    if ((col + 2 < 2 *Width - 1) && (this.Maze[pos + 1] != '1'))
                    {
                        col += 2;
                    }
                    break;
                case "left"://le ft
                    if ((col - 2 >= 0) && (this.Maze[pos - 1] != '1'))
                    {
                        col -= 2; 
                    }
                    break;
            }
            return new Pair(row, col);
        }
        public int GetNxtClue(Pair cor)
        {
            Pair p;
            int pos;
            List<string> dir = new List<string>{ "up", "down", "left", "right" };
            foreach (string d in dir)
            {
                p= move(d, cor.Row, cor.Col);//check the dirction
                if (p != null)
                {
                   pos = p.GetPos(Width);
                    if ((solv[pos].Equals('2')) & (!(lastClue.Contains(pos))))
            {
                        return pos;
                    }
                }
            }  
            return lastClue.Last(); ;
        }
    }
    }
