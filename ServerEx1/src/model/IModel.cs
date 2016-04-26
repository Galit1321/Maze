using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using algoOnGraph;
using ServerExe1.src.view;
using ServerExe1.src.Presentor;

namespace ServerExe1.src.model
{

    /// <summary>
    /// the model of the server
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// get the maze according to the name, and the type
        /// </summary>
        /// <param name="nameMaze">the name of the maze</param>
        /// <param name="type">the type of the creating, 0 to random, 1 to DFS</param>
        /// <returns>the maze</returns>
        IMaze GetMaze(string nameMaze, int type);

        /// <summary>
        /// get maze with solve
        /// </summary>
        /// <param name="nameMaze">the maze name</param>
        /// <param name="type">the type of the solve, 0 to BFS, 1 to Best</param>
        /// <returns>Maze with solution, if the maze doesn't exist return null</returns>
        IMaze GetSolutionMaze(string nameMaze, int type);

        /// <summary>
        /// Add Player to game with this name
        /// </summary>
        /// <param name="nameGame">the name of the game</param>
        /// <param name="forStart">the handler of the start of the game</param>
        /// <param name="forMsg">the handler of the play</param>
        /// <param name="view">the view of this player</param>
        void AddPlayerToGame(string nameGame, IHandlerUpdate forStart,
            IHandlerUpdate forMsg, ISendableView view);

        /// <summary>
        /// player played
        /// </summary>
        /// <param name="move">where moved</param>
        /// <param name="whoSend">the view of the player to know who moved</param>
        void PlayerMoved(string move,ISendableView whoSend);

        /// <summary>
        /// close game
        /// </summary>
        /// <param name="nameGame">the name of the game</param>
        /// <param name="whoSend">who send to closeing</param>
        void ColseGame(string nameGame, ISendableView whoSend);
    }
}