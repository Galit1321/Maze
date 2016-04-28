using ServerExe1.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ServerExe1.src.view
{
    /// <summary>
    /// convert to Json for send to the client
    /// </summary>
    class ConvertJsonFormat : IConvertableView
    {
        /// <summary>
        /// convert maze to Json
        /// </summary>
        /// <param name="maze">the maze</param>
        /// <returns>the maze in JSon</returns>
        public string ConvertIMaze(IMaze maze)
        {

            Dictionary<string, string> forJson = new Dictionary<string, string>();
            forJson.Add("Name", maze.GetName());
            forJson.Add("Maze", maze.ToString());
            forJson.Add("Start", this.ConvertCoordinate(maze.GetStartPlace()));
            forJson.Add("End", this.ConvertCoordinate(maze.GetEndPlace()));
            string temp = JsonConvert.SerializeObject(forJson);
            //temp.Replace("\\", string.Empty);
            return temp;
        }

        /// <summary>
        /// convert Maze without name to Json
        /// </summary>
        /// <param name="maze">the maze</param>
        /// <returns>maze in Json</returns>
        public string ConvertIMazeWithoutName(IMaze maze)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, string> forJson = new Dictionary<string, string>();
            forJson.Add("Maze", maze.ToString());
            forJson.Add("Start", this.ConvertCoordinate(maze.GetStartPlace()));
            forJson.Add("End", this.ConvertCoordinate(maze.GetEndPlace()));
            string temp = JsonConvert.SerializeObject(forJson);
            // temp.Replace("\\", string.Empty);
            return temp;
        }

        /// <summary>
        /// convert coordinate in the matrix to json
        /// </summary>
        /// <param name="coor">the coordinate</param>
        /// <returns>the coordinate</returns>
        private string ConvertCoordinate(Tuple<int, int> coor)
        {

            string s = coor.Item1.ToString();
            s += "@";
            s += coor.Item2.ToString();
            return s;
        }

        /// <summary>
        /// convert solution to Json
        /// </summary>
        /// <param name="maze">the maze with the sol</param>
        /// <returns>the sol in JSon</returns>
        public string ConvertSolutionIMaze(IMaze maze)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, string> forJson = new Dictionary<string, string>();
            forJson.Add("Name", maze.GetName());
            forJson.Add("Maze", maze.PrintSolve());
            forJson.Add("Start", this.ConvertCoordinate(maze.GetStartPlace()));
            forJson.Add("End", this.ConvertCoordinate(maze.GetEndPlace()));
            string temp = JsonConvert.SerializeObject(forJson);

            return temp;
        }

        /// <summary>
        /// convert the all output to Json
        /// </summary>
        /// <param name="command">the command</param>
        /// <param name="Content">the content of the return vlaue</param>
        /// <returns>the return string</returns>
        public string ConvertOutput(string command, string Content)
        {
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //Dictionary<string, string> forJson = new Dictionary<string, string>();
            //forJson.Add("Type", command);
            //forJson.Add("Content", Content);
            //string temp = JsonConvert.SerializeObject(forJson);
            return Content;
        }

        /// <summary>
        /// convert movment on the graph
        /// </summary>
        /// <param name="gameName">the name of the game</param>
        /// <param name="move">the movement</param>
        /// <returns>the json </returns>
        public string ConvertPlay(string gameName, string move)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, string> forJson = new Dictionary<string, string>();
            forJson.Add("Name", gameName);
            forJson.Add("Move", move);
            string temp = JsonConvert.SerializeObject(forJson);
            return temp;
        }

        /// <summary>
        /// convert start game to JSon
        /// </summary>
        /// <param name="gameName">the game naem</param>
        /// <param name="you">the maze of the player</param>
        /// <param name="other">the maze of the other player</param>
        /// <returns>the output in Json</returns>
        public string ConvertStartGame(string gameName, IMaze you, IMaze other)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, string> forJson = new Dictionary<string, string>();
            forJson.Add("Name", gameName);
            forJson.Add("MazeName", you.GetName());
            forJson.Add("You", this.ConvertIMazeWithoutName(you));
            forJson.Add("Other", this.ConvertIMazeWithoutName(other));
            string temp = JsonConvert.SerializeObject(forJson);
           

            return temp;
        }
    }
}
