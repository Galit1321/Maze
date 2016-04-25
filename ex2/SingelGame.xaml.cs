﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ex2.controls;

namespace ex2
{
    /// <summary>
    /// Interaction logic for SingalGame.xaml
    /// </summary>
    public partial class SingelGame : Window
    {
        public ViewModel vm;
        public static event MainWindow.SoundEvent soundMain;
        public SingelGame()
        {
            InitializeComponent();
            UserControl m = new Maze();
            vm = ViewModel.Instance;
            vm.Init(new Model(new TCPClient()));
            DataContext = vm;
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            soundMain();
            Close();
        }

        
    }
}
