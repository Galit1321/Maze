using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// the state class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T> : IComparable
    {
        public T state { get; set; }    	 // the state represented by a string
        public double cost { get; set; }     // cost to reach this state (set by a setter)
        public double value { get; set;}
        public State<T> cameFrom { set; get; }  // the state we came from to this state (setter)
    
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="state"> state </param>
        public State(T state)
        {
            this.state = state;
        }

        /// <summary>
        /// compare to given state
        /// </summary>
        /// <param name="obj"> other to compare </param>
        /// <returns> returns 0 if compare </returns>
        public int CompareTo(object obj)
        {
            State<T> other = obj as State<T>;
            return this.cost.CompareTo(other.cost);
        }

        /// <summary>
        /// equals
        /// </summary>
        /// <param name="obj"> compare to obj </param>
        /// <returns> returns true if equals </returns>
        public override bool Equals(object obj) // we override Object's Equals method
        {
            return state.Equals((obj as State<T>).state);
        }

    }

}
