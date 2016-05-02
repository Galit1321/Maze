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
        private SoundPlayer song;
        public Multiplayer()
        {

            InitializeComponent();
            vm = ViewModel.Instance;
            DataContext = vm;
            Play();
            UserControl m1 = new Maze();
            UserControl m2 = new Maze();
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
    }
}
