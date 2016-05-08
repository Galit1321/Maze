using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ex2
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
        /// <summary>
        /// over equal to comper between curr 
        /// pair to goal pair
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
           Pair l = obj as Pair;
            return (this.Row.Equals(l.Row)) && (this.Col.Equals(l.Col));
        }
        /// <summary>
        /// for equal function
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public int GetPos(int width)
        {
            return Row * (2 * width - 1) + Col;

        }
    }
}
