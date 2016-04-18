using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// The class do Breadth Firs Search
    /// </summary>
    /// <typeparam name="T"></typeparam>
     public class Bfs<T> : BestFS<T>
    {
         /// <summary>
         /// Init the cost of each vertex to 1
         /// </summary>
         /// <param name="s"></param>
        public override void InitCost(State<T> s) {
            s.value = 1;
        }
    }
}
