using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace View
{
    class Model : IModelable
    {
        TCPClient Client;
        volatile bool stop;
        private int Heigth;
        private int Width;
        public SingleMaze MyMaze;
        public Model(TCPClient client)
        {
            this.Client = client;
            stop = false;
            this.ip= ConfigurationManager.AppSettings["IP"];
            this.port= Int32.Parse(ConfigurationManager.AppSettings["Port"]);
            this.Width= Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Heigth= Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            Winner = false;
        }
        private string ip;
        public string IP
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
                NotifyPropertyChanged("IP");
            }
        }
        private string maze;
        public string MazeString
        {
            get
            {
                return maze;
            }

            set
            {
               maze= value;
               NotifyPropertyChanged("Maze");

            }
        }
        private int port;
        public int Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
                NotifyPropertyChanged("Port");
            }
        }
        private Pair coordinate;
        public Pair Coordinate
        {
            get
            {
                return coordinate;
            }

            set
            {
                coordinate = value;
                NotifyPropertyChanged("Coordinate");
            }
        }
        private string  name;
        public string MazeName
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        private Pair yrivcor;
        public Pair Yriv_Cor
        {
            get
            {
                return yrivcor;
            }

            set
            {
                yrivcor = value;
                NotifyPropertyChanged("Yriv_Cor");
            }
        }
        private bool win;
        public bool Winner
        {
            get
            {
                return win;
            }

            set
            {
                win = value;
                NotifyPropertyChanged("Winner");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// helper method to the event 
        /// </summary>
        /// <param name="propName">the pro that was changed</param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        /// <summary>
        /// connect to client
        /// </summary>
        /// <param name="ip">ip address from app.cnp</param>
        /// <param name="port">port number</param>
        public void connect(string ip, int port)
        {
            this.Client.Connect(IP, Port);
        }
        /// <summary>
        /// create a maze for player
        /// </summary>
        public void createMaze()
        {
            Winner = false;
            Client.SendMsg("generate maze" + Coordinate + "0");
            string str = Client.ReceviveMsg();
            JavaScriptSerializer ser = new JavaScriptSerializer();
            SingleMaze maze = ser.Deserialize<SingleMaze>(str);
            this.MazeString = maze.GetMaze();
            this.Coordinate = maze.GetStart();
            this.MazeName = maze.Name;
        }

        public void disconnect()
        {
            stop = true;
        }
        /// <summary>
        /// get a clue from server where to go
        /// </summary>
        /// <returns></returns>
        public string getClue()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// move on the maze to given direction if 
        /// possible, we dont need the ans from server just to
        /// send that we move in case of an multiplayer game
        /// </summary>
        /// <param name="direction">which arrow key was press</param>
        public void move(string direction)
        {
            char[] maze = this.MazeString.ToCharArray();
            switch (direction)
            {
                case "up":
                    if ((this.Coordinate.Row-2>0)&& (maze[this.Coordinate.Row - Width] != '1'))
                    {
                        Client.SendMsg("play " + direction); 
                        this.Coordinate = new Pair(this.coordinate.Row - 2, this.Coordinate.Col);
                    }
                    break;
                case "down":
                    if ((this.Coordinate.Row + 2 > 2*Heigth-1) && (maze[this.Coordinate.Row + Width] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        this.Coordinate = new Pair(this.coordinate.Row + 2, this.Coordinate.Col);
                    }
                    break;
                case "right":
                    if ((this.Coordinate.Col + 2 >2* Width-1) && (maze[this.Coordinate.Col + 1] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        this.Coordinate = new Pair(this.coordinate.Row , this.Coordinate.Col+2);
                    }
                    break;
                case "left":
                    if ((this.Coordinate.Col - 2 >0) && (maze[this.Coordinate.Col - 1] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        this.Coordinate = new Pair(this.coordinate.Row , this.Coordinate.Col-2);
                    }
                    break;
            }
            if (this.Coordinate.Equals(MyMaze.End))
            {
                Winner = true;
            }
        }

        public void start()
        {
           
        }
        private Random rnd = new Random();
        public string CreateGame()
        {
            int num = rnd.Next(0, Port);
            Client.SendMsg("multiplayer game" + num);
            string ans=Client.ReceviveMsg();
            if (ans.Equals("one player"))
            {
                start();
                return "wait";
            }else
            {
                return "game on";
            }
        }
    }
}
