using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class Pair
    {
        public int Row;
        public int Col;
        /// <summary>
        /// construrctor
        /// </summary>
        /// <param name="i">row in a matrix</param>
        /// <param name="j">col in a matrix</param>
        public Pair (int i,int j)
        {
            this.Row = i;
            this.Col = j;
        }

        public override bool Equals(object obj)
        {
           Pair l = obj as Pair;
            return (this.Row.Equals(l.Row)) && (this.Col.Equals(l.Col));
        }
    }
}
