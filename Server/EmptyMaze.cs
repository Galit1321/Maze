using Mazelib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
enum direction { UP, RIGHT, DOWN, LEFT }
namespace Server.Modle
{
    /// <summary>
    /// An empty maze
    /// </summary>
    public class EmptyMaze
    {
        /// <summary>
        /// empty constructor
        /// </summary>
        public EmptyMaze() {}

        /// <summary>
        /// Get an empty maze
        /// </summary>
        /// <returns> returns empty maze </returns>
        public MazeByGraph GetEmptyMaze()
        {
            int width = Int32.Parse(ConfigurationManager.AppSettings["Width"]);
            int height = Int32.Parse(ConfigurationManager.AppSettings["Height"]);
            //init
            List<IVertex> maze = new List<IVertex>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    IVertex v = new Vertex(new Location(i, j), 1);
                    // treate the adg
                    if (i == 0)
                    {
                        v.AddNeighbor(new Vertex(new Location(-1, j), -1), (int)direction.UP);
                    }
                    if (j == 0)
                    {
                        v.AddNeighbor(new Vertex(new Location(i, -1), -1), (int)direction.LEFT);
                    }
                    if (i == width - 1)
                    {
                        v.AddNeighbor(new Vertex(new Location(width, j), -1), (int)direction.DOWN);
                    }
                    if (j == height - 1)
                    {
                        v.AddNeighbor(new Vertex(new Location(i, height), -1), (int)direction.RIGHT);
                    }
                    maze.Add(v);
                }
            }
            MazeByGraph emptyMaze = new MazeByGraph(width, height, maze);
            return emptyMaze;
        }
    }
}
