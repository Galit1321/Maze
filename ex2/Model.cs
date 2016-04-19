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
        public Model(TCPClient client)
        {
            this.Client = client;
            stop = false;
            this.ip= ConfigurationManager.AppSettings["IP"];
            this.port= Int32.Parse(ConfigurationManager.AppSettings["Port"]);
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

        public void move(string direction)
        {
            char[] maze = this.Maze.ToCharArray();
            switch (direction)
            {
            //    case "up":
          //          if ((this.Coordinate.Row-2<0) && )
            }
        }

        public void start()
        {
   
        }
    }
}
