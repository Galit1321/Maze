using Mazelib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Maze by graph
    /// </summary>
    public class MazeByGraph : IMaze<IVertex>
    {
        private List<IVertex> vertix;
        private IVertex start;
        private IVertex end;
        private int width;
        private int height;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="width"> width </param>
        /// <param name="height"> height </param>
        /// <param name="vertexis"> vertix </param>
        public MazeByGraph(int width, int height ,List<IVertex> vertexis)
        {
            this.width = width;
            this.height = height;
            this.vertix = vertexis;
         }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="other"> other maze to copy </param>
        public MazeByGraph(MazeByGraph other)
        {
            this.width = other.width;
            this.height = other.height;
            this.start = new Vertex((Vertex)other.start);
            this.end = new Vertex((Vertex)other.end);
            this.vertix = new List<IVertex>();
            IVertex v;
            foreach (IVertex vother in other.vertix)
	        {
                v = new Vertex((Vertex)vother);
		        this.vertix.Add(v);
	        }

        }

        /// <summary>
        /// set start and end point
        /// </summary>
        /// <param name="start"> new start point </param>
        /// <param name="end"> new end point </param>
        public void SetStartEndPoints(IVertex start, IVertex end)
        {
            this.start = start;
            this.end = end;
        }

        /// <summary>
        /// set random start point
        /// </summary>
        /// <returns> returns the new point </returns>
        public IVertex SetNewStart()
        {
            Random rand = new Random();
            int x, y;
            Location loc;
            do
            {
                x = rand.Next(width);
                y = rand.Next(height);
                loc = new Location(x, y);
            } while (x == this.end.GetLocation().Row || y == this.end.GetLocation().Col);
            this.start = new Vertex(loc, 1);
            return this.start;
        }

        /// <summary>
        /// get this start point
        /// </summary>
        /// <returns> the start point </returns>
        public IVertex GetStart()
        {
            return this.start;
        }

        /// <summary>
        /// get this end point
        /// </summary>
        /// <returns> the end point </returns>
        public IVertex GetEnd()
        {
            return this.end;
        }

        /// <summary>
        /// get this list of vertix
        /// </summary>
        /// <returns> returns list of vertix </returns>
        public List<IVertex> GetIVertex()
        {
            return this.vertix;
        }

        /// <summary>
        /// get the initial state
        /// </summary>
        /// <returns> returns the initial state </returns>
        public State<IVertex> GetInitialState()
        {
            return new State<IVertex>(this.start);
        }

        /// <summary>
        /// get this goal state
        /// </summary>
        /// <returns> returns th goal state </returns>
        public State<IVertex> GetGoalState()
        {
            return new State<IVertex>(this.end);
        }

        /// <summary>
        /// get height
        /// </summary>
        /// <returns> returns this height </returns>
        public int Gethight()
        {
            return this.height;
        }

        /// <summary>
        /// get width
        /// </summary>
        /// <returns> return this width </returns>
        public int GetWidth()
        {
            return this.width;
        }

        /// <summary>
        /// get all possible states
        /// </summary>
        /// <param name="s"> the state to start from </param>
        /// <returns> returns list of possible states </returns>
        public List<State<IVertex>> GetAllPossibleStates(State<IVertex> s)
        {
            List<State<IVertex>> possibleStates = new List<State<IVertex>>();
            if ((s.state as IVertex).GetAdj()[(int)direction.UP] != null && (s.state as IVertex).GetAdj()[(int)direction.UP].GetValue() != -1)
            {
                possibleStates.Add(new State<IVertex>((s.state as IVertex).GetAdj()[(int)direction.UP]));
            }
            if ((s.state as IVertex).GetAdj()[(int)direction.RIGHT] != null && (s.state as IVertex).GetAdj()[(int)direction.RIGHT].GetValue() != -1)
            {
                possibleStates.Add(new State<IVertex>((s.state as IVertex).GetAdj()[(int)direction.RIGHT]));
            }
            if ((s.state as IVertex).GetAdj()[(int)direction.DOWN] != null && (s.state as IVertex).GetAdj()[(int)direction.DOWN].GetValue() != -1)
            {
                possibleStates.Add(new State<IVertex>((s.state as IVertex).GetAdj()[(int)direction.DOWN]));
            }
            if ((s.state as IVertex).GetAdj()[(int)direction.LEFT] != null && (s.state as IVertex).GetAdj()[(int)direction.LEFT].GetValue() != -1)
            {
                possibleStates.Add(new State<IVertex>((s.state as IVertex).GetAdj()[(int)direction.LEFT]));
            }
            return possibleStates;
        }

        /// <summary>
        /// Maze to string
        /// </summary>
        /// <returns> returns maze by string </returns>
        public override string ToString()
        {
            char[,] maze = new char[width * 2 - 1, height * 2 - 1];
            string s = "";
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    IVertex temp = vertix.Find(v => v.GetLocation().Equals(new Location(i, j)));
                    maze[i * 2, j * 2] = '0';
                    // if the vertex is not in the adj
                    if (j != width - 1)
                    {
                        if (temp.GetAdj()[(int)direction.RIGHT] != null)
                        {
                            maze[i * 2, j * 2 + 1] = '0';
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
                            maze[i * 2 + 1, j * 2] = '0';
                        }
                        else
                        {
                            maze[i * 2 + 1, j * 2] = '1';
                        }
                    }
                    // if in the corner
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
            return s;
        }
    }
}
