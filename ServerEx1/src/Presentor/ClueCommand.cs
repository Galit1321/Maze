using ServerExe1.src.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerExe1.src.Presentor
{
    class ClueCommand: ICommandable
    {
        private ClueHandleView clueHandleView;
        private model.IModel model;

        public ClueCommand(ClueHandleView clueHandleView, model.IModel model)
        {
            // TODO: Complete member initialization
            this.clueHandleView = clueHandleView;
            this.model = model;
        }

        public void Execute(List<string> args, ISendableView sender)
        {
            string toSend = this.model.GetClue(args[0], Int32.Parse(args[1]), Int32.Parse(args[2]));
            this.clueHandleView.HandleOutput(toSend, sender);
        }
    }
}
