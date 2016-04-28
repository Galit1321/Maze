using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using algoOnGraph;

namespace ServerExe1.src.model
{
    /// <summary>
    /// the cell of graph
    /// </summary>
    class Cell : ICell
    {
        private ICell previous;
        private int realValue;
        private bool reached;
        private int id;

        /// <summary>
        /// set the value of the cell
        /// </summary>
        /// <param name="newVal">the new value</param>
        public void SetValue(int newVal)
        {
            this.realValue = newVal;
        }

        /// <summary>
        /// set when the cell reached
        /// </summary>
        public void SetReached() { this.reached = true; }

        /// <summary>
        /// get the reached status
        /// </summary>
        /// <returns>the status, true if reached false oterwise</returns>
        public bool GetReached() { return this.reached; }

        /// <summary>
        /// get the value of the cell
        /// </summary>
        /// <returns>the value</returns>
        public int GetValue() { return this.realValue; }

        /// <summary>
        /// c'tor of the cell
        /// </summary>
        /// <param name="givenId">the id of the cell, the order of the creation</param>
        public Cell(int givenId)
        {
            this.realValue = -1;
            this.id = givenId;
            this.reached = false;
        }

        /// <summary>
        /// check if equals to the obj
        /// </summary>
        /// <param name="obj">the check</param>
        /// <returns>true if equals, otherwise return false</returns>
        public override bool Equals(object obj)
        {
            Cell other = (Cell)obj;
            return this.id == other.id;
        }

        /// <summary>
        /// check if not equal
        /// </summary>
        /// <param name="c1">first cell</param>
        /// <param name="c2">sec cell</param>
        /// <returns>true if not equals, otherwise false</returns>
        public static bool operator !=(Cell c1, Cell c2)
        {
            return !(c1 == c2);
        }

        /// <summary>
        /// Override to the == operator, get two cells and cehck if their
        /// id is equal.
        /// </summary>
        /// <param name="c1"></param> first cell.
        /// <param name="c2"></param> second cell.
        /// <returns></returns>
        public static bool operator ==(Cell c1, Cell c2)
        {
            return c1.id == c2.id;
        }

        /// <summary>
        /// Return the id of the cell.
        /// </summary>
        /// <returns></returns>
        public int GetPlace()
        {
            return this.id;
        }

        /// <summary>
        /// Override function hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Set the previous cell in the maze solution to the given one.
        /// </summary>
        /// <param name="prev"></param> the last cell.
        public void SetPrevious(ICell prev)
        {
            this.previous = prev;
        }

        /// <summary>
        /// Return the previous cell.
        /// </summary>
        /// <returns></returns>
        public ICell GetPrevious()
        {
            return previous;
        }
    }
}
