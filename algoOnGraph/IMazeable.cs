using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Interface Name: IMazeable.
/// Implement or extend: ISearchable.
/// Functions: SetConnection, GetRandomCell, GetGraphDimension, ChangeToEndSituation,
///            ChangeToBeginSituation, GetUnvisitedNodes, IsTheEnd, GetNeighbors and IsNeighbors.
/// Summary: This interface is responsible for all of the graphs/matrix etc. that can be maze
///          i.e. all of the data structure that can be mazes.  
/// </summary>
namespace algoOnGraph
{
    public interface IMazeable : ISearchable
    {
        //The function make and edge between src and dst.
        void SetConnection(ICell src, ICell dst);
        //Return random cell from the bounds.
        ICell GetRandomCell();
        //Return the maze dimension.
        Tuple<double, double> GetGraphDimension();
        //Change the beginning cell to the given.
        void ChangeToEndSituation(ICell end);
        //Change the end cell to the given.
        void ChangeToBeginSituation(ICell start);
        //Return list that contains all of the cells that our path didn't found yet. 
        List<ICell> GetUnvisitedNodes();
        //Is the given cell is the end point of the maze.
        bool IsTheEnd(ICell dst);
        //Return list of cells that contains all of the given cell's neighbors.
        List<ICell> GetNeighbors(ICell src);
        //Return the beginning cell of the maze path.
        ICell GetBeginning();
        //Return true if the both cells are neighbors.
        bool IsNeighbors(ICell src, ICell dst);
    }
}
