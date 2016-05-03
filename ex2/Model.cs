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
        public event OpenMsnWin WinWin;

        volatile bool stop = false;
        private int Heigth;
        private int Width;
        public SingleMaze MyMaze;
        public SingleMaze YarivMaze;
        public ConvertFromJson ser;
        public Model(TCPClient client)
        {

            this.Client = client;
         
            NeedClue = false;
            this.IP = ConfigurationManager.AppSettings["IP"];
            this.Port = Int32.Parse(ConfigurationManager.AppSettings["Port"]);
            this.Width = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Heigth = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            connect(ip, port);
            start();
           

        }
        ~Model()
        {
            stop = true;
           

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
                NotifyPropertyChanged("Coordinate");
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
        Random rnd = new Random();
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
        public void createMaze()
        {
            Winner = false;
            NeedClue = false;
            Loser = false;
            Client.SendMsg("generate maze" + rnd.Next() + " 1");
            while (MyMaze == null)
            {
                Thread.Sleep(1);
            }
           
        }

        public void disconnect()
        {
            stop = true;
            Thread.Sleep(20000);//let start thread to read stop;
            Client.disconnect();
           // t.Join();
        }
        
        /// <summary>
        /// get a clue from server where to go
        /// </summary>
        /// <returns></returns>
        public void getClue()
        {
            Client.SendMsg("clue " +MyMaze.Name+" "+ MyRow + " " + MyCol);
            while (Clue == null)
            {
                Thread.Sleep(1200);
            }
            switch (Clue)
            {
                case "up":
                    ClueCol = MyCol;
                    ClueRow = MyRow - 2;
                    NeedClue = true;
                    break;
                case "down":
                    ClueCol = MyCol;
                    ClueRow = MyRow + 2;
                    NeedClue = true;
                    break;
                case "left":
                    ClueCol = MyCol-2;
                    ClueRow = MyRow ;
                    NeedClue = true;
                    break;
                case "right":
                    ClueCol = MyCol+2;
                    ClueRow = MyRow ;
                    NeedClue = true;
                    break;
            }
            return;
        }
       
        
        /// <summary>
        /// move on the maze to given direction if 
        /// possible, we dont need the ans from server just to
        /// send that we move in case of an multiplayer game
        /// </summary>
        /// <param name="direction">which arrow key was press</param>
        public void move(int direction)
        {

            NeedClue = false;
            int pos = (2 * this.Width - 1) * (this.Coordinate.Row) + this.Coordinate.Col;//the plae of cor in maze string
            switch (direction)
            {
                case 1://up
                    if ((MyRow - 2 >= 0) && (this.MazeString[pos - (2 * Width - 1)] != '1'))
                    {
                        Client.SendMsg("play up");
                        this.MyRow = this.MyRow - 2;
                        this.Coordinate.Row = MyRow;
                    }
                    break;
                case 2://down
                    if ((MyRow + 2 < 2 * Heigth - 1) && (this.MazeString[pos + (2 * Width - 1)] != '1'))
                    {
                        Client.SendMsg("play down");
                        this.MyRow = this.MyRow + 2;

                        this.Coordinate.Row = MyRow;
                    }
                    break;
                case 3://right
                    if ((MyCol + 2 < 2 * Width - 1) && (this.MazeString[pos + 1] != '1'))
                    {
                        Client.SendMsg("play right");
                        MyCol += 2;
                        this.Coordinate.Col = MyCol;
                    }
                    break;
                case 4://le ft
                    if ((MyCol - 2 >= 0) && (this.MazeString[pos - 1] != '1'))
                    {
                        Client.SendMsg("play left");
                        MyCol -= 2;
                        this.Coordinate.Col = MyCol;
                    }
                    break;
            }

            if ((this.Coordinate.Equals(MyMaze.End)))//we reach goal in maze;
            {
                Winner = true;

            }
        }
        public void Waiting()
        {
            while (numOfPlayer < 2)
            {
                Thread.Sleep(1000);

            }
        }
        int numOfPlayer;
        volatile bool isWorking = false;
        public void start()
        {
            new Thread(delegate () {
            while (!stop)
            {
                string msn = "";
                msn = Client.ReceviveMsg();
                    if (!isWorking)
                    {
                        isWorking = true;
                        ser = new ConvertFromJson(msn);
                        
                    }   
                FindType(ser.Type);
                Thread.Sleep(500);

                }
        }).Start();
        }
        public void FindType(string type)
        {
            switch (type)
            {
                case "1":
                    ser.CreateMaze();
                    MazeHelper();
                    break;
                
                case "3":
                    numOfPlayer += 1;
                    ser.ConvertStartGame();
                    StartGame();
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
        private string Clue;
        private string gamename;
        public void StartGame()
        {
            Game g = ser.g;
            gamename = g.Name;
            MyMaze = g.You;
            YarivMaze = g.Other;
            this.Coordinate = MyMaze.Start;
            this.MyCol = this.Coordinate.Col;
            this.MyRow = this.Coordinate.Row;
            this.EndCol = MyMaze.End.Col;
            this.EndRow = MyMaze.End.Row;
            this.Yriv_Cor = YarivMaze.Start;
            this.YrivCol = this.Yriv_Cor.Col;
            this.YrivRow = this.Yriv_Cor.Row;
            this.EndYrivCol = YarivMaze.End.Col;
            this.EndYrivRow = YarivMaze.End.Row;
            this.MazeString = MyMaze.Maze;
            this.YrivMazeString = YarivMaze.Maze;
        

        }
        private void moveYriv(string d)
        {
            int pos = (2 * this.Width - 1) * (this.Yriv_Cor.Row) + this.Yriv_Cor.Col;//the plae of cor in maze string
            switch (d)
            {
                case "up"://up
                    if ((this.YrivRow - 2 >= 0) && (this.YrivMazeString[pos - (2 * Width - 1)] != '1'))
                    {
                       
                        this.YrivRow = this.YrivRow - 1;
                        this.YrivRow = this.YrivRow - 1;
                        this.Yriv_Cor.Row = this.YrivRow;
                    }
                    break;
                case "down"://down
                    if ((this.YrivRow + 2 < 2 * Heigth - 1) && (this.YrivMazeString[pos + (2 * Width - 1)] != '1'))
                    {

                        this.YrivRow = this.YrivRow + 2;
                         this.Yriv_Cor.Row = this.YrivRow;
                    }
                    break;
                case "right"://right
                    if ((YrivCol + 2 < 2 * Width - 1) && (this.YrivMazeString[pos + 1] != '1'))
                    {

                        YrivCol += 2;
                        this.Yriv_Cor.Col = YrivCol;
                    }
                    break;
                case "left"://le ft
                    if ((YrivCol - 2 >= 0) && (this.YrivMazeString[pos - 1] != '1'))
                    {

                        YrivCol -= 2;
                        this.Yriv_Cor.Col = YrivCol;
                    }
                    break;
            }
            if ((this.Yriv_Cor.Row.Equals(this.EndYrivRow)) && (this.Yriv_Cor.Col.Equals(this.EndYrivCol)))
            {
                this.Loser = true;
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
            Client.SendMsg("multiplayer " +name);
            while (MyMaze == null)
            {
                Thread.Sleep(1);
            }
            Game g = ser.g;
            if (g.Name.Equals("one player"))
            {

                return "wait";
            }
            else
            {
                
                return "game on";
            }
           
              
            }

        public void RestGame()
        {
            this.MyRow =StartPoint.Row;
            this.MyCol =StartPoint.Col;
            this.Coordinate = new Pair(MyRow, MyCol);
        }
        public void closeGame()
        {
            this.Client.SendMsg("close " + gamename);
        }
    }
}
