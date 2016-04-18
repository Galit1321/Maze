using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// interface of searchable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// get initial state
        /// </summary>
        /// <returns> returns initial state </returns>
        State<T> GetInitialState();

        /// <summary>
        /// get goal state
        /// </summary>
        /// <returns> returns goal state </returns>
        State<T> GetGoalState();

        /// <summary>
        /// get all of the possible state from a given state
        /// </summary>
        /// <param name="s"> the current state </param>
        /// <returns> returns list of possible states </returns>
        List<State<T>> GetAllPossibleStates(State<T> s);
   }

}
