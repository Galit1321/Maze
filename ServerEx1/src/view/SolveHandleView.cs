using ServerExe1.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.view
{
    /// <summary>
    /// handle the solve output
    /// </summary>
    class SolveHandleView : GeneralHandleOutput<IMaze>
    {
        private const string number = "2";

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="convert">the convert</param>
        public SolveHandleView(IConvertableView convert):base(convert){}

        /// <summary>
        /// convert the msg and then return it
        /// </summary>
        /// <param name="msg">the msg</param>
        /// <returns>the string of the msg after convert</returns>SS
        protected override string Handle(IMaze msg)
        {
            return base.convert.ConvertSolutionIMaze(msg);
        }

        /// <summary>
        /// get the num of the command
        /// </summary>
        /// <returns>the number of the command</returns>
        protected override string GetNumberCommandHandle()
        {
            return SolveHandleView.number;
        }

    }
}
