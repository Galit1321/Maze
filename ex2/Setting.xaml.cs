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
        private SoundPlayer song;
        public Setting()
        {
            vm = ViewModel.Instance;
            Play();
            vm.Init(new Model(new TCPClient()));
            DataContext = vm;
            InitializeComponent();

        }

        private void Play()
        {
            try
            {
                song = new System.Media.SoundPlayer();
                string path = System.IO.Path.GetFullPath(".");
                path += "\\Titanium.wav";
                song.SoundLocation = path;
                song.Load();
                song.Play();
                song.PlayLooping();
            }
            catch (Exception)
            {

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel vm = ViewModel.Instance;
            vm.VM_IP = IP.Text;
            vm.VM_Port = Int32.Parse(Port.Text);
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings.Remove("IP");
            configuration.AppSettings.Settings.Add("IP", IP.Text);
            configuration.AppSettings.Settings.Remove("Port");
            configuration.AppSettings.Settings.Add("Port", Port.Text);
            //          configuration.AppSettings.Settings["IP"].Value = IP.Text;
            //          configuration.AppSettings.Settings["Port"].Value = Port.Text;
            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            this.Close();
        }

        private void Close_Window(object sender, EventArgs e)
        {
            song.Stop();
            soundSettings();
        }
    }
}
