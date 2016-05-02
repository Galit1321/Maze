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
        int MyRow { get; set;  }
        int MyCol { get; set; }
        int EndRow { get; set; }
        int EndCol { get; set; }
        int YrivRow { get; set; }
        int YrivCol { get; set; }
        int EndYrivRow { get; set; }
        int EndYrivCol { get; set; }
        Pair Coordinate { get; set; }
           Pair Yriv_Cor { get; set; }

        string MazeName { get; set; }
        bool Winner { get; set; }
        bool Loser { get; set; }
        bool InSession { get; set; }
            //active method
            void move(int direction);
            List<int> getClue();
            void createMaze();
            string CreateGame(string name);
        void StartGame(string ans);
        void Waiting();
        void RestGame();
        void closeGame();

    }
}
