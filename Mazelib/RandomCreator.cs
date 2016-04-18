using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// 
    /// </summary>
    public class RandomCreator : Creator
    {
        /// <summary>
        /// implements abstract helper function
        /// </summary>
        /// <param name="width"> the width </param>
        /// <param name="height"> the height </param>
        /// <param name="startIndex"> the point to start from </param>
        public override void CreateHelper(int width, int height, int startIndex)
        {
            // list of vertix
            List<IVertex> vertix = maze;
            // list of neighbours
            List<int> nei = new List<int>();
            // list of sets of vertex
            List<List<IVertex>> sets = new List<List<IVertex>>();
            Random rand = new Random();

            foreach (IVertex v in maze)
            {
                List<IVertex> l = new List<IVertex>();
                l.Add(v);
                sets.Add(l);
            }
            int count = 0;
            while (sets.Count > 1)
            {
                int i = rand.Next(vertix.Count);
                IVertex current = vertix[i];
                List<int> walls = current.GetWall();
                if (walls.Count == 0)
                {
                    continue;
                }
                int index = rand.Next(walls.Count);

                int direct = walls[index];
                IVertex v = null;
                switch (direct)
                {
                    case 0: //up
                        v = maze.Find(u => (u.GetLocation().Equals(new Location(current.GetLocation().Row - 1, current.GetLocation().Col))));
                        break;
                    case 1: //right
                        v = maze.Find(u => (u.GetLocation().Equals(new Location(current.GetLocation().Row, current.GetLocation().Col + 1))));
                        break;
                    case 2: //down
                        v = maze.Find(u => (u.GetLocation().Equals(new Location(current.GetLocation().Row + 1, current.GetLocation().Col))));
                        break;
                    case 3: //left
                        v = maze.Find(u => (u.GetLocation().Equals(new Location(current.GetLocation().Row, current.GetLocation().Col - 1))));
                        break;
                    default:
                        break;
                }
                // if the sets are different
                List<IVertex> l1 = sets.Find(list => list.Contains(current));
                if (!l1.Contains(v))
                {
                    current.AddNeighbor(v, direct);
                    v.AddNeighbor(current, (direct + 2) % 4);
                    List<IVertex> l2 = sets.Find(list => list.Contains(v));
                    // union
                    sets.Remove(l2);
                    l1.AddRange(l2);
                    count++;
                }
            }

            // choose end point
            bool isUpdated = false;
            IVertex e;
            while (!isUpdated)
            {
                int x = rand.Next(maze.Count);
                e = maze[x];
                // if just one neighbor
                if (e.NumOfNeighbs() == 1)
                {
                    this.end = e;
                    isUpdated = true;
                }
            }
        }
    }
}
