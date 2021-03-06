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

namespace ex2
{
    public delegate void OpenMsnWin(string msn);
    public delegate void ClosenMsnWin();
    class Model : IModelable
    {
        TCPClient Client;
        public SingleMaze MyMaze;
        public SingleMaze YarivMaze;
        public ConvertFromJson ser;
        int Width;  
        int Heigth; 
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="client"></param>
        public Model(TCPClient client)
        {
            Wait = false;
            this.IP = ConfigurationManager.AppSettings["IP"];
            this.Port = Int32.Parse(ConfigurationManager.AppSettings["Port"]);
            this.Client = client;
            this.Width = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Heigth = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            Disconnection =false;
            Client.LostConn += delegate () {
                this.Disconnection = true;
            };
            NeedClue = false;//clue rec to stay hidden for now
            Client.UpdateModel += delegate ()//we receive new data and we prossening it
            {
                string ans = Client.ReceiveData();
                ser = new ConvertFromJson(ans);
                FindType(ser.Type);
            };
            connect(IP, Port);
            
        }
        

        /// <summary>
        /// find what type of class is the 
        /// receive messge
        /// </summary>
        /// <param name="type"></param>
        public void FindType(string type)
        {
            switch (type)
            {
                case "1":
                    ser.CreateMaze();
                    MazeHelper();
                    Client.SendMsg("solve " + MyMaze.Name + " 0");
                    break;
                case "2":
                    ser.CreateMaze();
                    MyMaze.solv = ser.maze.Maze;
                    break;
                case "3":
                    ser.ConvertStartGame();
                    StartGame();
                    Client.SendMsg("solve " + MyMaze.Name + " 0");
                    break;
                case "4":
                    ser.ConvertPlay();
                    Play m = ser.move;
                    string d = m.Move;
                    moveYriv(d);
                    break;
                case "6":
                    ser.ConvertPlay();
                    Play m1 = ser.move;
                    Clue = m1.Move;
                    break;

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
            if (Client.Connect(IP, Port))
            {
                start();//stast thread pool
            }
        }
        
        /// <summary>
        /// create a maze for player
        /// </summary>
        private Pair StartPoint;
        public void MazeHelper()
        {
            MyMaze = ser.maze;
            this.MazeString = MyMaze.GetMaze();
            this.Coordinate = MyMaze.GetStart();
            EndRow = MyMaze.End.Row;
            EndCol = MyMaze.End.Col;
            int r = MyMaze.Start.Row;
            int c = MyMaze.Start.Col;
            StartPoint = new Pair(r, c);
            this.MyRow = r;
            this.MyCol = c;
            this.MazeName = MyMaze.Name;
        }
        Random rnd = new Random();//generate rendon num to add to maze name
        /// <summary>
        /// create a maze to single game
        /// </summary>
        public void createMaze()
        {
            Winner = false;
            NeedClue = false;
            Loser = false;
            Client.SendMsg("generate maze" + rnd.Next() + " 1");
            while (MyMaze == null)
            {
                Thread.Sleep(100);
            }
            return;

        }
        /// <summary>
        /// disconnect from server
        /// </summary>
        public void disconnect()
        {
            Client.Disconnect();
            Disconnection = true;

        }
        private string Clue;
        /// <summary>
        /// get a clue from server where to go
        /// </summary>
        /// <returns></returns>
        public void getClue()
        { 
            if( MyMaze.solv == null)
            {
                Client.SendMsg("solve " + MyMaze.Name + " 0");
            }
            while (MyMaze.solv == null)
            {
                
            } 
            int clue_pos;
            int pos = this.Coordinate.GetPos(Width);
            if (MyMaze.solv[pos] == '2')
            {
                clue_pos = MyMaze.GetNxtClue(this.Coordinate);
            }
            else
            {
                clue_pos = MyMaze.lastClue.Last();
               
            }
            ClueCol = clue_pos % (2 * Width - 1);
            ClueRow = clue_pos / (2 * Heigth - 1);
            NeedClue = true;

            return;
        }
        /// <summary>
        /// move on the maze to given direction if 
        /// possible, we dont need the ans from server just to
        /// send that we move in case of an multiplayer game
        /// </summary>
        /// <param name="direction">which arrow key was press</param>
        public void move(string direction)
        {
            NeedClue = false;//hide clue if we last request it 
            Pair p = MyMaze.move(direction,MyRow,MyCol);
            if (this.Coordinate.Equals(p))//we didnt move at all 
            {
                return;
            }
            Client.SendMsg("play "+direction);
            MyRow = p.Row;
            MyCol = p.Col;
            this.Coordinate = p;
            int pos = this.Coordinate.GetPos(Width);
            if (MyMaze.solv[pos].Equals('2'))
            {
                MyMaze.lastClue.Add(pos);
            }
                if ((this.Coordinate.Equals(MyMaze.End)))//we reach goal in maze;
            {
                Winner = true;
            }
        }
        /// <summary>
        /// start to communicte with server
        /// </summary>
        public void start()
        {
            new Thread(delegate ()
            {
                Client.Start();
            }).Start();
        }
        private string gamename;
        /// <summary>
        /// start to generte a multiplayer game
        /// </summary>
        public void StartGame()
        {
            Game g = ser.g;
            gamename = g.Name;
            MyMaze = g.You;
            YarivMaze = g.Other;
            this.MazeString = MyMaze.GetMaze();
            this.Coordinate = MyMaze.GetStart();
            EndRow = MyMaze.End.Row;
            EndCol = MyMaze.End.Col;
            int r = MyMaze.Start.Row;
            int c = MyMaze.Start.Col;
            StartPoint = new Pair(r, c);
            this.MyRow = r;
            this.MyCol = c;
            this.MazeName = MyMaze.Name;
            this.Yriv_Cor = YarivMaze.Start;
            this.YrivCol = this.Yriv_Cor.Col;
            this.YrivRow = this.Yriv_Cor.Row;
            this.EndYrivCol = YarivMaze.End.Col;
            this.EndYrivRow = YarivMaze.End.Row;
            this.YrivMazeString = YarivMaze.Maze;
            Wait = false;
        }    
        /// <summary>
        /// move the yriv acoording to 
        /// the receive move from server
        /// </summary>
        /// <param name="d">yriv direction</param>
        private void moveYriv(string d)
        {
            Pair p = MyMaze.move(d, YrivRow, YrivCol);
            if (this.Yriv_Cor.Equals(p))//we didnt move at all 
            {
                return;
            }
            YrivRow = p.Row;
            YrivCol = p.Col;
            this.Yriv_Cor= p;
            if ((this.Yriv_Cor.Equals(YarivMaze.End)))//we reach goal in maze;
            {
                Loser= true;
            }
        }   
         /// <summary>
        /// create new game 
        /// </summary>
        /// <param name="name">game name </param>
        /// <returns></returns>
        public string CreateGame(string name)
        {
            Winner = false;
            Loser = false;
            Client.SendMsg("multiplayer " + name);
            Wait = true;
                return "game on";
            }
        /// <summary>
        /// reset the maze to starting point
        /// </summary>
        public void RestGame()
        {
            this.MyRow = StartPoint.Row;
            this.MyCol = StartPoint.Col;
            this.Coordinate = new Pair(MyRow, MyCol);
            if (gamename != null)//if we in multiplayer game
            {
                this.Client.SendMsg("close " + this.gamename);
            }
        }
        /// <summary>
        /// close multiplayer game 
        /// </summary>
        public void closeGame(string name)
        {
            MyMaze = null;
            MazeString = null;
            if (name.Length!= 0)
            {
                this.Client.SendMsg("close " + name);
            }
            else
            {
                this.Client.SendMsg("close " + this.gamename);
            }

        }
        /// <summary>
        /// change the port and ip in app.conpig file
        /// </summary>
        /// <param name="newIP"></param>
        /// <param name="portstr"></param>
        public  void ChangeApp(string newIP,string portstr)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Remove("IP");
            configuration.AppSettings.Settings.Add("IP", newIP);
            configuration.AppSettings.Settings.Remove("Port");
            configuration.AppSettings.Settings.Add("Port", portstr);
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            this.IP = newIP;
            this.Port = int.Parse(portstr);
        }
        /// <summary>
        /// close the single game and clean old data
        /// </summary>
        public void closeSingle()
        {
            MyMaze = null;
            MazeString = null;
        }
        ///*********************************************Begin properties***************************************
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
                mazestr = value;
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
            }
        }
        private string name;
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

            }
        }
        private bool lost;
        public bool Loser
        {
            get
            {
                return lost;
            }
            set
            {
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
        private int row;
        public int MyRow
        {
            get
            {
                return row;
            }

            set
            {
                row = value;
                NotifyPropertyChanged("MyRow");
            }
        }
        private int col;
        public int MyCol
        {
            get
            {
                return col;
            }

            set
            {
                col = value;
                NotifyPropertyChanged("MyCol");
            }
        }
        private int endRow;
        public int EndRow
        {
            get
            {
                return endRow;
            }
            set
            {
                endRow = value;
                NotifyPropertyChanged("EndRow");
            }
        }
        private int endCol;
        public int EndCol
        {
            get
            {
                return endCol;
            }
            set
            {
                endCol = value;
                NotifyPropertyChanged("EndCol");
            }
        }
        private int yriv_row;
        public int YrivRow
        {
            get
            {
                return yriv_row;
            }

            set
            {
                yriv_row = value;
                NotifyPropertyChanged("YrivRow");
            }
        }
        private int yriv_col;
        public int YrivCol
        {
            get
            {
                return yriv_col;
            }

            set
            {
                yriv_col = value;
                NotifyPropertyChanged("YrivCol");
            }
        }
        private int eyr;
        public int EndYrivRow
        {
            get
            {
                return eyr;
            }

            set
            {
                eyr = value;
                NotifyPropertyChanged("EndYrivRow");
            }
        }
        private int eyc;
        public int EndYrivCol
        {
            get
            {
                return eyc;
            }

            set
            {
                eyc = value;
                NotifyPropertyChanged("EndYrivCol");
            }
        }
        private int cpow;
        public int ClueRow
        {
            get
            {
                return cpow;
            }

            set
            {
                cpow = value;
                NotifyPropertyChanged("ClueRow");
            }
        }
        private int ccol;
        public int ClueCol
        {
            get
            {
                return ccol;
            }

            set
            {
                ccol = value;
                NotifyPropertyChanged("ClueCol");
            }
        }
        bool visi;
        public bool NeedClue
        {
            get
            {
                return visi;
            }

            set
            {
                visi = value;
                NotifyPropertyChanged("NeedClue");
            }
        }
        private bool serverConnect;
        public bool Disconnection
        {
            get
            {
                return serverConnect;
            }

            set
            {
                serverConnect = value;
                NotifyPropertyChanged("Disconnection");
            }
        }
        private bool w;
        public bool Wait
        {
            get
            {
               return w;
            }

            set
            {
                w = value;
                NotifyPropertyChanged("Wait");
            }
        }

        ///*********************************************end properties***************************************
    }
}