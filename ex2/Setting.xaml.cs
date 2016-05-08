using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Media;
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
    /// Interaction logic for Setting.xaml
    /// and init
    /// </summary>
    public partial class Setting : Window
    {
        ViewModel vm;
        public static event MainWindow.SoundEvent soundSettings;
        private MediaPlayer song;

       /// <summary>
        /// constructor
        /// </summary>
        public Setting()
        {
            vm = ViewModel.Instance;
            Play();
            DataContext = vm;
            InitializeComponent();
            
        }
        /// <summary>
        /// play music
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
        /// song has ended so play it from start 
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
        /// cancel the change in text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// save chage made and update binding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel vm = ViewModel.Instance;
            vm.Disconnect1();
            vm.ChangeApp(IP.Text, Port.Text);
            BindingExpression ip_be = IP.GetBindingExpression(TextBox.TextProperty);//call to update IP.text field
            ip_be.UpdateSource();
            BindingExpression Port_be = IP.GetBindingExpression(TextBox.TextProperty);//call to update Port.text field
            Port_be.UpdateSource();
            vm.Connect();
            this.Close();
           
        }
        /// <summary>
        /// event of this window close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Window(object sender, EventArgs e)
        {
            song.Stop();
            soundSettings();

        }
        
    }
}
