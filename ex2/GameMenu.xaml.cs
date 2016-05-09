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
        public Window War;
        private MediaPlayer song;
        public Gamename()
        {
            vm = ViewModel.Instance;
            InitializeComponent();
            Play();
            vm.Open += OpenWin;
            vm.Close += CloseWin;
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
                //song.Load();
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
            DataContext = vm;
            string g = Game_name.ToString();
            vm.CreateGame(g);
           
           
        }
        /// <summary>
        /// Open window.
        /// </summary>
        /// <param name="msn"></param>
        public void OpenWin(string msn)
        {
            War = new Warning(msn);
            War.Show();

        }
        /// <summary>
        /// close window.
        /// </summary>
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

        /// <summary>
        /// Close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            vm.Open -= OpenWin;
            vm.Close -= CloseWin;
            song.Stop();
        }
    }
}
