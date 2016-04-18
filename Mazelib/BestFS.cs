using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// The class for Best First Search
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BestFS<T> : Searcher<T>
    {
        /// <summary>
        /// Do the algorithm
        /// </summary>
        /// <param name="searchable"></param>
        /// <returns>
        /// returns the solution
        /// </returns>
        public override Solution<T> Search(ISearchable<T> searchable)
        {
            InitCost(searchable.GetInitialState());
            AddToOpenList(searchable.GetInitialState());
            List<State<T>> closed = new List<State<T>>();
            while (OpenListSize > 0)
            {
                State<T> n = PopOpenList();  // inherited from Searcher, removes the best state
                closed.Add(n);
                if (n.Equals(searchable.GetGoalState()))
                {
                    return BackTrace(n); // private method, back traces through the parents
                }
                // calling the delegated method, returns a list of states with n as a parent
                List<State<T>> succerssors = searchable.GetAllPossibleStates(n);
                foreach (State<T> s in succerssors)
                {
                    if (!closed.Contains(s) && !OpenListContaines(s))
                    {
                        s.cameFrom = n;  // already done by getSuccessors
                        InitCost(s);
                        s.cost = n.cost + s.value;
                        AddToOpenList(s);
                    }
                    else if (s.cost > n.cost + s.value)
                    {
                        if (OpenListContaines(s))
                        {
                            s.cost = n.cost + s.value;
                            RemoveOpenList(s);
                            AddToOpenList(s);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// init the cost of vertix by random
        /// </summary>
        /// <param name="state"></param>
        public virtual void InitCost(State<T> state) {
            Random rand = new Random();
            state.value = rand.Next(100);
        }
    }
}

