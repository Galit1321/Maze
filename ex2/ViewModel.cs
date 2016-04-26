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
                
            }
        }
        public string VM_MazeString {
            get
            {
                return model.MazeString;
            }
          
        }

        public Pair VM_Coordinate
        {
            get
            {
                return model.Coordinate;
            }
        }
        public Pair VM_Yriv_Cor {
            get
            {
                return model.Yriv_Cor;
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
        public int VM_MyRow
        {
            get
            {
                return model.MyRow;
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
        public void WaitingInView()
        {
            model.Waiting();
            
            Close();
        }
         public void CreateGame(string name)
        {
            string ans= model.CreateGame(name);
            if (ans.Equals("wait"))
            {
                Open("Only One");
                Thread t = new Thread(WaitingInView);
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }else
            {
                Close();
            }
        }
    }
}
