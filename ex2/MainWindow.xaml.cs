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
        private MediaPlayer song;
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
            Multiplayer.soundMulti += Play;
        }
        /// <summary>
        /// play.
        /// </summary>
        private void Play()
        {
            try
            {
                song = new MediaPlayer();
                string path = System.IO.Path.GetFullPath(".");
                path += "\\A Thousand years.mp3";
                song.Open(new Uri(path));
                //song.Load();
                song.MediaEnded += new EventHandler(Media_Ended);
                song.Play();

            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// media.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_Ended(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetFullPath(".");
            path += "\\A Thousand years.mp3";
            song.Open(new Uri(path));
            return;
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            Window s = new Setting();
            song.Stop();
            s.ShowDialog();
        }
        /// <summary>
        /// single.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            song.Stop();
            Window s = new SingelGame();
            s.ShowDialog();
        }
        /// <summary>
        /// multi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void multiButton_Click(object sender, RoutedEventArgs e)
        {
             song.Stop();
            Window m = new Gamename();
            m.ShowDialog();

        }
        /// <summary>
        /// close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Close(object sender, EventArgs e)
        {
            song.Stop();
            vm.Disconnect1();

        }

        private void popclose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

       
    }
}
