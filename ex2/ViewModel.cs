using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    class ViewModel : INotifyPropertyChanged
    {
        private static ViewModel instance;
        private IModelable model;
        //proprties here
<<<<<<< HEAD
        int Port_vm { get; set; }
        string IP_vm { get; set; }
        string MazeString_vm { get; set; }
        Pair Yriv_Cor_vm { get; set; }
        string MazeName_vm { get; set; }
        bool Winner_vm { get; set; }

        public ViewModel(IModelable model)
=======
        int VM_Port { get; set; }
        string VM_IP { get; set; }
        string VM_MazeString { get; set; }
        Pair VM_Coordinate
>>>>>>> 6092862766929cd2b1f1ac357edb53354ddcf34c
        {
            get
            {
                return model.Coordinate;
            } }
        Pair VM_Yriv_Cor { get; set; }
        string VM_MazeName { get; set; }
        bool VM_Winner { get; set; }
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
           NotifyPropertyChanged(e.PropertyName+ "_vm");
       };

        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 
        /// </summary>
        public Pair Coordinate_vm
        {
            get
            {
                return model.Coordinate;
            } 
        }
    }
}
