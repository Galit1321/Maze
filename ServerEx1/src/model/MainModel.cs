using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ServerExe1.src.Presentor;
using ServerExe1.src.view;

namespace ServerExe1.src.model
{
    /// <summary>
    /// the main model of the server hold the games and the mazes
    /// </summary>
    class MainModel: IModel
    {
        private AllMaze mazes;
        private List<Game> games;

        /// <summary>
        /// crete the collections
        /// </summary>
        public MainModel()
        {
            mazes = new AllMaze();
            games = new List<Game>();
        }

        /// <summary>
        /// get the maze according to the name, and the type,
        /// </summary>
        /// <param name="nameMaze">the name of the maze</param>
        /// <param name="type">the type of the creating, 0 to random, 1 to DFS</param>
        /// <returns>the maze</returns>
        public IMaze GetMaze(string nameMaze, int type)
        {
            return this.mazes.GetMaze(nameMaze, type) as IMaze;
        }

        /// <summary>
        /// get maze with solve
        /// </summary>
        /// <param name="nameMaze">the maze name</param>
        /// <param name="type">the type of the solve, 0 to BFS, 1 to Best</param>
        /// <returns>Maze with solution, if the maze doesn't exist return null</returns>
        public IMaze GetSolutionMaze(string nameMaze, int type)
        {
            return this.mazes.GetSolutionMaze(nameMaze, type) as IMaze;
        }

        /// <summary>
        /// Add Player to game with this name
        /// </summary>
        /// <param name="nameGame">the name of the game</param>
        /// <param name="forStart">the handler of the start of the game</param>
        /// <param name="forMsg">the handler of the play</param>
        /// <param name="view">the view of this player</param>
        /// <returns>true if added, false if the player allready in game</returns>
        public void AddPlayerToGame(string nameGame, IHandlerUpdate forStart,
            IHandlerUpdate forMsg, ISendableView view)
        {
            List<Game> thePossibleGames; //if the 
            //if the player allready in game
            if (this.games.Find(item => item.IsPlayerHere(view))!=null)
            {
                return;
            }
            //if got to here so the player doesn't play in any game.

            //find the all games that have that nameGame
            if ((thePossibleGames=this.games.FindAll(item=>item.NameGame==nameGame))!=null)
            {
                /*find the first game that is't played allready and add the player to the 
                 * game and send to the players that in the game*/
                foreach (Game theGame in thePossibleGames)
                {
                    //check
                    if (theGame.IsPlay != true)
                    {
                        theGame.AddSecMember(forStart, forMsg, view);
                        Tuple<IMaze, IMaze> firstmazes = this.mazes.GetMazesForGame(nameGame);
                        Tuple<IMaze, IMaze> secMazes = new Tuple<IMaze, IMaze>(firstmazes.Item2, firstmazes.Item1);
                        theGame.FirstPlayerMaze.UpdateView(new Tuple<string, Tuple<IMaze, IMaze>>(nameGame, (firstmazes)),
                            theGame.viewFirstPlayer);
                        theGame.SecPlayerMaze.UpdateView(new Tuple<string, Tuple<IMaze, IMaze>>(nameGame, (secMazes)),
                            theGame.ViewSecPlayer);
                        return;
                    }   
                }
            }
            /*if got to here it's meen that there is no game with the nameGame that
             *  have place to the new player*/
            this.games.Add(new Game(nameGame, forStart, forMsg, view));
        }

        /// <summary>
        /// update the other player that the player moved
        /// </summary>
        /// <param name="move">the movement</param>
        /// <param name="whoSend">who send the movemnt</param>
        public void PlayerMoved(string move,ISendableView whoSend)
        {
            Game game;
            //find the game that the player inside and send the other player that moved
            if ((game=this.games.Find(item=> item.IsPlayerHere(whoSend)))!=null)
            {
                if (whoSend.Equals(game.viewFirstPlayer))
                {
                    game.SendSecPlayer(move);
                }
                else
                {
                    game.SendFirstPlayer(move);
                }
            }
        }
        
        /// <summary>
        /// close all the game that have the nameGame, even if they did't played yet
        /// </summary>
        /// <param name="nameGame">the name of the game</param>
        /// <param name="whoSend">who send it</param>
        public void ColseGame(string nameGame, ISendableView whoSend)
        {
            this.games.RemoveAll(item => item.NameGame == nameGame);
        }
    }
}
