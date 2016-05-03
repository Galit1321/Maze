using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerExe1.src.view
{
    class ClueHandleView:GeneralHandleOutput<string>
    {
        private const string number = "6";

        public ClueHandleView(IConvertableView convert):base(convert){}


        protected override string Handle(string msg)
        {
            return this.convert.ConvertPlay(msg.Split(' ')[0], msg.Split(' ')[1]);
        }

        protected override string GetNumberCommandHandle()
        {
            return ClueHandleView.number;
        }
    }
}
