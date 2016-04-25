using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    public interface IModelable: INotifyPropertyChanged
    {
        
            void connect(string ip, int port);
            void disconnect();
            void start();

            //proprties here
            int Port { get; set; }
            string IP { get; set; }
            string MazeString { get; set; }
            string YrivMazeString { get; set; }
           string YrivMazeName { get; set; }
           Pair Coordinate { get; set; }
           Pair Yriv_Cor { get; set; }

        string MazeName { get; set; }
        bool Winner { get; set; }
        bool Loser { get; set; }
            //active method
            void move(string direction,Pair cor);
            List<int> getClue();
            void createMaze();
            string CreateGame(string name);
        
    }
}
