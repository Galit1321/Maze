using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    public class ViewModel : INotifyPropertyChanged
    {
        private static ViewModel instance;
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
        /// <summary>
        /// create a new single game in model
        /// </summary>
        public void CreateSingle()
        {
            model.createMaze();
        }
         public string CreateGame(string name)
        {
            return model.CreateGame(name);
        }
    }
}
