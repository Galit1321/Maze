using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Class Name: CreateMazeDFS.
/// Implement or extend: ICreator.
/// Members: None.
/// Functions: Create (Implements).
/// Summary: This class is one of the two classes that implements the logic of creating the maze.
///          This class is managed to implement the logic of DFS creating that the new maze.
/// </summary>
namespace algoOnGraph
{
    public class CreateMazeDFS :ICreator
    {
        /// <summary>
        /// Function Name: Create.
        /// This function is randing cell to be the beginning, and after the function goes
        /// over his neighbors and open one of them (by random), then the function keep going
        /// with the dfs logic till she opened all of the cells.
        /// </summary>
        /// <param name="graph"></param> The graph that we want to maze it.
        public void Create(ref IMazeable graph)
        {
            Stack<ICell> DFSStack = new Stack<ICell>();
            Random rnd = new Random();
            ICell beginning = graph.GetRandomCell();
            //Get random beginning.
            graph.ChangeToBeginSituation(beginning);
            ICell current = beginning;
            //Calculate the min path length.
            int path = (int)(1.5 * graph.GetGraphDimension().Item1);
            bool flag = false;
            while (graph.GetUnvisitedNodes().Count > 0)
            {
                path--;
                if (path <= 0 && !flag)
                {
                    //If we found cell that can be the end point of the maze.
                    if (graph.IsBelongToBounds(current))
                    {
                        graph.ChangeToEndSituation(current);
                        flag = true;
                    }
                }
                if (graph.GetReachableCells(current).Count > 0)
                {
                    //Get random cell from the current cell neighbors.
                    ICell temp = (graph.GetReachableCells(current))[rnd.Next(0,
                        graph.GetReachableCells(current).Count)];
                    //Find only cells that we can not reach.
                    while (temp.GetReached())
                    {
                        temp = (graph.GetReachableCells(current))[rnd.Next(0,
                        graph.GetReachableCells(current).Count)];
                    }
                    DFSStack.Push(current);
                    //Set current and temp an edge.
                    graph.SetConnection(current, temp);
                    current = temp;
                    //Change the current cell state (Reached etc.)
                    graph.ChangeCellState(current);
                }
                else
                {
                    if (DFSStack.Count > 0)
                    {
                        current = DFSStack.Pop();
                        graph.ChangeCellState(current);
                    }
                    else
                    {
                        break;
                    }
                }

            }
            
        }
    }
}
