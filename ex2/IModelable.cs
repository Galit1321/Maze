using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    interface IModelable: INotifyPropertyChanged
    {
        
            void connect(string ip, int port);
            void disconnect();
            void start();

            //proprties here
            int Port { get; set; }
            string IP { get; set; }
            string Maze { get; set; }
           Pair Coordinate { get; set; }
        string MazeName { get; set; }
            //active method
            void move(string direction);
            string getClue();
            void createMaze();

        
    }
}
