using ServerExe1.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ServerExe1.src.view
{
    /// <summary>
    /// multiplay command handle
    /// </summary>
    class MultiHandleView : GeneralHandleOutput<Tuple<string, Tuple<IMaze, IMaze>>>
    {
        private const string number = "3";

        /// <summary>
        /// c'tor
        /// </summary>
        /// <param name="convert">the convert</param>
        public MultiHandleView(IConvertableView convert):base(convert){}

        /// <summary>
        /// convert the msg and then return it
        /// </summary>
        /// <param name="msg">the msg</param>
        /// <returns>the string of the msg after convert</returns>
        protected override string Handle(Tuple<string, Tuple<IMaze, IMaze>> msg)
        {
            return base.convert.ConvertStartGame(msg.Item1, msg.Item2.Item1, msg.Item2.Item2);
        }

        /// <summary>
        /// get the num of the command
        /// </summary>
        /// <returns>the number of the command</returns>
        protected override string GetNumberCommandHandle()
        {
            return MultiHandleView.number;
        }

    }
}
