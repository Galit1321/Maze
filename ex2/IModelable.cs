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
            string MazeString { get; set; }
           Pair Coordinate { get; set; }
           Pair Yriv_Cor { get; set; }
        string MazeName { get; set; }
        bool Winner { get; set; }
            //active method
            void move(string direction,Pair cor);
            string getClue();
            void createMaze();
            string CreateGame(string name);
        
    }
}
