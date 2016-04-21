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
        private IModelable model;
        //proprties here
        int Port_vm { get; set; }
        string IP_vm { get; set; }
        string MazeString_vm { get; set; }
        Pair Coordinate_vm { get; set; }
        Pair Yriv_Cor_vm { get; set; }
        string MazeName_vm { get; set; }
        bool Winner_vm { get; set; }
        public ViewModel(IModelable model)
        {
            this.model = model;
            model.PropertyChanged +=
          delegate (Object sender, PropertyChangedEventArgs e) {
           NotifyPropertyChanged("VM_" + e.PropertyName);
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
        public Pair VM_Coordinate
        {
            get
            {
                return model.Coordinate;
            } 
        }
    }
}
