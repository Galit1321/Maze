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

namespace View
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public static event MainWindow.SoundEvent soundSettings;
        public Setting()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            soundSettings();
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel vm = ViewModel.Instance;
            vm.NotifyPropertyChanged("VM_IP");
            vm.NotifyPropertyChanged("VM_Port");
            vm.VM_IP = IP.Text;
            vm.VM_Port = Int32.Parse(Port.Text);
            soundSettings();
            this.Close();
        }
        
    }
}
