using algoOnGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.model
{
    /// <summary>
    /// hold the solution of maze
    /// </summary>
    class SolutionMaze
    {
        private List<ICell> theSoul;

        /// <summary>
        /// c'tor of the sol
        /// </summary>
        /// <param name="sol">get the list of cell of the solution</param>
        public SolutionMaze(List<ICell> sol)
        {
            this.theSoul = new List<ICell>(sol);
        }

        /// <summary>
        /// copy c'tor
        /// </summary>
        /// <param name="sol">the other solution</param>
        public SolutionMaze(SolutionMaze sol) : this(sol.theSoul) { }

        /// <summary>
        /// reverse the solution list
        /// </summary>
        public void ReverseSol()
        {
            this.theSoul.Reverse();
        }

        /// <summary>
        /// get the list of the cells
        /// </summary>
        /// <returns>the list of the cells</returns>
        public List<ICell> GetList()
        {
            return this.theSoul;
        }
    }
}
