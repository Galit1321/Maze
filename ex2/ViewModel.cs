using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class ViewModel : INotifyPropertyChanged
    {
        private static ViewModel instance;
        private IModelable model;
        //proprties here

        public int VM_Port { get; set; }
        public string VM_IP { get; set; }
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
            } }
        public Pair VM_Yriv_Cor { get; set; }
        public string VM_MazeName { get; set; }
        public bool VM_Winner { get; set; }
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
       public void CreateSingle()
        {
            model.createMaze();
        }
    }
}
