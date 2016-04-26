using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Interface name: ICreator.
/// Functions name: Create.
///  This function represent the function that create the maze by given mazeable.
/// </summary>
namespace algoOnGraph
{
    public interface ICreator
    {
        //The function that get mazeable and make from it maze.
        void Create(ref IMazeable graph);
    }
}
