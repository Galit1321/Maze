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
        int ClueRow { get; set; }
        int ClueCol { get; set; }
        bool NeedClue { get; set; }
        string MazeName { get; set; }
        bool Winner { get; set; }
        bool Loser { get; set; }
       bool Disconnection { get; set; }
        bool Wait { get; set; }
        //active method
        void ChangeApp(string newIP,string port);
            void move(string direction);
            void getClue();
            void createMaze();
            string CreateGame(string name);
        void StartGame();
        void Waiting();
        void RestGame();
        void closeGame(string name);
        void closeSingle();

    }
}
