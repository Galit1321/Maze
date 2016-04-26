using ServerExe1.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerExe1.src.view
{
    /// <summary>
    /// convert output
    /// </summary>
    interface IConvertableView
    {
        /// <summary>
        /// convert maze to Json
        /// </summary>
        /// <param name="maze">the maze</param>
        /// <returns>the maze in JSon</returns>
        string ConvertIMaze(IMaze maze);

        /// <summary>
        /// convert Maze without name to Json
        /// </summary>
        /// <param name="maze">the maze</param>
        /// <returns>maze in Json</returns>
        string ConvertIMazeWithoutName(IMaze maze);

        /// <summary>
        /// convert solution to Json
        /// </summary>
        /// <param name="maze">the maze with the sol</param>
        /// <returns>the sol in JSon</returns>
        string ConvertSolutionIMaze(IMaze maze);

        /// <summary>
        /// convert the all output to Json
        /// </summary>
        /// <param name="command">the command</param>
        /// <param name="Content">the content of the return vlaue</param>
        /// <returns>the return string</returns>
        string ConvertOutput(string command, string Content);

        /// <summary>
        /// convert movment on the graph
        /// </summary>
        /// <param name="gameName">the name of the game</param>
        /// <param name="move">the movement</param>
        /// <returns>the json </returns>
        string ConvertPlay(string gameName, string move);

        /// <summary>
        /// convert start game to JSon
        /// </summary>
        /// <param name="gameName">the game naem</param>
        /// <param name="you">the maze of the player</param>
        /// <param name="other">the maze of the other player</param>
        /// <returns>the output in Json</returns>
        string ConvertStartGame(string gameName, IMaze you, IMaze other);
    }
}
