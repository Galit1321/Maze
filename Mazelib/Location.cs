using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// location of row and coloumn
    /// </summary>
    public class Location
    {
        public int Row { get; set; }
        public int Col { get; set; }

        /// <summary>
        /// empty constructor
        /// </summary>
        public Location(){}

        /// <summary>
        /// costruct Location given x and y
        /// </summary>
        /// <param name="x"> row </param>
        /// <param name="y"> col </param>
        public Location(int x, int y)
        {
            this.Row = x;
            this.Col = y;
        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="other"> other to copy </param>
        public Location(Location other)
        {
            this.Row = other.Row;
            this.Col = other.Col;
        }

        /// <summary>
        /// compare with other location
        /// </summary>
        /// <param name="obj"> other to compare </param>
        /// <returns> returns true if equals </returns>
        public override bool Equals(object obj)
        {
            Location l = obj as Location;
            return (this.Row.Equals(l.Row)) && (this.Col.Equals(l.Col));
        }
        
    }
}
