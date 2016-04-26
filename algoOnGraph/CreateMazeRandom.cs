using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class Name: CreateMazeRandom.
/// Implement or extend: ICreator.
/// Members: None.
/// Functions: Create (Implements).
/// Summary: This class is one of the two classes that implements the logic of creating the maze.
///          This class is managed to implement the logic of randomize creating that the new maze.
/// </summary>
namespace algoOnGraph
{
    public class CreateMazeRandom : ICreator
    {
        /// <summary>
        /// Function Name: Create.
        /// This function is randing cell to be the beginning, and after the function goes
        /// through the neighbors list and get one random neighbors and set the connection,
        /// and keep going.
        /// </summary>
        /// <param name="graph"></param> The graph that we want to maze it.
        public void Create(ref IMazeable graph)
        {
            ICell beginning = graph.GetRandomCell();
            //Get random cell.
            graph.ChangeToBeginSituation(beginning);
            Tuple<double, double> dim = graph.GetGraphDimension();
            int pathLength = (int)(1.5 * dim.Item1), index;
            ICell current = beginning, tempCell;
            List<ICell> neigh = null, path = new List<ICell>();
            path.Add(current);
            neigh = graph.GetReachableCells(current);
            graph.ChangeCellState(current);
            Random rnd = new Random();
            //Goes over the path length that we evalueted, and create it.
            for (int i = 0; i < pathLength; i++)
            {
                if (neigh.Count == 0) { break; }
                index = rnd.Next(0, neigh.Count);
                tempCell = neigh[index];
                graph.SetConnection(current, tempCell);
                current = tempCell;
                path.Add(current);
                neigh = graph.GetReachableCells(current);
                graph.ChangeCellState(current);
            }
            //Check if the current cell can be the end cell.
            if (neigh.Count == 0 || graph.IsBelongToBounds(current)){

                graph.ChangeToEndSituation(current);
               
            } else
            {
                //Keep looking till we find a good cell that can be the end point.
                while (path.Count != graph.GetAllNodes().Count && !graph.IsBelongToBounds(current))
                {
                    graph.ChangeCellState(current);
                    neigh = graph.GetReachableCells(current);
                    if (neigh.Count == 0) { break; }
                    index = rnd.Next(0, neigh.Count);
                    tempCell = neigh[index];
                    graph.SetConnection(current, tempCell);
                    path.Add(current);
                    current = tempCell;
                }
                if (neigh.Count == 0 || graph.IsBelongToBounds(current))
                {

                    graph.ChangeToEndSituation(current);
                }

            }
            int tempCount = 0;
            //Openning all of the rest cells.
            while (path.Count != graph.GetAllNodes().Count)
            {
                
                ICell tempBreakWallsCell = path[rnd.Next(0, path.Count)];
                tempCount = graph.GetReachableCells(tempBreakWallsCell).Count;
                if (tempCount == 0)
                {
                    continue;
                }
                ICell newCell = graph.GetReachableCells(tempBreakWallsCell)[rnd.Next(0, tempCount)];
                if (newCell.GetReached()) { continue; }
                graph.SetConnection(tempBreakWallsCell, newCell);
                path.Add(newCell);
                graph.ChangeCellState(newCell);

            }

            
        }
        /// <summary>
        /// Function name: BreakWalls.
        /// The function is completing the create of the matrix.
        /// </summary>
        /// <param name="graph"></param> the ref to the graph.
        /// <param name="path"></param> the path that we already did.
        public void BreakWalls(ref IMazeable graph, List<ICell> path)
        {
            Random rnd = new Random();
            int count = 0;
            int trys = 0;
            while (count != 4 && trys <= 10)
            {
                trys++;
                ICell rand = path[rnd.Next(0, path.Count)];
                List<ICell> l = graph.GetReachableCells(rand);
                if (l.Count == 0) { continue; }
                ICell tempCell = l[rnd.Next(0, l.Count)];
                graph.ChangeCellState(tempCell);
                graph.SetConnection(rand, tempCell);
                count++;
            }
        }
    }
}
