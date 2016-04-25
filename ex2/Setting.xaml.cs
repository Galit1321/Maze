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
    /// Interaction logic for Setting.xaml
    /// and init
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel vm = ViewModel.Instance;
            vm.NotifyPropertyChanged("VM_IP");
            vm.NotifyPropertyChanged("VM_Port");
            vm.VM_IP = IP.Text;
            vm.VM_Port = Int32.Parse(Port.Text);
            this.Close();
        }
        
    }
}
