using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.Presentor;
using ServerExe1.src.view;

namespace ServerExe1.src.model
{
    /// <summary>
    /// the delegete
    /// </summary>
    /// <param name="obj">the obj</param>
    /// <param name="send">who send</param>
    delegate void Update(object obj, ISendableView send);
    /// <summary>
    /// Game between two players
    /// </summary>
    class Game
    {
        public event Update updateFirst;
        public event Update updateSec;

        public string NameGame { get; private set; }
        public IHandlerUpdate FirstPlayerMsg { get; private set; }
        public IHandlerUpdate FirstPlayerMaze { get; private set; }
        public ISendableView viewFirstPlayer { get; private set; }
        public IHandlerUpdate SecPlayerMsg { get; private set; }
        public IHandlerUpdate SecPlayerMaze { get; private set; }
        public ISendableView ViewSecPlayer { get; private set; }
        public bool IsPlay { get; private set; }

        /// <summary>
        /// create the game and put the first player
        /// </summary>
        /// <param name="name">the name of the game</param>
        /// <param name="playerMaze">the handler update of the maze</param>
        /// <param name="playerMsg">the handler update of the play</param>
        /// <param name="view">the view of the player</param>
        public Game(string name, IHandlerUpdate playerMaze, IHandlerUpdate playerMsg,
            ISendableView view)
        {
            this.FirstPlayerMaze = playerMaze;
            this.updateFirst += playerMsg.UpdateView;
            this.viewFirstPlayer = view;
            this.NameGame = name;
            IsPlay = false;
        }

        /// <summary>
        /// add the second player to the game, and start play
        /// </summary>
        /// <param name="playerMaze">the handler update of the maze</param>
        /// <param name="playerMsg">the handler update of the play</param>
        /// <param name="view">the view of the player</param>
        public void AddSecMember(IHandlerUpdate playerMaze,
            IHandlerUpdate playerMsg, ISendableView view)
        {
            this.SecPlayerMaze = playerMaze;

            this.updateSec += playerMsg.UpdateView;
            this.ViewSecPlayer = view;
            IsPlay = true;
        }

        /// <summary>
        /// check if player is in the game
        /// </summary>
        /// <param name="theSender">the player</param>
        /// <returns>true if here, otherwise false</returns>
        public bool IsPlayerHere(ISendableView theSender)
        {
            if (this.viewFirstPlayer.Equals(theSender) ||
                (this.ViewSecPlayer != null && this.ViewSecPlayer.Equals(theSender)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// send to te first player the movement
        /// </summary>
        /// <param name="move">the move</param>
        public void SendFirstPlayer(string move)
        {
            this.updateFirst(this.NameGame + " " + move, this.viewFirstPlayer);
        }

        /// <summary>
        /// send to te first player the movement
        /// </summary>
        /// <param name="move">the move</param>
        public void SendSecPlayer(string move)
        {
            this.updateSec(this.NameGame + " " + move, this.ViewSecPlayer);
        }
    }
}
