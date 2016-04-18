using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// implements ISearcher
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Searcher<T> : ISearcher<T>
    {
        private PriorityQueue<State<T>> openList;

        /// <summary>
        /// empty constructor
        /// </summary>
        public Searcher()
        {
            openList = new PriorityQueue<State<T>>();
        }

        /// <summary>
        /// pop from open list
        /// </summary>
        /// <returns> return the pop state </returns>
        protected State<T> PopOpenList()
        {
            //evaluatedNodes++;
            State<T> a = openList.GetItem();
            openList.RemoveItem(a);
            return a;
        }

        /// <summary>
        /// add given state to open list
        /// </summary>
        /// <param name="state"> the state to add </param>
        protected void AddToOpenList(State<T> state)
        {
            openList.AddItem(state);
        }

        /// <summary>
        /// check if the open list contains given state
        /// </summary>
        /// <param name="state"> the state to check </param>
        /// <returns> returns true if contains the state </returns>
        protected bool OpenListContaines(State<T> state)
        {
            return this.openList.Containes(state);
        }
       
        /// <summary>
        /// get the size of open list
        /// </summary>
        public int OpenListSize
        { // it is a read-only property :)
            get { return openList.Count(); }
        }

        /// <summary>
        /// remove given state from open list
        /// </summary>
        /// <param name="state"> the state to remove </param>
        public void RemoveOpenList(State<T> state)
        {
            this.openList.RemoveItem(state);
        }

        /// <summary>
        /// get the solution by backtracing
        /// </summary>
        /// <param name="state"> the state to start from </param>
        /// <returns></returns>
        public Solution<T> BackTrace(State<T> state)
        {
            State<T> current = state;
            List<State<T>> stateList = new List<State<T>>();
            while (current != null)
            {
                stateList.Add(current);
                current = current.cameFrom;
            }
            Solution<T> sol = new Solution<T>();
            sol.states = stateList;
            return sol;
        }

        /// <summary>
        /// abstract search function
        /// </summary>
        /// <param name="searchable"> the searchable to search </param>
        /// <returns> returns the solution </returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
