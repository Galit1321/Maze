using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mazelib
{
    /// <summary>
    /// interface of searcher
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearcher<T>
    {
        /// <summary>
        /// search the given searchable
        /// </summary>
        /// <param name="searchable"> the searchable to search </param>
        /// <returns> returns the solution </returns>
        Solution<T> Search(ISearchable<T> searchable);
    }
}
