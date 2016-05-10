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
    /// Interaction logic for Wining.xaml
    /// </summary>
    public partial class Wining : Window
    {
        MediaPlayer song;
        public Wining()
        {
            InitializeComponent();
            Play();
        }
        private void Play()
        {
            song = new MediaPlayer();
            string path = System.IO.Path.GetFullPath(".");
            path += "\\win.mp3";
            song.Open(new Uri(path));
            song.MediaEnded += new EventHandler(Media_Ended);
            song.Play();
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetFullPath(".");
            path += "\\win.mp3";
            song.Open(new Uri(path));
            return;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            song.Stop();
        }
    }
}
