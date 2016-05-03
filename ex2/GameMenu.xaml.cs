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

namespace ex2
{
    /// <summary>
    /// Interaction logic for Gamename.xaml
    /// </summary>
    public partial class Gamename : Window
    {
        public ViewModel vm;
        public Window War;
        public Gamename()
        {
            vm = ViewModel.Instance;
            InitializeComponent();
            vm.Open += OpenWin;
            vm.Close += CloseWin;

        }

        private void bntCnt_Click(object sender, RoutedEventArgs e)
        {
            
            vm.Init(new Model(new TCPClient()));
            DataContext = vm;
            string g = Game_name.ToString();
            vm.CreateGame(g);
           
           
        }
        public void OpenWin(string msn)
        {
            War = new Warning(msn);
            War.Show();

        }
        public void CloseWin()
        {
            Dispatcher.Invoke(() => {//invike the right thread to change ui
                Window m = new Multiplayer();
                m.Show();
                if (War != null) {
                    War.Close();
                }
                this.Close();
            });
            

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            vm.Open -= OpenWin;
            vm.Close -= CloseWin;
        }
    }
}
