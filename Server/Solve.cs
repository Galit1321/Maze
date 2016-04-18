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
    /// Solve implements Icommand
    /// </summary>
    class Solve : ICommand
    {
        public string[] args;
        public string result;
        private int TaskId;
        public event taskUpdate commendChange;

        /// <summary>
        /// handle this command
        /// </summary>
        public void handle()
        {
            DataBase db = DataBase.Instance;

            if (!db.mazeSolve.ContainsKey(args[1]))
            {
                this.result = "maze not exist";
            }
            else
            {
                MazeSolved maze = db.mazeSolve[args[1]];
                if (maze.Sol == null)
                {
                    ISearcher<IVertex> search;
                    if (Convert.ToDecimal(args[2]) == (int)type.RANDOM)
                    {
                        search = new Bfs<IVertex>();
                    }
                    else
                    {
                        search = new BestFS<IVertex>();
                    }

                    maze.setSol(search.Search(maze.GetMaze()));
                    db.Write();
                }
                GeneratorAnswer gen = new GeneratorAnswer(maze);
                gen.Maze = maze.Sol;
                SerializeAnswer ser = new SerializeAnswer(2, gen);
                this.result = ser.Serialize();

            }
            commendChange(this.TaskId, this.result);
        }

        /// <summary>
        /// Set the arguments
        /// </summary>
        /// <param name="args"> the arguments to set </param>
        public void SetArgs(string[] args)
        {
            this.args = args;
        }

        /// <summary>
        /// create this command
        /// </summary>
        /// <returns> return new solve command </returns>
        public ICommand NewCommand()
        {
            return new Solve();
        }

        /// <summary>
        /// set the id of relevant client
        /// </summary>
        /// <param name="id"> id </param>
        public void setClientId(int id)
        {
            this.TaskId = id;
        }
    }
}
