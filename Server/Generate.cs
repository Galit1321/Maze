using Mazelib;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    enum type {RANDOM, DFS};
    /// <summary>
    /// Generate command implements ICommand
    /// </summary>
    class Generate : ICommand
    {
        private string[] args;
        private string result;
        private int TaskId;

        /// <summary>
        /// handle generate
        /// </summary>
        public void handle()
        {
            EmptyMaze empty = new EmptyMaze();
            MazeByGraph maze = empty.GetEmptyMaze();
            ICreator creator;
            if (Convert.ToDecimal(args[2]) == (int)type.RANDOM)
            {
                creator = new RandomCreator();
            }
            else
            {
                creator = new DFSCreator();
            }
            DataBase db = DataBase.Instance;
            if (db.mazeSolve.ContainsKey(args[1]))
            {
                this.result = "maze already exist";
            }
            else
            {
                creator.Create(maze);
                MazeSolved mazeAndSol = new MazeSolved(args[1], maze, maze.GetStart().GetLocation(), maze.GetEnd().GetLocation());
                //get the data base class
                //add the maze and the name to the list of mazes
                db.mazeSolve.Add(args[1], mazeAndSol);

                GeneratorAnswer answer = new GeneratorAnswer(mazeAndSol);
                SerializeAnswer ser = new SerializeAnswer(1, answer);
                this.result = ser.Serialize();
            }
            commendChange( this.TaskId, this.result);
        }

        /// <summary>
        /// Set the args
        /// </summary>
        /// <param name="args"> args to set </param>
        public void SetArgs(string[] args)
        {
            this.args = args;
        }

        /// <summary>
        /// create new command
        /// </summary>
        /// <returns> returns generate command </returns>
        public ICommand NewCommand()
        {
            return new Generate();
        }

        /// <summary>
        /// Set client id
        /// </summary>
        /// <param name="id"> id </param>
        public void setClientId(int id)
        {
            this.TaskId = id;
        }

        public event taskUpdate commendChange;
    }
}
