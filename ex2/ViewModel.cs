using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ex2
{
    public class ViewModel : INotifyPropertyChanged
    {
        private static ViewModel instance;
        public event OpenMsnWin Open;
        public event ClosenMsnWin Close;
        
        private List<string> game=new List<string>();
        private IModelable model;
        //proprties here

        public int VM_Port
        {
            get
            {
                return model.Port;
            }
            set
            {
                model.Port = value;
            }
        }
        public string VM_IP
        {
            get
            {
                return model.IP;
            }
            set
            {
                model.IP = value; 
            }
        }
        public string VM_MazeString {
            get
            {
                return model.MazeString;
            }
          
        }        
        public string VM_MazeName
        {
            get
            {
                return model.MazeString;
            }
        }
        public bool VM_Winner {
            get
            {
                return model.Winner;
            }
        }
        private ViewModel() { }
        int d;
        public int VM_MyRow
        {
            get
            {
                return model.MyRow;
            }
            set
            {
                d = value;
               // model.move(d, model.MyRow,mode);
            }
        }
       public string VM_YrivMazeString { get
            {
                return model.YrivMazeString;
            }
             }
        public int VM_MyCol
        {
            get
            {
                return model.MyCol;
            }
        }

        public int VM_EndRow
        {
            get
            {
                return model.EndRow;
            }
        }
        public int VM_EndCol
        {
            get
            {
                return model.EndCol;
            }
        }

        public string VM_YrivMazeName
        {
            get
            {
                return model.YrivMazeName;
            }
        }
       public int VM_YrivRow
        {
            get
            {
                return model.YrivRow;
            }
        }
        public int VM_YrivCol
        {
            get
            {
               return model.YrivCol;
            }
        }
       public int VM_EndYrivRow
        {

            get
            {
                return model.EndYrivRow;
            }
        }
        public int VM_EndYrivCol
        {
            get
            {
                return model.EndYrivCol;
            }
        }

        public static ViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ViewModel();
                }
                return instance;
            }
        }
        public void Init(IModelable model)
        {

            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e) {
           NotifyPropertyChanged("VM_"+e.PropertyName);
       };

        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Disconnect()
        {
            model.disconnect();
        }
        /// <summary>
        /// create a new single game in model
        /// </summary>
        public void CreateSingle()
        {
            model.createMaze();
        }

        public void Connect()
        {
            model.connect(VM_IP, VM_Port);
        }
        private Thread t;
        public void WaitingInView()
        {
            model.Waiting();
            Close();
             t = new Thread(model.start);
            t.Start();
        }
        
         public void CreateGame(string name)
        {

            string ans = model.CreateGame(name);
            if (ans.Equals("wait"))
            {
                Open("Only One");
                Thread t = new Thread(WaitingInView);
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
            else
            {
                Close();
            }
        }
        public int VM_ClueRow
        {
            get
            {
                return model.ClueRow;
            }
        }
        public bool ClueVisi
        {
            get
            {
                return model.ClueVisi ;
            }
        }
        public int VM_ClueCol
        {
            get
            {
                return model.ClueCol; ;

            }
        }
       public void move(int d)
        {
            model.move(d);
            if (VM_Winner)
            {
                Open(string.Empty);
            }
        }
        public void RestMaz()
        {
            model.RestGame();
        }
        public void closeGame()
        {
            model.closeGame();
            if (t != null) { t.Interrupt(); }
           
        }
    }
}
