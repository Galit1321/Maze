using ex2;
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
using System.Windows.Navigation;
using System.Media;
using System.IO;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = ViewModel.Instance;
            vm.Init(new Model(new TCPClient()));
            DataContext = vm;
            SoundPlayer song;
            try
            {
                song = new System.Media.SoundPlayer();
                string path = Path.GetFullPath(".");
                path += "\\BeginAgain.wav";
                song.SoundLocation = path;
                song.Load();
                song.Play();
            }
            catch (Exception)
            {

            }
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Window s = new Setting();
            s.ShowDialog();
        }

        private void SingalPlayer_Click(object sender, RoutedEventArgs e)
        {
            Window s = new SingalGame();
            s.ShowDialog();
        }
    }
}
