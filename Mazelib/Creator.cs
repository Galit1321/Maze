using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mazelib;

namespace Mazelib
{
    /// <summary>
    /// The class implements ICreator
    /// </summary>
    public abstract class Creator : ICreator
    {
        protected List<IVertex> maze;
        protected IVertex start;
        protected IVertex end;

        /// <summary>
        /// create maze given empty maze
        /// </summary>
        /// <param name="emptyMaze">
        /// empty maze </param>
        public void Create(IMaze<IVertex> emptyMaze)
        {
            this.maze = emptyMaze.GetIVertex();
            //chose random start point
            Random rand = new Random();
            int startIndex = rand.Next(emptyMaze.GetWidth()*emptyMaze.Gethight()-1);
            // call to the abstract method
            this.start = maze[startIndex];
            CreateHelper(emptyMaze.GetWidth(), emptyMaze.Gethight(), startIndex);

            emptyMaze.SetStartEndPoints(this.start, this.end);
        }

        /// <summary>
        /// abstract helper function
        /// </summary>
        /// <param name="width"> the width </param>
        /// <param name="height"> the height </param>
        /// <param name="start">the start point location at list </param>
        public abstract void CreateHelper(int width, int height, int start);

    }
}
