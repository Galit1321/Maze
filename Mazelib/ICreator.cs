using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// interface of creator
    /// </summary>
    public interface ICreator
    {
        /// <summary>
        /// create maze given empty maze
        /// </summary>
        /// <param name="emptyMaze"> empty maze to create </param>
        void Create(IMaze<IVertex> emptyMaze);
    }
}
