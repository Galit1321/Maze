using Server.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    /// <summary>
    /// interface of model
    /// </summary>
    public interface IModel
    {
        event updateModel ModelChanged;
        /// <summary>
        /// do the work
        /// </summary>
        /// <param name="id"> id </param>
        /// <param name="massege"> message what to do </param>
        void DoWork(int id, string massege);        
    }
}
