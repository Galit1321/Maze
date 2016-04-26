using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using algoOnGraph;
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;

namespace ServerExe1.src.model
{
    /// <summary>
    /// maze
    /// </summary>
    class Maze:IMaze
    {
        private const string placeSizeApp = "SizeMaze";
        private const int defaultSolve = 0;
        public string Name{get;set;}
        private Graphs theGraph;
        private SolutionMaze solve;
        public string PrintOfMaze{get; set;}
        public string PrintOfSolution{get; set;}
        private Tuple<int, int> start;
        public string StartPrint
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Append(start.Item1.ToString());
                s.Append(" ");
                s.Append(start.Item2.ToString());
                return s.ToString();
            }
            set
            {
                string[] s = value.Split(' ');
                this.start = new Tuple<int, int>(Int32.Parse(s[0]), Int32.Parse(s[1]));
            }
        }
        private Tuple<int, int> end;
        public string EndPoint
        {
            get
            {
                StringBuilder s = new StringBuilder();
                s.Append(this.end.Item1.ToString());
                s.Append(" ");
                s.Append(this.end.Item2.ToString());
                return s.ToString();
            }
            set
            {
                string[] e = value.Split(' ');
                this.end = new Tuple<int, int>(Int32.Parse(e[0]), Int32.Parse(e[1]));
            }
        }

        /// <summary>
        /// crete the maze
        /// </summary>
        /// <param name="name">the naem</param>
        /// <param name="type">the type of the creating</param>
        public Maze(string name, int type)
        {
            this.solve = null;
            this.Name = name;
            int sizeMaze = this.GetSizeMaze();
            Tuple<double, double> tSize = new Tuple<double, double>(sizeMaze,sizeMaze);
            this.theGraph = new Graphs(ref tSize);
            //apply the maze on the graph
            new FactoryMazeable().CreateTheMaze(theGraph, type);
            //save the printing
            this.PrintOfMaze = this.ToString();
            this.PrintOfSolution = null;
            //get the end and the start of the maze
            this.start = this.GetPlaceCellInMatrix(this.theGraph.Begin);
            this.end = this.GetPlaceCellInMatrix(this.theGraph.End);            
        }

        /// <summary>
        /// for the Json
        /// </summary>
        public Maze(){}

        /// <summary>
        /// for copy maze
        /// </summary>
        /// <param name="old">old maze to copy</param>
        /// <param name="newName">the new name of the maze</param>
        public Maze(Maze old, string newName)
        {
            this.Name = newName;
            if (old.theGraph != null)
            {
                this.theGraph = new Graphs(old.theGraph);
            }
            if (old.solve != null)
            {
                this.solve = new SolutionMaze(old.solve);
            }
            this.start = old.GetStartPlace();
            this.end = old.GetEndPlace();
            this.PrintOfMaze = old.PrintOfMaze;
            this.PrintOfSolution = old.PrintOfSolution;
        }

        /// <summary>
        /// get the name of the maze
        /// </summary>
        /// <returns>the name of the maze</returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// solve the maze and save the printing of the sol
        /// </summary>
        /// <param name="type">the type of the solving</param>
        public void SolveMaze(int type)
        {
            if (this.solve != null | this.PrintOfSolution != null)
            {
                return;
            }
            this.solve = new SolutionMaze(new FactorySolvable().SolveTheMaze(this.theGraph, type));
            this.PrintOfSolution = this.PrintSolve();
        }

        /// <summary>
        /// get the second maze of this maze, to reverse the end and the start
        /// </summary>
        /// <param name="newName">the new name</param>
        /// <returns>the new reverse maze</returns>
        public IMaze GetSecondMaze(string newName)
        {
            Maze newMaze = new Maze(this, newName);
            newMaze.theGraph.ChangeToBeginSituation(this.theGraph.End);
            newMaze.theGraph.ChangeToEndSituation(this.theGraph.Begin);
            newMaze.start = this.end;
            newMaze.end = this.start;
            return newMaze;
        }

        /// <summary>
        /// print the maze
        /// </summary>
        /// <returns>the printing of the maze</returns>
        public override string ToString()
        {
            if (this.PrintOfMaze != null)
            {
                return this.PrintOfMaze;
            }
            int sizeMatix = this.GetSizeMatrix();
            int[,] con = this.theGraph.ToMatrix();
            //this.PrintMat(con, this.GetSizeMatrix());
            this.PrintOfMaze = this.ConvertMatrixToString(con, sizeMatix);
            return PrintOfMaze;
        }

        /// <summary>
        /// convert matrix to string
        /// </summary>
        /// <param name="matrix">the matrx </param>
        /// <param name="sizeMatrix">the size of the matrix</param>
        /// <returns>the strig of the matrix</returns>
        private string ConvertMatrixToString(int[,] matrix, int sizeMatrix)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    s.Append(matrix[i, j]);
                }
            }
            return s.ToString();
        }

        /// <summary>
        /// according to the all Matrix size, get the place of the cell
        /// </summary>
        /// <param name="cell">the cell to check</param>
        /// <returns>the place in the matrix, according to the id</returns>
        private Tuple<int, int> GetPlaceCellInMatrix(ICell cell)
        {
            int size = Int32.Parse(ConfigurationManager.AppSettings[placeSizeApp]);
            int i = cell.GetPlace() / size;
            int j = cell.GetPlace() % size;
            return new Tuple<int, int>(i * 2, j * 2);
        }

        /// <summary>
        /// check if the maze solved
        /// </summary>
        /// <returns>true if solved, otherwise false</returns>
        public bool IsSolved(){
            if(this.solve!=null||this.PrintOfSolution!=null){
                return true;
            }
            return false;
        }

        /// <summary>
        /// solve if needed with defalt, and return the solved maze
        /// </summary>
        /// <returns>the solved maze string</returns>
        public string PrintSolve()
        {
            if (this.solve == null)
            {
                this.SolveMaze(Maze.defaultSolve);
            }
            
            if (this.PrintOfSolution != null)
            {
                return this.PrintOfSolution;
            }
            int[,] con = this.theGraph.ToMatrix();
            foreach (ICell cell in this.solve.GetList())
            {
                Tuple<int, int> place = this.GetPlaceCellInMatrix(cell);
                int i = place.Item1, j = place.Item2;
                //Console.WriteLine("i: "+place.Item1.ToString()+", j: "+place.Item2.ToString());
                con[place.Item1, place.Item2] = 2;
            }
            for (int i = 0; i < this.GetSizeMatrix(); i += 2)
            {
                for (int j = 0; j < this.GetSizeMatrix(); j+=2)
                {
                    if(i!=0 && con[i,j]==2 && con[i-1,j]==0 && con[i-2,j]==2){
                        con[i - 1, j] = 2;
                    }
                    if (j != 0 && con[i, j] == 2 && con[i, j - 1] == 0 && con[i, j - 2] == 2)
                    {
                        con[i, j - 1] = 2;
                    }
                }
            }
            //this.PrintMat(con, this.GetSizeMatrix());
            this.PrintOfSolution = this.ConvertMatrixToString(con, this.GetSizeMatrix());
            return this.PrintOfSolution;
        }

        /// <summary>
        /// get te size of the maze
        /// </summary>
        /// <returns>the size of the maze itself</returns>
        private int GetSizeMaze()
        {
            return Int32.Parse(ConfigurationManager.AppSettings[placeSizeApp]);
        }

        /// <summary>
        /// get the size of the matrix persentation of the maze
        /// </summary>
        /// <returns>the size of the matrix of the maze</returns>
        private int GetSizeMatrix()
        {
            int size = this.GetSizeMaze();
            return size + size - 1;
        }

        /// <summary>
        /// print matrix-for checking
        /// </summary>
        /// <param name="matrix">the matrix to print</param>
        /// <param name="size">the size</param>
        private void PrintMat(int[,] matrix, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// get the start place of the maze
        /// </summary>
        /// <returns>the start place</returns>
        public Tuple<int, int> GetStartPlace()
        {
            return this.start;
        }

        /// <summary>
        /// get the end place of the maze
        /// </summary>
        /// <returns>end place of the maze</returns>
        public Tuple<int, int> GetEndPlace()
        {
            return this.end;
        }
    }
}