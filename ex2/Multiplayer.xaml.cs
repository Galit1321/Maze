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
using ex2.controls;

namespace ex2
{
    /// <summary>
    /// Interaction logic for Multiplayer.xaml
    /// </summary>
    public partial class Multiplayer : Window
    {
        ViewModel vm;
        public Multiplayer()
        {

            InitializeComponent();
            vm = ViewModel.Instance;
            DataContext = vm;
            UserControl m1 = new Maze();
            UserControl m2 = new Maze(vm.VM_YrivMazeString);
        }
    }
}
