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
using System.Media;

namespace ex2
{
    /// <summary>
    /// Interaction logic for Multiplayer.xaml
    /// </summary>
    public partial class Multiplayer : Window
    {
        ViewModel vm;
        public static event MainWindow.SoundEvent soundMulti;
        private MediaPlayer song;
        public Multiplayer()
        {

            InitializeComponent();
            vm = ViewModel.Instance;
            DataContext = vm;
            vm.Open += OpenWin;
            Play();
            UserControl m1 = new Maze();
            UserControl m2 = new MazeOpponent();
        }
        public void OpenWin(string msn)
        {
            song.Stop();
            if (msn.Equals("won"))
            {
                Window w = new Wining();
                w.ShowDialog(); 
                Close();
            }
            if (msn.Equals("lost"))
            {
                Window w = new LoseWindow();
                w.ShowDialog();
                Close();
            }
             }
        private void Play()
        {
            try
            {
                song = new MediaPlayer();
                 string path = System.IO.Path.GetFullPath(".");
                 path += "\\I've Just Seen a Face .mp3";
                song.Open(new Uri(path));
                //song.Load();
                song.MediaEnded += new EventHandler(Media_Ended);
                song.Play();
                
            }
            catch (Exception)
            {

            }
        }

        private void Media_Ended(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetFullPath(".");
            path += "\\Titanium - Pavane.mp3";
            song.Open(new Uri(path));
            return;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            soundMulti();
            song.Close();
            vm.Open -= OpenWin;
           vm.closeGame(string.Empty);
        }
        /// <summary>
        /// get back to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();

            }
        }
        /// <summary>
        /// get clue to the solution the server suggested
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clue_Click(object sender, RoutedEventArgs e)
        {
            vm.GetClue();
        }
        
       /// <summary>
       /// move the player 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                vm.move("down");
            }
            else if (e.Key == Key.Up)
            {
                vm.move("up");
            }
            else if (e.Key == Key.Left)
            {
                vm.move("left");
            }
            else if (e.Key == Key.Right)
            {
                vm.move("right");
            }
        }
        /// <summary>
        /// reset game to staring point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("are u sure? with this action you are playing by your self", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                vm.RestMaz();
            }            
        }
    }
}
