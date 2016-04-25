using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json; 

namespace View
{
    public delegate void OpenMsnWin();
    class Model : IModelable
    {
        TCPClient Client;
        public event OpenMsnWin WinWin;
        private JavaScriptSerializer ser;
        volatile bool stop;
        private int Heigth;
        private int Width;
        public SingleMaze MyMaze;
        public SingleMaze YarivMaze;
        public Model(TCPClient client)
        {
            this.Client = client;
            stop = false;
            ser = new JavaScriptSerializer();
            this.ip= ConfigurationManager.AppSettings["IP"];
            this.port= Int32.Parse(ConfigurationManager.AppSettings["Port"]);
            this.Width= Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Heigth= Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            connect(ip, port);
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
        private string mazestr;
        public string MazeString
        {
            get
            {
                return mazestr;
            }

            set
            {
               mazestr= value;
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
                //WinWin();
            }
        }
        private bool lost;
        public bool Loser
        {
            get
            {
                return lost;
            }
            set {
                lost = value;
                NotifyPropertyChanged("Loser");
            }
        }
        private string yrivstring;
        public string YrivMazeString
        {
            get
            {
                return yrivstring;
            }

            set
            {
                yrivstring = value;
                NotifyPropertyChanged("YrivMazeString");
            }
        }
        private string yrivname;
        public string YrivMazeName
        {
            get
            {
                return yrivname;
            }

            set
            {
                yrivname = value;
                NotifyPropertyChanged("YrivMazeName");
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
            Loser = false;
            Client.SendMsg("generate maze" + rnd.Next() + " 1");
           string str = Client.ReceviveMsg();
            MyMaze = JsonConvert.DeserializeObject<SingleMaze>(str);
            this.MazeString = MyMaze.GetMaze();
            this.Coordinate = MyMaze.GetStart();
            this.MazeName = MyMaze.Name;
        }

        public void disconnect()
        {
            stop = true;
            Client.disconnect();
        }
        /// <summary>
        /// get a clue from server where to go
        /// </summary>
        /// <returns></returns>
        public List<int> getClue()
        {
            List<int> s = new List<int>() ;
            Client.SendMsg("solve " + MyMaze.Name + " 0");
            SingleMaze sol_maze = JsonConvert.DeserializeObject<SingleMaze>(Client.ReceviveMsg());
            string strsolv = sol_maze.Maze;
            int pivot = this.Coordinate.Row * (2 * Heigth - 1) + this.Coordinate.Col * (2 * Width - 1);
            int begin = pivot - (2 * Width - 1);//look row before for clues 
            int end = pivot + (2 * Width - 1);//look in the next row clues
            if (begin<0)//if we are not in first row
            {
                begin = 0;
            }
            if (end > strsolv.Length)
            {
                end = strsolv.Length;
            }
            char[] arr = strsolv.ToCharArray();
            for (int i = begin; begin < end; i++)
            {
                if (arr[i].Equals('2'))
                {
                    s.Add(i);
                }
            }
            return s;
        }
        /// <summary>
        /// move on the maze to given direction if 
        /// possible, we dont need the ans from server just to
        /// send that we move in case of an multiplayer game
        /// </summary>
        /// <param name="direction">which arrow key was press</param>
        public void move(string direction,Pair cor)
        {
            char[] maze = this.MazeString.ToCharArray();
            switch (direction)
            {
                case "up":
                    if ((this.Coordinate.Row-2>0)&& (maze[this.Coordinate.Row - Width] != '1'))
                    {
                        Client.SendMsg("play " + direction); 
                        cor = new Pair(this.coordinate.Row - 2, this.Coordinate.Col);
                    }
                    break;
                case "down":
                    if ((this.Coordinate.Row + 2 > 2*Heigth-1) && (maze[this.Coordinate.Row + Width] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        cor = new Pair(this.coordinate.Row + 2, this.Coordinate.Col);
                    }
                    break;
                case "right":
                    if ((this.Coordinate.Col + 2 >2* Width-1) && (maze[this.Coordinate.Col + 1] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        cor = new Pair(this.coordinate.Row , this.Coordinate.Col+2);
                    }
                    break;
                case "left":
                    if ((this.Coordinate.Col - 2 >0) && (maze[this.Coordinate.Col - 1] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        cor= new Pair(this.coordinate.Row , this.Coordinate.Col-2);
                    }
                    break;
            }
            if ((cor.Equals(this.Coordinate))&&(cor.Equals(MyMaze.End)))//we reach goal in maze;
            {
                Winner = true;
                stop = true;
            } else if ((cor.Equals(this.yrivcor)) && (cor.Equals(YarivMaze.End)))//if yariv won the game
            {
                stop = true;
                Loser = true;
            }
        }

        public void start()
        {
            while (!stop)
            {
                string msn = "";
                msn = Client.ReceviveMsg();
                if (msn.Contains("{You"))
                {
                    Game g= ser.Deserialize<Game>(msn);
                    MyMaze = g.You;
                    YarivMaze = g.Other;
                    this.Coordinate = MyMaze.Start;
                    this.Yriv_Cor = YarivMaze.Start;
                }
                else
                {
                    Play m = ser.Deserialize<Play>(msn);
                    string d = m.Move;
                    move(d, this.Yriv_Cor);
                }
            }
        }
        private Random rnd = new Random();
        /// <summary>
        /// create new game 
        /// </summary>
        /// <param name="name">game name </param>
        /// <returns></returns>
        public string CreateGame(string name)
        {
            Winner = false;
            Loser = false;
            int num = rnd.Next(0, Port);
            Client.SendMsg("multiplayer " +name);
            string ans=Client.ReceviveMsg();
            if (ans.Equals("one player"))
            {
                Thread t = new Thread(start);//creating a thread to make connection 
                t.Start();
                return "wait";
            }else
            {
                return "game on";
            }
        }
    }
}
