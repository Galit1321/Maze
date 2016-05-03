using algoOnGraph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;

namespace ServerExe1.src.model
{
    /// <summary>
    /// contain the all mazes of the server, handle the saving and loading to file
    /// </summary>
    class AllMaze
    {
        private const string placeToSave = "saved_mazes.json";
        private const int defaultTypeMaze = 0;
        private const string postfixMazePlayerOne = "Maze1";
        private const string postfixMazePlayerTwo = "Maze2";
        private Hashtable mazes;
        
        /// <summary>
        /// c'tor' create
        /// </summary>
        public AllMaze()
        {
            mazes = new Hashtable();
            this.LoadMazes();
        }

        public Maze GetExistMaze(string nameMaze)
        {
            //if exist return it
            if (mazes.ContainsKey(nameMaze))
            {
                return this.mazes[nameMaze] as Maze;
            }
            return null;
        }

        /// <summary>
        /// get maze in according the name, if does't exist create according to the type
        /// </summary>
        /// <param name="nameMaze">the name of the maze</param>
        /// <param name="type">the type to create</param>
        /// <returns>retunr the maze</returns>
        public Maze GetMaze(string nameMaze, int type)
        {
            //if exist return it
            if (mazes.ContainsKey(nameMaze))
            {
                return this.mazes[nameMaze] as Maze;
            }
            //doesn't exist so create
            Maze newMaze = new Maze(nameMaze, type);
            mazes.Add(nameMaze, newMaze);
            return newMaze;
        }

        /// <summary>
        /// get the solurion of the maze, if doesn't exist solve according to the type.
        /// </summary>
        /// <param name="nameMaze">the maze name</param>
        /// <param name="type">the type to solve</param>
        /// <returns>the maze after solved it, if the maze doesn't exist return null</returns>
        public Maze GetSolutionMaze(string nameMaze, int type)
        {
            Maze returned = null;
            if (mazes.ContainsKey(nameMaze))
            {
                returned = this.mazes[nameMaze] as Maze;
                returned.SolveMaze(type);
                this.SolveSecIfHave(nameMaze);
                //save after solve
                this.SaveMazes();
            }
            return returned;
        }

        /// <summary>
        /// get mazes for game, if the match mazes doesn't exist create them
        /// </summary>
        /// <param name="nameGame">the game name</param>
        /// <returns>two mazes </returns>
        public Tuple<IMaze, IMaze> GetMazesForGame(string nameGame)
        {
            string nameGame1 = nameGame + AllMaze.postfixMazePlayerOne;
            string nameGame2 = nameGame + AllMaze.postfixMazePlayerTwo;
            Maze game1 = null;
            Maze game2 = null;
            //if the mazes are exist
            if (this.mazes.ContainsKey(nameGame1) && this.mazes.ContainsKey(nameGame2))
            {
                game1 = this.mazes[nameGame1] as Maze;
                game2 = this.mazes[nameGame2] as Maze;
            }
            //if just one exist, create the second with it
            else if (this.mazes.ContainsKey(nameGame1))
            {
                game2 = (this.mazes[nameGame1] as Maze).GetSecondMaze(nameGame2) as Maze;
                this.mazes.Add(nameGame2, game2);
            }
            else if (this.mazes.ContainsKey(nameGame2))
            {

                game1 = (this.mazes[nameGame2] as Maze).GetSecondMaze(nameGame1) as Maze;
                this.mazes.Add(nameGame1, game1);
            }
            //if no mazes match so create them both
            else
            {
                game1 = new Maze(nameGame1, AllMaze.defaultTypeMaze);
                game2 = game1.GetSecondMaze(nameGame2) as Maze;
                this.mazes.Add(nameGame1, game1);
                this.mazes.Add(nameGame2, game2);
            }
            return new Tuple<IMaze, IMaze>(game1, game2);
        }

        /// <summary>
        /// save the all mazes that have solution
        /// </summary>
        public void SaveMazes()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<Maze> toSave = new List<Maze>();
            //find the all mazes with solution
            foreach (string mazeName in this.mazes.Keys)
            {
                Maze maze = this.mazes[mazeName] as Maze;
                if (maze.IsSolved())
                {
                    toSave.Add(maze);
                }
            }
            //save them
            bool saved = false;
            while (saved == false)
            {
                try
                {
                    File.WriteAllText(AllMaze.placeToSave, ser.Serialize(toSave));
                    saved = true;
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Data.ToString());   
                }
            }
            
        }

        /// <summary>
        /// load all mazes form the file
        /// </summary>
        public void LoadMazes()
        {
            if (!File.Exists(placeToSave))
            {
                return;
            }
            //read
            string all = File.ReadAllText(AllMaze.placeToSave);

            JavaScriptSerializer ser = new JavaScriptSerializer();
            List<Maze> toLoad;
            //load
            toLoad = ser.Deserialize<List<Maze>>(all);
            if (toLoad == null) { return; }
            //add to the hashtable
            foreach (Maze maze in toLoad)
            {
                this.mazes.Add(maze.GetName(), maze);
            }
        }

        /// <summary>
        /// solve the second maze of game if haev
        /// </summary>
        /// <param name="nameMaze">the maze name</param>
        public void SolveSecIfHave(string nameMaze)
        {
            //check if the name is the first maze
            if (nameMaze.Substring(nameMaze.Length - AllMaze.postfixMazePlayerOne.Length).Equals(AllMaze.postfixMazePlayerOne))
            {
                string nameMazes = nameMaze.Substring(0,nameMaze.Length - AllMaze.postfixMazePlayerOne.Length);
                string nameSec = nameMazes +AllMaze.postfixMazePlayerTwo;
                Maze sec = this.mazes[nameSec] as Maze;
                if (sec == null)
                {
                    return;
                }
                
                if (!sec.IsSolved())
                {
                    sec.SolveMaze(AllMaze.defaultTypeMaze);
                }
            }
            //check if the name is the second maze
            else if (nameMaze.Substring(nameMaze.Length - AllMaze.postfixMazePlayerTwo.Length).Equals(AllMaze.postfixMazePlayerTwo))
            {
                string nameMazes = nameMaze.Substring(0, nameMaze.Length - AllMaze.postfixMazePlayerTwo.Length);
                string nameSec = nameMazes + AllMaze.postfixMazePlayerOne;
                Maze sec = this.mazes[nameSec] as Maze;
                if (sec == null)
                {
                    return;
                }
                //solve if needed
                if (!sec.IsSolved())
                {
                    sec.SolveMaze(AllMaze.defaultTypeMaze);
                }
            }
        }
    }
}
