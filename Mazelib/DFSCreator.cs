using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// create maze by DFS
    /// </summary>
    public class DFSCreator : Creator
    {
        /// <summary>
        /// implement abstract helper function
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="startIndex"></param>
        public override void CreateHelper(int width, int height, int startIndex)
        {
            Stack<IVertex> stack = new Stack<IVertex>();
            int numberVerex = 0;
            IVertex current = maze[startIndex];
            Dictionary<int, IVertex> notVisit;
            Random rand = new Random();
            List<IVertex> endPoints = new List<IVertex>();
            IVertex temp;
            int index;
            bool deadlock = true;
            while (numberVerex < width * height - 1)
            {
                if (current.IsViste() == false)
                {
                    current.SetVisit(true);
                    numberVerex++;
                }
               
                notVisit = NotVisted(current);
                //break wall to neighber
                if (notVisit != null)
                {
                    stack.Push(current);
                    deadlock = true;
                    List<int> option = notVisit.Keys.ToList<int>();
                    index = rand.Next(option.Count);
                    int direct = option[index];
                    temp = notVisit[direct];
                    //connect the vertex
                    temp.AddNeighbor(current, (direct + 2) % 4);
                    current.AddNeighbor(temp, direct);
                    current = temp;
                }
                //there is no neighber that not visted
                else if (stack.Count != 0)
                {
                    if (deadlock)
                    {
                        endPoints.Add(current);
                        deadlock = false;
                    }
                    current = stack.Pop();
                }
            }
            if (endPoints.Count == 0)
            {
                endPoints.Add(current);
            }
            index = rand.Next(endPoints.Count);
            end = endPoints[index];


        }

        /// <summary>
        /// return the not visited vertix
        /// </summary>
        /// <param name="current"></param>
        /// <returns> dictionary of not visited vertex </returns>
        public Dictionary<int, IVertex> NotVisted(IVertex current)
        {
            List<int> wall = maze.Find(v => v == current).GetWall();
            Dictionary<int, IVertex> notVisted = new Dictionary<int, IVertex>();
            IVertex temp;
            //go over for the walls and check if it between vertex that was visted already
            for (int i = 0; i < wall.Count; i++)
            {
                if (wall[i] == (int)direction.UP)
                {

                    temp = maze.Find(v => v.GetLocation().Equals(new Location(current.GetLocation().Row - 1, current.GetLocation().Col)));
                    if (!temp.IsViste())
                    {
                        notVisted.Add((int)direction.UP, temp);
                    }
                }
                else if (wall[i] == (int)direction.RIGHT)
                {
                    temp = maze.Find(v => v.GetLocation().Equals(new Location(current.GetLocation().Row, current.GetLocation().Col + 1)));
                    if (!temp.IsViste())
                    {
                        notVisted.Add((int)direction.RIGHT, temp);
                    }
                }
                else if (wall[i] == (int)direction.DOWN)
                {
                    temp = maze.Find(v => v.GetLocation().Equals(new Location(current.GetLocation().Row + 1, current.GetLocation().Col)));
                    if (!temp.IsViste())
                    {
                        notVisted.Add((int)direction.DOWN, temp);
                    }
                }
                else if (wall[i] == (int)direction.LEFT)
                {
                    temp = maze.Find(v => v.GetLocation().Equals(new Location(current.GetLocation().Row, current.GetLocation().Col - 1)));
                    if (!temp.IsViste())
                    {
                        notVisted.Add((int)direction.LEFT, temp);
                    }
                }
            }
            if (notVisted.Count == 0)
            {
                return null;
            }
            return notVisted;
        }
    }
}
