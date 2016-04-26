using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.model
{
    /// <summary>
    /// general interface of maze
    /// </summary>
    interface IMaze
    {
        /// <summary>
        /// solve the maze according the type
        /// </summary>
        /// <param name="type">the type of the solve</param>
        void SolveMaze(int type);

        /// <summary>
        /// get the name of the maze
        /// </summary>
        /// <returns>the name</returns>
        string GetName();
       
        /// <summary>
        /// get the string of the maze
        /// </summary>
        /// <returns>the string of the maze</returns>
        string ToString();

        /// <summary>
        /// get print of the solve in the maze
        /// </summary>
        /// <returns>the string of the solve</returns>
        string PrintSolve();

        /// <summary>
        /// get the start place in the matrix of the path
        /// </summary>
        /// <returns>the place of the start</returns>
        Tuple<int, int> GetStartPlace();

        /// <summary>
        /// get the end place in the matrix of the path
        /// </summary>
        /// <returns>the place of the end</returns>
        Tuple<int, int> GetEndPlace();
    }
}
