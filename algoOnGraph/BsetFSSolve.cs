using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Class Name: BsetFSSolve.
/// Implement or extend: SolveMaze.
/// Members: None.
/// Functions: InsertToQueue (Override), ChangePriority and AddToQueue.
/// Summary: This class is one of the two classes that implements the logic of solving the maze.
///          This class is managed to implement the logic of priority queue, that the best first 
///          search is using.
/// </summary>
namespace algoOnGraph
{
    public class BsetFSSolve : SolveMaze
    {
        /// <summary>
        /// Function Name:InsertToQueue
        /// This function goes over the list and insert it to the queue with 
        /// evaluate the new values.
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
            int edgeValue;
            foreach (ICell item in neighbors)
            {   
                //If we never saw this cell before, add it.
                if (!BFSQueue.Contains(item) && !path.Contains(item))
                {
                    item.SetPrevious(prev);
                    AddToQueue(ref BFSQueue, item);
                    continue;
                }
                //Get the value of the edge between the "father" and the current cell.
                edgeValue = values[new Tuple<int, int>(prev.GetPlace(), item.GetPlace())];
                if (item.GetValue() > prev.GetValue() + edgeValue)
                {
                    if (!BFSQueue.Contains(item)) { AddToQueue(ref BFSQueue, item); }
                    else { ChangePriority(ref BFSQueue, item); }
                }

            }
        }


        /// <summary>
        /// Function name:ChangePriority.
        /// The function check if we can improve the given cell and add it to the queue.
        /// </summary>
        /// <param name="BFSQueue"></param> The priority queue.
        /// <param name="item"></param> The cell that we want to change his priority.
        public void ChangePriority(ref Queue<ICell> BFSQueue,ICell item)
        {
            List<ICell> tempList = BFSQueue.ToList<ICell>();
            tempList.Remove(item);
            BFSQueue.Clear();
            foreach (ICell tempCell in tempList) 
            {
                BFSQueue.Enqueue(tempCell);
            }
            AddToQueue(ref BFSQueue, item);
        }

        /// <summary>
        /// Function name:AddToQueue.
        /// This function looking for the place in the priority queue and insert the
        /// given cell into.
        /// </summary>
        /// <param name="BFSQueue"></param>
        /// <param name="item"></param>
        public void AddToQueue(ref Queue<ICell> BFSQueue, ICell item)
        {
            List<ICell> tempList = BFSQueue.ToList<ICell>();
            BFSQueue.Clear();
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].GetValue() < item.GetValue())
                {
                    BFSQueue.Enqueue(item);
                }
                BFSQueue.Enqueue(tempList[i]);
            }
            if (!BFSQueue.Contains(item))
            {
                BFSQueue.Enqueue(item);
            }
        }
    }
}
