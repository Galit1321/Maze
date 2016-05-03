using ServerExe1.src.model;
using ServerExe1.src.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.Presentor
{
    /// <summary>
    /// the command handeer
    /// </summary>
    class CommandHandler
    {

        private const string generateCommand = "generate";
        private const string solveCommand = "solve";
        private const string multiCommand = "multiplayer";
        private const string playCommand = "play";
        private const string closeCommand = "close";
        private const string clueCommand = "clue";
        private Dictionary<string, ICommandable> commands;

        private string nextCommand;
        private ISendableView whoSend;

        /// <summary>
        /// c'tor of the command handler
        /// </summary>
        /// <param name="convert">how to convert</param>
        /// <param name="model">the model</param>
        /// <param name="command">the command </param>
        /// <param name="view">who send</param>
        public CommandHandler(IConvertableView convert, IModel model, string command, ISendableView view)
        {
            this.nextCommand = command;
            this.whoSend = view;

            //create the dict
            this.commands = new Dictionary<string, ICommandable>();
            PlayCommand playCommander = new PlayCommand(new PlayHandleView(convert), model);
            this.commands.Add(generateCommand, new GenerateCommand(new GenerateHandleView(convert), model));
            this.commands.Add(solveCommand, new SolveCommand(new SolveHandleView(convert), model));
            this.commands.Add(multiCommand, new MultiplayerCommand(new MultiHandleView(convert), playCommander, model));
            this.commands.Add(clueCommand, new ClueCommand(new ClueHandleView(convert), model));
            this.commands.Add(playCommand, playCommander);
            this.commands.Add(closeCommand, new CloseCommand(null, model));
        }

        /// <summary>
        /// handle the command 
        /// </summary>
        public void Handle()
        {
            //get the command and the args
            string[] theCommandSplit = this.nextCommand.Split(' ');
            List<string> args = new List<string>();
            for (int i = 1; i < theCommandSplit.Count(); i++)
            {
                args.Add(theCommandSplit[i]);
            }
            ICommandable command;
            //get the command if exist and do it
            bool isExist = this.commands.TryGetValue(theCommandSplit[0], out command);
            if (isExist == true)
            {
                command.Execute(args, this.whoSend);
            }
        }
    }
}
