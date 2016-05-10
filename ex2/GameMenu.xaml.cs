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
        public static event MainWindow.SoundEvent soundWait;
        public ViewModel vm;
        public Window War;
        private MediaPlayer song;
        public Gamename()
        {
            vm = ViewModel.Instance;
            DataContext = vm;
            InitializeComponent();
            Play();
    }
        /// <summary>
        /// Play song.
        /// </summary>
        private void Play()
        {
            try
            {
                song = new MediaPlayer();
                string path = System.IO.Path.GetFullPath(".");
                path += "\\Titanium - Pavane.mp3";
                song.Open(new Uri(path));
                song.MediaEnded += new EventHandler(Media_Ended);
                song.Play();

            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// repeat the song.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_Ended(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetFullPath(".");
            path += "\\Titanium - Pavane.mp3";
            song.Open(new Uri(path));
            return;
        }

        /// <summary>
        /// Creating game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bntCnt_Click(object sender, RoutedEventArgs e)
        {
            string g = Game_name.Text;
            vm.CreateGame(g);
        }


        /// <summary>
        /// close pop up window.
        /// </summary>
        private void wait_Closed(object sender, EventArgs e)
        {
            if (!vm.VM_Wait)
            {
                Window m = new Multiplayer();
                m.Show();
                this.Close();
            }

        }

        /// <summary>
        /// Close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            song.Stop();
        }

     /// <summary>
     /// we click and gave up on waiting to second player
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            vm.VM_Wait = false;
            vm.closeGame(Game_name.Text);
            soundWait();
        }

        private void wait_Opened(object sender, EventArgs e)
        {
            song.Stop();
            PlayPop();
        }
        private void PlayPop()
        {
            string path = System.IO.Path.GetFullPath(".");
            path += "\\John Mayer - Waiting On The World To Change (mp3cut.net).mp3";
            song.Open(new Uri(path));
            song.Play();
            return;
        }
    }
}
