using Mazelib;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// MultiPlayer command, implements ICommand
    /// </summary>
    class Multiplayer : ICommand
    {
        private string[] args;
        private string result;
        private int TaskId;
        public event taskUpdate commendChange;

        /// <summary>
        /// handle
        /// </summary>
        public void handle()
        {
            DataBase db = DataBase.Instance;
            if (!db.Games.ContainsKey(args[1]))
            {
                Game game = new Game();
                game.Name = args[1];
                EmptyMaze empty = new EmptyMaze();
                MazeByGraph maze = empty.GetEmptyMaze();
                ICreator creator;
                creator = new RandomCreator();
                creator.Create(maze);
                string s = args[1] + "_1";
                MazeSolved mazeAndSol = new MazeSolved(s, maze, maze.GetStart().GetLocation(), maze.GetEnd().GetLocation());
                db.mazeSolve.Add(s,mazeAndSol);
                game.Maze1 = mazeAndSol;
                game.Client1 = TaskId;
                db.Games.Add(args[1], game);
                commendChange(game.Client1, "one player");
            }
            else
            {
                Game game = db.Games[args[1]];
                game.Client2 = TaskId;
                MazeByGraph maze2= new MazeByGraph(game.Maze1.GetMaze());
                maze2.SetNewStart();
                string s = args[1] + "_2"; 
                game.Maze2 = new MazeSolved(s, maze2, maze2.GetStart().GetLocation(), maze2.GetEnd().GetLocation());
                db.mazeSolve.Add(s, game.Maze2);
                MultiplayerAnswer answer1 = new MultiplayerAnswer(game, game.Maze1.Name);
                SerializeAnswer ser = new SerializeAnswer(3, answer1);
                string s1 = ser.Serialize();
                commendChange(game.Client1, s1);
                MultiplayerAnswer answer2 = new MultiplayerAnswer(game, game.Maze2.Name);
                SerializeAnswer ser2 = new SerializeAnswer(3, answer2);
                string s2 = ser2.Serialize();
                commendChange(game.Client2, s2);

            }
        }

        /// <summary>
        /// Set the arguments
        /// </summary>
        /// <param name="args"> the args to set </param>
        public void SetArgs(string[] args)
        {
            this.args = args;
        }

        /// <summary>
        /// create new multiplayer command
        /// </summary>
        /// <returns> returns new multiplayer command </returns>
        public ICommand NewCommand()
        {
            return new Multiplayer();
        }

        /// <summary>
        /// Set client id
        /// </summary>
        /// <param name="id"> id of client to set </param>
        public void setClientId(int id)
        {
            this.TaskId = id;
        }

       
    }
}
