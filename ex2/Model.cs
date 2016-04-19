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
        private string ip;
        private int port;
        private int Heigth;
        private int Width;
        public Model(TCPClient client)
        {
            this.Client = client;
            stop = false;
            this.ip= ConfigurationManager.AppSettings["IP"];
            this.port= Int32.Parse(ConfigurationManager.AppSettings["Port"]);
            this.Width= Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Heigth= Int32.Parse(ConfigurationManager.AppSettings["Height"]);
        }
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
        public string Maze
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

        public void connect(string ip, int port)
        {
            this.Client.Connect(IP, Port);
        }

        public void createMaze()
        {
            Client.SendMsg("generate maze" + Coordinate + "0");
            string str = Client.ReceviveMsg();
            JavaScriptSerializer ser = new JavaScriptSerializer();
            SingleMaze maze = ser.Deserialize<SingleMaze>(str);
            this.Maze = maze.GetMaze();
            this.Coordinate = maze.GetStart();
            this.MazeName = maze.Name;
        }

        public void disconnect()
        {
            stop = true;
        }

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
            char[] maze = this.Maze.ToCharArray();
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
        }

        public void start()
        {
   
        }
    }
}
