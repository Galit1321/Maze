using Mazelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// A solved maze
    /// </summary>
    public class MazeSolved
    {
        public string Name { get; set; }
        public string Maze { get; set; }
        public string Sol { get; set; }
        public Location Start { get; set; }
        public Location End { get; set; }
        private MazeByGraph originalMaze;
        private Solution<IVertex> originalSol; 

        /// <summary>
        /// empty constructor
        /// </summary>
        public MazeSolved(){}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name"> name of maze </param>
        /// <param name="originalMaze"> the maze</param>
        /// <param name="start"> start point </param>
        /// <param name="end"> end point </param>
        public MazeSolved(string name,MazeByGraph originalMaze, Location start, Location end)
        {
            this.Name = name;
            this.originalMaze = originalMaze;
            this.Start = start;
            this.End = end;
            this.Maze = originalMaze.ToString();
        }

        /// <summary>
        /// set the solution
        /// </summary>
        /// <param name="originalSol"> the solution </param>
        public void setSol(Solution<IVertex> originalSol)
        {
            this.originalSol = originalSol;
            SolToString();
        }

        /// <summary>
        /// get the original maze
        /// </summary>
        /// <returns> returns the maze </returns>
        public MazeByGraph GetMaze()
        {
            return this.originalMaze;
        }

        /// <summary>
        /// get string of the solution 
        /// </summary>
        public void SolToString()
        {
            int width = this.originalMaze.GetWidth();
            int height = this.originalMaze.Gethight();
            char[,] maze = new char[width * 2 - 1, height * 2 - 1];
            string s = "";
            List<State<IVertex>> stateList = this.originalSol.states;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    IVertex temp = this.originalMaze.GetIVertex().Find(v => v.GetLocation().Equals(new Location(i, j)));
                    State<IVertex> v1 = stateList.Find(v => v.state.Equals(temp));
                    if (v1 != null)
                    {
                        maze[i * 2, j * 2] = '2';
                    }
                    else
                    {
                        maze[i * 2, j * 2] = '0';
                    }
                    // if the vertex is not in the adj
                    if (j != width - 1)
                    {
                        if (temp.GetAdj()[(int)direction.RIGHT] != null)
                        {
                            IVertex nextRight = this.originalMaze.GetIVertex().Find(v => v.GetLocation().Equals(new Location(i, j + 1)));
                            State<IVertex> nextState = stateList.Find(v => v.state.Equals(nextRight));
                            if (nextState != null && v1 != null)
                            {
                                maze[i * 2, j * 2 + 1] = '2';
                            }
                            else
                            {
                                maze[i * 2, j * 2 + 1] = '0';
                            }
                            
                        }
                        else
                        {
                            maze[i * 2, j * 2 + 1] = '1';
                        }

                    }
                    if (i != height - 1)
                    {
                        if (temp.GetAdj()[(int)direction.DOWN] != null)
                        {
                            IVertex nextDown = this.originalMaze.GetIVertex().Find(v => v.GetLocation().Equals(new Location(i + 1, j)));
                            State<IVertex> nextState = stateList.Find(v => v.state.Equals(nextDown));
                            if (nextState != null && v1 != null)
                            {
                                maze[i * 2 + 1, j * 2] = '2';
                            }
                            else
                            {
                                maze[i * 2 + 1, j * 2] = '0';
                            }
                        }
                        else
                        {
                            maze[i * 2 + 1, j * 2] = '1';
                        }
                    }
                    if (i != height - 1 && j != width - 1)
                    {
                        maze[i * 2 + 1, j * 2 + 1] = '1';
                    }

                }
            }
            for (int i = 0; i < width * 2 - 1; i++)
            {
                for (int j = 0; j < height * 2 - 1; j++)
                {
                    s += maze[i, j];
                }

             }
            this.Sol = s;
        }
    }
}
