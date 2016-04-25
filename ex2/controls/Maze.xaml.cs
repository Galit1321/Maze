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
using View;

namespace ex2.controls
{
    /// <summary>
    /// Interaction logic for Maze.xaml
    /// </summary>
    public partial class Maze : UserControl
    {
        public Maze()
        {
            InitializeComponent();
            init();
        }
        public string Order { get; set; }


        public void init()
        {
            ViewModel vm = ViewModel.Instance;
            int rowsNum = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            int columNum = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            rowsNum = (rowsNum * 2) - 1;
            columNum = (columNum * 2) - 1;
            //create the rows.
            for (int i = 0; i < rowsNum; i++)
            {
                RowDefinition rw1 = new RowDefinition();
                mazeGrid.RowDefinitions.Add(rw1);
            }
            //create the column
            for (int i = 0; i < columNum; i++)
            {
                ColumnDefinition c = new ColumnDefinition();
                mazeGrid.ColumnDefinitions.Add(c);
            }
            string mazeStr = vm.VM_MazeString;
            int x = 0;
            for (int i = 0; i < rowsNum; i++)
           {
                for (int j = 0; j < columNum; j++)
                {
                    Rectangle r = new Rectangle();
                    r.Stroke = new SolidColorBrush(Color.FromRgb(2, 2, 50));
                    if (mazeStr[x] == '0')
                        r.Fill = new SolidColorBrush(Color.FromRgb(0, 100, 0));
                    else if (mazeStr[x] == '1')
                        r.Fill = new SolidColorBrush(Color.FromRgb(100, 0, 0));
                    Grid.SetRow(r, i);
                    Grid.SetColumn(r, j);
                    mazeGrid.Children.Add(r);
                    x++;
                }
            }
           
            

        }
    }
}
