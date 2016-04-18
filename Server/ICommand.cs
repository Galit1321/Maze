using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    /// <summary>
    /// interface ICommand
    /// </summary>
    public interface ICommand
    {
        event taskUpdate commendChange;

        /// <summary>
        /// Set client id given id
        /// </summary>
        /// <param name="id"> id </param>
        void setClientId(int id);

        /// <summary>
        /// Set the arguments
        /// </summary>
        /// <param name="args"> args to set </param>
        void SetArgs(string[] args);

        /// <summary>
        /// handle the command
        /// </summary>
        void handle();

        /// <summary>
        /// create new this command
        /// </summary>
        /// <returns> returns new command </returns>
        ICommand NewCommand();
    }
}

