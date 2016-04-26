using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.view
{
    /// <summary>
    /// 
    /// </summary>
    class PlayHandleView : GeneralHandleOutput<string>
    {
        private const string number = "4";

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="convert">the convert</param>
        public PlayHandleView(IConvertableView convert):base(convert){}

        /// <summary>
        /// convert the msg and then return it
        /// </summary>
        /// <param name="msg">the msg</param>
        /// <returns>the string of the msg after convert</returns>
        protected override string Handle(string msg)
        {
            return base.convert.ConvertPlay(msg.Split(' ')[0], msg.Split(' ')[1]);   
        }

        /// <summary>
        /// get the num of the command
        /// </summary>
        /// <returns>the number of the command</returns>
        protected override string GetNumberCommandHandle()
        {
            return PlayHandleView.number;
        }
    }
}
