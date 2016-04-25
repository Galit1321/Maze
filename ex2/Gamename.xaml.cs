using System;
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
        public Gamename()
        {
            InitializeComponent();
        }

        private void bntCnt_Click(object sender, RoutedEventArgs e)
        {
            vm = ViewModel.Instance;
            vm.Init(new Model(new TCPClient()));
            DataContext = vm;
            string g = Game_name.ToString();
            string res = vm.CreateGame(g);
            if (res.Equals("wait"))
            {
                MessageBoxResult result = MessageBox.Show("It seem you are the only one that want to play", "Single in Multiplayer");
               
            }
           
        }
    }
}
