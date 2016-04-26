using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Interface name: ICell.
/// Functions name: SetValue, SetReached, GetReached,
///                 GetValue, GetPlace, SetPrevious and GetPrevious.
///  This function represent one cell in the maze.
/// </summary>
namespace algoOnGraph
{
    public interface ICell
    {
        //Update the value of cell.
        void SetValue(int newVal);
        //Set that the cell is reached.
        void SetReached();
        //Return if the cell was reached or no.
        bool GetReached();
        //Get the value of the cell.
        int GetValue();
        //Get the id of the cell.
        int GetPlace();
        //Set the prev cell of the current cell in the solution path.
        void SetPrevious(ICell prev);
        //Get the father of that cell.
        ICell GetPrevious();
    }
}
