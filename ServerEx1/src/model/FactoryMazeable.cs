using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using algoOnGraph;

namespace ServerExe1.src.model
{
    /// <summary>
    /// Factory to applay maze on graph
    /// </summary>
    class FactoryMazeable
    {
        private const int random = 0;
        private const int dfs = 1;

        private Dictionary<int, ICreator> fact;

        /// <summary>
        /// create the dictionary
        /// </summary>
        public FactoryMazeable()
        {
            fact = new Dictionary<int, ICreator>();
            fact.Add(random, new CreateMazeRandom());
            fact.Add(dfs, new CreateMazeDFS());
        }

        /// <summary>
        /// take the match creator and create
        /// </summary>
        /// <param name="graph">the graph</param>
        /// <param name="type">the type</param>
        public void CreateTheMaze(IMazeable graph, int type)
        {
            ICreator create;
            if (this.fact.TryGetValue(type, out create))
            {
                create.Create(ref graph);
            }
        }
    }
}
