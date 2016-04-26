using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Class Name: SolveBFS.
/// Implement or extend: SolveMaze.
/// Members: None.
/// Functions: InsertToQueue (Override).
/// Summary: This class is one of the two classes that implements the logic of solving the maze.
///          This class is managed to implement the logic of normal queue, that the breadth first 
///          search is using.
/// </summary>
namespace algoOnGraph
{
    public class SolveBFS : SolveMaze
    {
        /// <summary>
        /// Function Name:InsertToQueue
        /// This function goes over the list and insert it to the queue.
        /// </summary>
        /// <param name="BFSQueue"></param> The queue that we want to update.
        /// <param name="neighbors"></param> The list of the cells  that we want to insert.
        /// <param name="path"></param> The current path from the beginning to till now.
        /// <param name="prev"></param> The "father" of the current cells that we want to insert.
        /// <param name="values"></param> The value of the edges in the graph.
        public override void InsertToQueue(ref Queue<ICell> BFSQueue, 
            List<ICell> neighbors, ref List<ICell> path,ref ICell prev,
            Dictionary<Tuple<int, int>, int> values)
        {
            foreach (ICell item in neighbors)
            {
                item.SetPrevious(prev);
                if (path.Contains(item)) { continue; }
                BFSQueue.Enqueue(item);
            }
        }
    }
}
