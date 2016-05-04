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


        public SingleMaze(Pair start, Pair end, string maze)
        {
            this.Width = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Height = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            this.Start = start;
            this.End = end;
            this.Maze = maze;
            this.Name = "You";
        }
        public SingleMaze(Pair start, Pair end,string maze,string name)
        {
            this.Width = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Height = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
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
    }
    }
