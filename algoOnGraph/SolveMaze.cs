using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Class Name: SolveMaze.
/// Members: None.
/// Functions: InsertToQueue (Abstract), Solve and UpdateValues.
/// Summary: This class is responsible for getting maze and solve it.
///          The class is using bfs principle.
/// </summary>
namespace algoOnGraph
{
    public abstract class SolveMaze
    {
        /// <summary>
        /// Function Name:InsertToQueue
        /// We leave this function to each method inserting to queue.
        /// </summary>
        /// <param name="BFSQueue"></param> The queue that we want to update.
        /// <param name="neighbors"></param> The list of the cells  that we want to insert.
        /// <param name="path"></param> The current path from the beginning to till now.
        /// <param name="prev"></param> The "father" of the current cells that we want to insert.
        /// <param name="values"></param> The value of the edges in the graph.
        public abstract void InsertToQueue(ref Queue<ICell> BFSQueue,
            List<ICell> neighbors,ref List<ICell> path,ref ICell prev,
            Dictionary<Tuple<int, int>, int> values);

        /// <summary>
        /// This funtion is getting maze and with the method of bfs solve it.
        /// </summary>
        /// <param name="graph"></param> the maze that we want to solve.
        /// <returns></returns>
        public List<ICell> Solve(IMazeable graph)
        {
            //The edges values.
            Dictionary<Tuple<int,int>,int> values =  UpdateValues(ref graph);

            Queue<ICell> BFSQueue = new Queue<ICell>();
            List<ICell> pathToTheEnd = new List<ICell>();
            List<ICell> realPath = new List<ICell>();
            Random rnd = new Random();
            //Get the beginning of the maze.
            ICell beginning = graph.GetBeginning();
            beginning.SetValue(0);
            int length;
            BFSQueue.Enqueue(beginning);
            ICell temp = null;
            while (BFSQueue.Count > 0)
            {
                temp = BFSQueue.Dequeue();
                pathToTheEnd.Add(temp);
                if (graph.IsTheEnd(temp)) { break; }
                //Call to the abstract method.
                InsertToQueue(ref BFSQueue, graph.GetNeighbors(temp),ref pathToTheEnd, 
                    ref temp,values);
            }
            length = pathToTheEnd.Count;
            if (!graph.IsTheEnd(temp))
            {
                return null;
            }
            realPath.Add(temp);
            //Recover the path to the end.
            for (int i = length - 2; i >= 0; i--)
            {
                if (graph.IsNeighbors(temp,pathToTheEnd[i]))
                {
                    temp = pathToTheEnd[i];
                    realPath.Add(temp);
                }
            }
            return realPath;
        }

        /// <summary>
        /// This function get the graph and random value 
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public Dictionary<Tuple<int, int>,int> UpdateValues(ref IMazeable graph)
        {
            Dictionary<Tuple<int, int>,int> values = new Dictionary<Tuple<int, int>,int>();
            List<ICell> allNodes = graph.GetAllNodes();
            int firstDiemn = (int)graph.GetGraphDimension().Item1;
            int length = (int)(graph.GetGraphDimension().Item1 * graph.GetGraphDimension().Item2);
            Random rnd = new Random();
            foreach (ICell item in allNodes)
            {
                //Rand value to each node.
                item.SetValue(rnd.Next(0, 100));
                List<ICell> tempList = new List<ICell>(graph.GetNeighbors(item));
                foreach (ICell tempCell in tempList)
                {
                    //Rand values to the cell edges.
                    Tuple<int,int> t = new Tuple<int, int>(item.GetPlace(), tempCell.GetPlace());
                    int rndValue = rnd.Next(0, 100);
                    values.Add(t, rndValue);
                }
            }
            //make value of edge (u,v) = (v,u)
            foreach (ICell item in allNodes)
            {
                List<ICell> tempList = new List<ICell>(graph.GetNeighbors(item));
                foreach (ICell tempCell in tempList)
                {
                    Tuple<int, int> t = new Tuple<int, int>(item.GetPlace(), tempCell.GetPlace());
                    Tuple<int, int> tTemp = new Tuple<int, int>(tempCell.GetPlace(), item.GetPlace());
                    values[tTemp] = values[t];
                }
            }
            return values;
        }

    }
}
