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
        private SoundPlayer song;
        public Multiplayer()
        {

            InitializeComponent();
            vm = ViewModel.Instance;
            DataContext = vm;
            Play();
            UserControl m1 = new Maze();
            UserControl m2 = new MazeOpponent();
        }

        private void Play()
        {
            try
            {
                song = new System.Media.SoundPlayer();
                string path = System.IO.Path.GetFullPath(".");
                path += "\\A Thousand years.wav";
                song.SoundLocation = path;
                song.Load();
                song.Play();
                song.PlayLooping();
            }
            catch (Exception)
            {

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            song.Stop();
           vm.closeGame();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to close this window?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                soundMulti();
                song.Stop();
                vm.closeGame();
                Close();

            }
        }
        private void Clue_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            vm.RestMaz();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                vm.move(2);
            }
            else if (e.Key == Key.Up)
            {
                vm.move(1);
            }
            else if (e.Key == Key.Left)
            {
                vm.move(4);
            }
            else if (e.Key == Key.Right)
            {
                vm.move(3);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                vm.move(2);
            }
            else if (e.Key == Key.Up)
            {
                vm.move(1);
            }
            else if (e.Key == Key.Left)
            {
                vm.move(4);
            }
            else if (e.Key == Key.Right)
            {
                vm.move(3);
            }
        }
    }
}
