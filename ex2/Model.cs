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

        volatile bool stop;
        private int Heigth;
        private int Width;
        public SingleMaze MyMaze;
        public SingleMaze YarivMaze;
        public Model(TCPClient client)
        {
            this.Client = client;
            stop = false;
            this.ip= ConfigurationManager.AppSettings["IP"];
            this.port= Int32.Parse(ConfigurationManager.AppSettings["Port"]);
            this.Width= Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            this.Heigth= Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            connect(ip, port);

        }
        ~Model()
        {
            stop = false;
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
                if (lost==true) WinWin("You Lost :(");
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
               row= value;
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

        private bool ins;
        public bool InSession
        {
            get
            {
                return ins;
            }

            set
            {
                ins = value;
                stop = true;
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
            ConvertFromJson ser=new ConvertFromJson(str);
            MyMaze = ser.CreateMaze();
            this.MazeString = MyMaze.GetMaze();
            this.Coordinate = MyMaze.GetStart();
            EndRow = MyMaze.End.Row;
            EndCol = MyMaze.End.Col;
            this.MyRow = MyMaze.Start.Row;
            this.MyCol = MyMaze.Start.Col;
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
            ConvertFromJson ser = new ConvertFromJson(Client.ReceviveMsg());
            SingleMaze sol_maze = ser.CreateMaze();
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
       public void move(int direction)
        {
            char[] maze = this.MazeString.ToCharArray();
            int pos = (2 * this.Width - 1) * (this.Coordinate.Row) + this.Coordinate.Col;//the plae of cor in maze string
            switch (direction)
            {
                case 1://up
                    if ((MyRow-2>=0)&& (this.MazeString[pos-(2*Width-1) ] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        this.MyRow = this.MyRow - 1;
                        this.MyRow = this.MyRow - 1;
                        this.coordinate.Row = MyRow;
                    }
                    break;
                case 2://down
                    if ((MyRow + 2 < 2*Heigth-1) && (this.MazeString[pos+(2*Width-1)] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        this.MyRow =this.MyRow+ 2;
                        this.Coordinate.Row = MyRow;
                    }
                    break;
                case 3://right
                    if ((MyCol + 2 <2* Width-1) && (this.MazeString[pos+1 ] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        MyCol += 2;
                        this.coordinate.Col = MyCol;
                    }
                    break;
                case 4://le ft
                    if ((MyCol - 2 >=0) && (this.MazeString[pos - 1] != '1'))
                    {
                        Client.SendMsg("play " + direction);
                        MyCol -= 2;
                        this.coordinate.Col = MyCol;
                    }
                    break;
            }


            if ((this.Coordinate.Equals(MyMaze.End)))//we reach goal in maze;
            {
                Winner = true;
                stop = true;
            }
        }
        public void Waiting()
        {
            string msg = "";
            while (msg.Length==0)
            {
                msg = Client.ReceviveMsg();
                
            }
            StartGame(msg);
           
        }
        public void start()
        {
            while (!stop)
            {
                string msn = "";
                msn = Client.ReceviveMsg();
                Play m = JsonConvert.DeserializeObject<Play>(msn);
                string d = m.Move;
                moveYriv(d);

            }
        }
        public void StartGame(string ans)
        {
            ConvertFromJson ser = new ConvertFromJson(ans);
            Game g = ser.ConvertStartGame();
            MyMaze = g.You;
            YarivMaze = g.Other;
            this.Coordinate = MyMaze.Start;
            this.MyCol = this.Coordinate.Col;
            this.MyRow = this.Coordinate.Row;
            this.Yriv_Cor = YarivMaze.Start;
            this.YrivCol = this.Yriv_Cor.Col;
            this.YrivRow = this.Yriv_Cor.Row;
            this.MazeString = MyMaze.Maze;
            this.YrivMazeString = YarivMaze.Maze;
            
        }
        private void moveYriv(string d)
        {
            char[] maze = this.YrivMazeString.ToCharArray();
            switch (d)
            {
                case "up":
                    if ((YrivRow - 2 > 0) && (maze[this.Coordinate.Row - Width] != '1'))
                    {
                        Client.SendMsg("play " + d);
                        YrivRow -= 2;
                    }
                    break;
                case "down":
                    if ((MyRow + 2 > 2 * Heigth - 1) && (maze[this.Coordinate.Row + Width] != '1'))
                    {
                        Client.SendMsg("play " + d);
                        YrivRow += 2;
                    }
                    break;
                case "right":
                    if ((YrivCol + 2 > 2 * Width - 1) && (maze[this.Coordinate.Col + 1] != '1'))
                    {
                        Client.SendMsg("play " + d);
                       YrivCol += 2;
                    }
                    break;
                case "left":
                    if ((YrivCol - 2 > 0) && (maze[this.Coordinate.Col - 1] != '1'))
                    {
                        Client.SendMsg("play " + d);
                        YrivCol -= 2;
                    }
                    break;
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
            Client.SendMsg("multiplayer " +name);
            string ans = Client.ReceviveMsg();
            ConvertFromJson ser = new ConvertFromJson(ans);
            Game g = ser.ConvertStartGame();
            if (g.Name.Equals("one player"))
            {
                return "wait";
            }
            else
            {
                StartGame(ans);
               // Thread t = new Thread(start);
             //   t.Start();
                return ans;
            }
           
              
            }
        

       
    }
}
