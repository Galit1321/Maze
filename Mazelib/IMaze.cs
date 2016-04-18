using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mazelib
{
    /// <summary>
    /// interface of Maze
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMaze<T> : ISearchable<T>
    {
        /// <summary>
        /// to string
        /// </summary>
        /// <returns> maze by string </returns>
        string ToString();

        /// <summary>
        /// get the width
        /// </summary>
        /// <returns> returns the width </returns>
        int GetWidth();

        /// <summary>
        /// get the height
        /// </summary>
        /// <returns> returns the height</returns>
        int Gethight();

        /// <summary>
        /// get the vertix
        /// </summary>
        /// <returns> return list of the vertix </returns>
        List<IVertex> GetIVertex();

        /// <summary>
        /// set the start/end points, given the points
        /// </summary>
        /// <param name="start"> given start point </param>
        /// <param name="end"> given end point </param>
        void SetStartEndPoints(IVertex start, IVertex end);

        /// <summary>
        /// get the start point
        /// </summary>
        /// <returns> returns start point </returns>
        IVertex GetStart();

        /// <summary>
        /// get the end point
        /// </summary>
        /// <returns> returns end point </returns>
        IVertex GetEnd();
     
    }
}
