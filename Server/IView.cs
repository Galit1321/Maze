using Server.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    /// <summary>
    /// interface of view
    /// </summary>
    public interface IView
    {
        event updateView ViewChanged;
        /// <summary>
        /// display data
        /// </summary>
        /// <param name="id"> the id </param>
        /// <param name="massege"> the message </param>
        void DisplayData(int id, string massege);
    }
}
