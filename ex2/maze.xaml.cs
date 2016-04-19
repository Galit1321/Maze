using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace ex2
{
    /// <summary>
    /// Interaction logic for maze.xaml
    /// </summary>
    public partial class maze : UserControl
    {
        public maze()
        {
            InitializeComponent();
        }
        public void init()
        {
            int rowsNum = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            int columNum = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            Grid g = new Grid();
            //create the rows.
            for (int i = 0; i < rowsNum; i++)
            {
                RowDefinition rw1 = new RowDefinition();
                g.RowDefinitions.Add(rw1);
            }
            //create the column
            for (int i = 0; i < columNum; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                g.ColumnDefinitions.Add(c);
            }
        }
    }
}
