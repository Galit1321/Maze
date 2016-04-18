using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
enum direction { UP, RIGHT, DOWN, LEFT }

namespace Mazelib
{
    /// <summary>
    /// interface of vertex that is a part of maze
    /// </summary>
    public interface IVertex
    {
        /// <summary>
        /// get the number of possible neighbours
        /// </summary>
        /// <returns> returns the number of neighbours </returns>
        int NumOfNeighbs();

        /// <summary>
        /// add new neighbour given the side
        /// </summary>
        /// <param name="neighbor"> the vertex to add </param>
        /// <param name="type"> the location to add the new neighbour </param>
        void AddNeighbor(IVertex neighbor, int type);

        /// <summary>
        /// get the walls of this vertex
        /// </summary>
        /// <returns> returns an array of 4 walls, 1 for wall and 0 if there is not wall </returns>
        List<int> GetWall();

        /// <summary>
        /// get the possible neighbours
        /// </summary>
        /// <returns> returns an array of neighbours </returns>
        IVertex[] GetAdj();

        /// <summary>
        /// check if this vertex is visited at the search
        /// </summary>
        /// <returns> returns true if visited </returns>
        bool IsViste();

        /// <summary>
        /// set if visited
        /// </summary>
        /// <param name="val"> the value to set </param>
        void SetVisit(bool val);

        /// <summary>
        /// get the value of vertex
        /// </summary>
        /// <returns> returns the value </returns>
        int GetValue();

        /// <summary>
        /// get the location
        /// </summary>
        /// <returns> returns this vertex location </returns>
        Location GetLocation();

        /// <summary>
        /// compare with other vertwx
        /// </summary>
        /// <param name="other"> other vertex to compare </param>
        /// <returns> returns true if equals </returns>
        bool Equals(Object other);
    }
}
