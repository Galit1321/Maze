﻿using System;
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
        int VM_Port { get; set; }
        string VM_IP { get; set; }
        string VM_MazeString { get; set; }
        Pair VM_Coordinate
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
