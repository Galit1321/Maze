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
    /// Interaction logic for Warning.xaml
    /// </summary>
    public partial class Warning : Window
    {
        /// <summary>
        /// warning.
        /// </summary>
        /// <param name="lab"></param>
        public Warning(string lab)
        {
            InitializeComponent();
            label.Content = lab;
        }
        /// <summary>
        /// warning.
        /// </summary>
        public Warning()
        {
            InitializeComponent();
        }
        /// <summary>
        /// close.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}