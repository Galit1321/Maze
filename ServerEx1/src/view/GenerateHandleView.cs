using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.Presentor;
using ServerExe1.src.model;

namespace ServerExe1.src.view
{
    /// <summary>
    /// handle generate command output
    /// </summary>
    class GenerateHandleView : GeneralHandleOutput<IMaze>
    {
        private const string number = "1";

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="convert">the convert</param>
        public GenerateHandleView(IConvertableView convert):base(convert){}

        /// <summary>
        /// handle te maze
        /// </summary>
        /// <param name="msg">the maze</param>
        /// <returns>the convert of the maze to the oputpt</returns>
        protected override string Handle(IMaze msg)
        {
            return base.convert.ConvertIMaze(msg);

        }

        /// <summary>
        /// get the num of the command
        /// </summary>
        /// <returns>the number of the command</returns>
        protected override string GetNumberCommandHandle()
        {
            return GenerateHandleView.number;
        }
    }
}
