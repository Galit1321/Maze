using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Interface Name: ISearchable.
/// Functions: GetReachableCells, IsBelongToBounds, ChangeCellState and GetAllNodes.
/// Summary: This class is unit all the data structure that we can search on.
/// </summary>
namespace algoOnGraph
{
    public interface ISearchable
    {
        //Return list of all the cells that the given cell can reach.
        List<ICell> GetReachableCells(ICell current);
        //Return true if the given cell is on the bounds.
        bool IsBelongToBounds(ICell src);
        //Change cell in the DS to be reached.
        void ChangeCellState(ICell src);
        //Return all the nodes that in our DS.
        List<ICell> GetAllNodes();
    }
}
