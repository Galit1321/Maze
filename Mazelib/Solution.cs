using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// contains the solution as list of states
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Solution<T>
    {
        public List<State<T>> states{set; get;}
    }
}
