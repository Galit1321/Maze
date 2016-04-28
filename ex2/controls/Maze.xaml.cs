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
        ViewModel vm;
        public string Order { get; set; }
      
        public Maze()
        {
            ViewModel vm = ViewModel.Instance;
            Order = vm.VM_MazeString;
            this.Initialized += delegate
            {
                init();
            };
            InitializeComponent();
            
            
        }

        public Maze(string yrivmaze)
        {
            InitializeComponent();
            ViewModel vm = ViewModel.Instance;
            Order = vm.VM_YrivMazeString;
            init();
        }
        
        public void init()
        {
            
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
            string mazeStr = Order;
            int x = 0;
            for (int i = 0; i < rowsNum; i++)
           {
                for (int j = 0; j < columNum; j++)
                {
                    Rectangle r = new Rectangle();
                    if (mazeStr[x] == '1')
                    {
                        r.Fill = new SolidColorBrush(Color.FromRgb(0, 10, 1));
                    Grid.SetRow(r, i);
                    Grid.SetColumn(r, j);
                    mazeGrid.Children.Add(r);
                    }
                    x++;
                }
            } }
        public void MarkClue(List<int> cell)
        {
            int x = cell.First() ;
            int row;
            int col;
            Rectangle r = new Rectangle();
            r.Fill= new SolidColorBrush(Colors.Aqua);
            foreach (int elem in cell)
            {
                row = x % mazeGrid.RowDefinitions.Count;
                col = x % mazeGrid.ColumnDefinitions.Count;
                Grid.SetRow(r, row);
                Grid.SetColumn(r, col);
                mazeGrid.Children.Add(r);
                
            }
        }

        
    }
}
