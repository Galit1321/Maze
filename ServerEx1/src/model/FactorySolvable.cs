using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using algoOnGraph;

namespace ServerExe1.src.model
{
    /// <summary>
    /// Factory to applay solve on graph
    /// </summary>
    class FactorySolvable
    {

        private const int bfs = 0;
        private const int bestFs = 1;

        private Dictionary<int, SolveMaze> fact;

        /// <summary>
        /// create the dictionary
        /// </summary>
        public FactorySolvable()
        {
            fact = new Dictionary<int, SolveMaze>();
            fact.Add(bfs, new SolveBFS());
            fact.Add(bestFs, new BsetFSSolve());
        }

        /// <summary>
        /// take the match solver and solve
        /// </summary>
        /// <param name="graph">the graph</param>
        /// <param name="type">the type</param>
        /// <rereturns>list of the cell of the solve</rereturns>
        public List<ICell> SolveTheMaze(IMazeable graph, int type)
        {
            SolveMaze solve;
            if (this.fact.TryGetValue(type, out solve))
            {
                return solve.Solve(graph);
            }
            return null;
        }
    }
}
