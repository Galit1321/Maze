using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// Implements of priority queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T>
    {
        private List<T> queue;

        /// <summary>
        /// constructor
        /// </summary>
        public PriorityQueue()
        {
            this.queue = new List<T>();
        }

        /// <summary>
        /// get item from the queue
        /// </summary>
        /// <returns> returns the item with the biggest priority </returns>
        public T GetItem()
        {
            if (this.queue.Count == 0)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                queue.Sort();
                return queue[0];
            }
        }

        /// <summary>
        /// add the given item
        /// </summary>
        /// <param name="item"> the item to add to the queue </param>
        public void AddItem(T item)
        {
            this.queue.Add(item);
        }

        /// <summary>
        /// remove the given item
        /// </summary>
        /// <param name="item"> the item to remove from queue </param>
        public void RemoveItem(T item)
        {
            this.queue.Remove(item);
        }

        /// <summary>
        /// get the size of
        /// </summary>
        /// <returns> returns the size of queue </returns>
        public int Count()
        {
            return this.queue.Count();
        }

        /// <summary>
        /// check if the queue contains the given item
        /// </summary>
        /// <param name="item"> the item to check </param>
        /// <returns> returns true if contains the item </returns>
        public bool Containes(T item)
        {
            return queue.Contains(item);
        }
    }
}
