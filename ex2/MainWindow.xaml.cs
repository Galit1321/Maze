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
using View;

namespace ex2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel vm;
        private SoundPlayer song;
        public delegate void SoundEvent();
        public MainWindow()
        {
            InitializeComponent();
            vm = ViewModel.Instance;
            vm.Init(new Model(new TCPClient()));
            DataContext = vm;
            Play();
            SingelGame.soundMain += Play;
            ex2.Setting.soundSettings += Play;
        }

        private void Play()
        {
            try
            {
                song = new System.Media.SoundPlayer();
                string path = Path.GetFullPath(".");
                path += "\\Without You.wav";
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
            song.Stop();
            s.ShowDialog();
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            song.Stop();
            vm.CreateSingle();
            Window s = new SingelGame();
            s.ShowDialog();
        }

        private void multiButton_Click(object sender, RoutedEventArgs e)
        {
             song.Stop();
            Window m = new Gamename();
            m.ShowDialog();

        }
    }
}
