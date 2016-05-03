using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using algoOnGraph;
/// <summary>
/// Class name: Graphs.
/// Extend classes: IMazeable.
/// Members: ICell[,] matrixOfCells;, List<ICell> myNodes, Dictionary<ICell, List<ICell>> reachable,
///          Dictionary<ICell, List<ICell>> openNeighbors, Tuple<double, double> dimension,
///          ICell Begin, ICell End and Dictionary<ICell, bool> isOnBounds;
///          
/// Function: Graphs(Cons'), Graphs(Copy cons'), SetUpReachAble, InitialCells, ChangeCellState, 
///           ChangeToBeginSituation, ChangeToEndSituation, GetAllNodes, GetGraphDimension,
///           GetRandomCell, GetReachableCells, GetUnvisitedNodes, IsBelongToBounds, IsTheEnd,
///           SetConnection, GetTheGraph, ToMatrix, ToString, GetNeighbors,
///           IsNeighbors and GetBeginning.
/// Summary: This calls is a one waz to implement the logic of the mazeable interface.
///          This class is combine the logic of matrix and graph representation of 
///          maze. We create the logic that each cell in the maze have the connection to its
///          neigbors that he can reach and also the its neighbors that he can not reach.
/// </summary>
namespace ServerExe1.src.model
{
    class Graphs : IMazeable
    {
        //The matrix that contain all of the nodes.
        private ICell[,] matrixOfCells;
        //List that contain all of the nodes.
        private List<ICell> myNodes;
        //Dictionary that connect each cell to his reachable neigbors.
        private Dictionary<ICell, List<ICell>> reachable;
        //Dictionary that connect each cell to his open neighbors neigbors.
        private Dictionary<ICell, List<ICell>> openNeighbors;
        //The dimension of the maze.
        private Tuple<double, double> dimension;
        //The beginning cell.
        public  ICell Begin { get;private set;}
        //The end cell.
        public ICell End { get;private set; }
        //The dictionary that tell me that if the cell is on the bounds or not.
        private Dictionary<ICell, bool> isOnBounds;

        /// <summary>
        /// Constructor of the graph.
        /// </summary>
        /// <param name="givenDimension"></param> the dimension of the maze.
        public Graphs(ref Tuple<double, double> givenDimension)
        {
            //Init the graph members.
            this.dimension = givenDimension;
            matrixOfCells = new ICell[(int)dimension.Item1, (int)dimension.Item1];
            reachable = new Dictionary<ICell, List<ICell>>();
            openNeighbors = new Dictionary<ICell, List<ICell>>();
            isOnBounds = new Dictionary<ICell, bool>();
            myNodes = new List<ICell>();
            //Real creation of the cells.
            InitialCells();
            SetUpReachAble();
        }


        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="old"></param> the graph that we want to copy.
        public Graphs(Graphs old)
        {
            this.dimension = new Tuple<double, double>(old.dimension.Item1, old.dimension.Item2);
            this.matrixOfCells = new ICell[(int)dimension.Item1, (int)dimension.Item1];
            for (int i = 0; i < this.dimension.Item1; i++)
            {
                for (int j = 0; j < this.dimension.Item2; j++)
                {
                    this.matrixOfCells[i, j] = old.matrixOfCells[i, j];
                }
            }
            this.myNodes = new List<ICell>(old.myNodes);
            this.reachable = new Dictionary<ICell, List<ICell>>(old.reachable);
            this.openNeighbors = new Dictionary<ICell, List<ICell>>(old.openNeighbors);
            this.Begin = old.Begin;
            this.End = old.End;
            this.isOnBounds = new Dictionary<ICell, bool>(old.isOnBounds);
        }


        /// <summary>
        /// The function is creating all of the dictionaries.
        /// </summary>
        private void SetUpReachAble()
        {
            int length = (int)dimension.Item1;

            //Init the reachable cells of the four corners.
            ICell temp = matrixOfCells[0, 0];
            List<ICell> firstList = new List<ICell>();
            firstList.Add(matrixOfCells[1, 0]); firstList.Add(matrixOfCells[0, 1]);
            reachable.Add(temp, firstList);

            ICell tempTwo = matrixOfCells[length - 1, 0];
            List<ICell> secondList = new List<ICell>();
            secondList.Add(matrixOfCells[length - 1, 1]); secondList.Add(matrixOfCells[length - 2, 0]);
            reachable.Add(tempTwo, secondList);

            ICell tempThree = matrixOfCells[0, length - 1];
            List<ICell> thirdList = new List<ICell>();
            thirdList.Add(matrixOfCells[0, length - 2]); thirdList.Add(matrixOfCells[1, length - 1]);
            reachable.Add(tempThree, thirdList);

            ICell tempFour = matrixOfCells[length - 1, length -1];
            List<ICell> forthList = new List<ICell>();
            forthList.Add(matrixOfCells[length - 2, length - 1]);
            forthList.Add(matrixOfCells[length - 1, length - 2]);
            reachable.Add(tempFour, forthList);



            //Initial the reachable cells of the first line in the matrix.
            for (int i = 1; i < length - 1; i++)
            {
                ICell tempICell = matrixOfCells[0, i];
                List<ICell> tempList = new List<ICell>();
                tempList.Add(matrixOfCells[0, i + 1]); tempList.Add(matrixOfCells[1, i]);
                tempList.Add(matrixOfCells[0, i - 1]);
                reachable.Add(tempICell, tempList);
            }
            //Initial the reachable cells of the last column in the matrix.
            for (int i = 1; i < length - 1; i++)
            {
                ICell tempICell = matrixOfCells[i, length - 1];
                List<ICell> tempList = new List<ICell>();
                tempList.Add(matrixOfCells[i - 1, length - 1]);
                tempList.Add(matrixOfCells[i + 1, length - 1]);
                tempList.Add(matrixOfCells[i, length - 2]);
                reachable.Add(tempICell, tempList);
            }

            //Initial the reachable cells of the last line in the matrix.
            for (int i = 1; i < length - 1; i++)
            {
                ICell tempICell = matrixOfCells[length - 1, i];
                List<ICell> tempList = new List<ICell>();
                tempList.Add(matrixOfCells[length - 1, i + 1]);
                tempList.Add(matrixOfCells[length - 1, i - 1]);
                tempList.Add(matrixOfCells[length - 2, i]);
                reachable.Add(tempICell, tempList);
            }

            //Initial the reachable cells of the first column in the matrix.
            for (int i = 1; i < length - 1; i++)
            {
                ICell tempICell = matrixOfCells[i, 0];
                List<ICell> tempList = new List<ICell>();
                tempList.Add(matrixOfCells[i, 1]);
                tempList.Add(matrixOfCells[i - 1, 0]);
                tempList.Add(matrixOfCells[i + 1, 0]);
                reachable.Add(tempICell, tempList);
            }


            //Initial the reachable cells of the inner matrix cell.
            for (int i = 1; i < length - 1; i++)
            {
                for (int j = 1; j < length - 1; j++)
                {
                    ICell tempICell = matrixOfCells[i, j];
                    List<ICell> tempList = new List<ICell>();
                    tempList.Add(matrixOfCells[i, j + 1]);
                    tempList.Add(matrixOfCells[i, j - 1]);
                    tempList.Add(matrixOfCells[i + 1, j]);
                    tempList.Add(matrixOfCells[i - 1, j]);
                    reachable.Add(tempICell, tempList);
                }
            }
        }


        /// <summary>
        /// The function is creating and init the matrix and allNodes cell.
        /// </summary>
        private void InitialCells()
        {
            int length = (int)dimension.Item1;
            int count = 0;
            //Creating the matrix length.
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Cell tempCell = new Cell(count);
                    matrixOfCells[i, j] = tempCell;
                    myNodes.Add(tempCell);
                    //Check if the current cell is on bounds.
                    if (i == 0 || j == 0 || i == length - 1 || j == length - 1)
                    { isOnBounds.Add(tempCell, true); }
                    else { isOnBounds.Add(tempCell, false); }
                    openNeighbors.Add(tempCell, new List<ICell>());
                    count++;
                }
            }
        }

        /// <summary>
        /// The function handle if in the creation we reached to the given cell.
        /// In the function we are making this cell as unreachable.
        /// </summary>
        /// <param name="src"></param> the givem cell.
        public void ChangeCellState(ICell src)
        {
            
            if (!myNodes.Contains(src)) { return; }
            src.SetReached();
            List<ICell> canReach = null;
            
            foreach (ICell item in reachable.Keys)
            {
                if (src.Equals(item)) { canReach = reachable[item]; break; }
            }

            foreach (List<ICell> item in reachable.Values)
            {
                item.Remove(src);
            } 
        }

        /// <summary>
        /// Change the beignning cell to the given cell.
        /// </summary>
        /// <param name="start"></param> The given begin cell.
        public void ChangeToBeginSituation(ICell start)
        {
            this.Begin = start;
        }
        /// <summary>
        /// Change the end cell to the given cell.
        /// </summary>
        /// <param name="end"></param> The given end cell.
        public void ChangeToEndSituation(ICell end)
        {
            this.End = end;
        }

        /// <summary>
        /// Return the nodes list.
        /// </summary>
        /// <returns></returns>
        public List<ICell> GetAllNodes()
        {
            return this.myNodes;
        }

        /// <summary>
        /// Return the graph dimension.
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double> GetGraphDimension()
        {
            return this.dimension;
        }

        /// <summary>
        /// Return random cell from the bounds.
        /// </summary>
        /// <returns></returns>
        public ICell GetRandomCell()
        {
            Random rnd = new Random();
            List<ICell> bounds = new List<ICell>();
            //Get all the cells that are on the bounds.
            foreach (ICell item in myNodes)
            {
                if (isOnBounds[item]) { bounds.Add(item); }
            }
            return bounds[rnd.Next(0, bounds.Count)];
        }


        /// <summary>
        /// Return the list of the cells that the given cell can reach.
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public List<ICell> GetReachableCells(ICell current)
        {
            List<ICell> tempList = null;
            foreach (ICell item in reachable.Keys)
            {
                if (item.Equals(current)) { tempList = reachable[item]; break; }
            }
            return tempList;
        }

        /// <summary>
        /// The functoin evaluate all of the unvisited cell, and return the list of this cell.
        /// </summary>
        /// <returns></returns>
        public List<ICell> GetUnvisitedNodes()
        {
            List<ICell> unvisited = new List<ICell>();
            foreach (ICell x in this.myNodes)
            {
                if (!x.GetReached()) { unvisited.Add(x); }
            }
            return unvisited;
        }

        /// <summary>
        /// The function check if the given cell is on one of the bounds.
        /// </summary>
        /// <param name="src"></param> The given cell.
        /// <returns></returns>
        public bool IsBelongToBounds(ICell src)
        {
            foreach (ICell item in isOnBounds.Keys)
            {
                if (item.Equals(src)) { return isOnBounds[item]; }
            }
            return false;
        }

        /// <summary>
        /// Check if the given cell is the end cell.
        /// </summary>
        /// <param name="dst"></param> The given cell.
        /// <returns></returns>
        public bool IsTheEnd(ICell dst)
        {
            return this.End == dst;
        }

        /// <summary>
        /// The function adding the first cell to the second cell list of neighbors,
        /// and do so for the opposite.
        /// </summary>
        /// <param name="src"></param> One side of the edge.
        /// <param name="dst"></param> Second side of the edge.
        public void SetConnection(ICell src, ICell dst)
        {
            ICell temp1 = null, temp2= null;
            foreach (ICell item in openNeighbors.Keys)
            {
                if (item.Equals(src)) { temp1 = item; }
                else if (item.Equals(dst)) { temp2 = item; }
            }
            if (openNeighbors[temp1].Contains(temp2) || openNeighbors[temp2].Contains(temp1)) { return; }
            openNeighbors[temp1].Add(temp2);
            openNeighbors[temp2].Add(temp1);
        }

        /// <summary>
        /// Return the 2D array of the cells.
        /// </summary>
        /// <returns></returns>
        public ICell[,] GetTheGraph()
        {
            return this.matrixOfCells;
        }

        /// <summary>
        /// The function is convert from the maze to int's matrix, and return it.
        /// </summary>
        /// <returns></returns>
        public int[,] ToMatrix()
        {
            int x = (int)((this.dimension.Item1 * 2) - 1);
            int[,] c = new int[x, x];
            //Init the matrix to 1's.
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    c[i, j] = 1;
                }
            }

            int length = (int)this.dimension.Item1;
            int a = 0, b = 0;
            int pow = (int)(this.dimension.Item1 * this.dimension.Item1);

            for (int i = 0; i < pow; i++)
            {
                c[a, b] = 0;
                ICell ctemp = myNodes[i];
                List<ICell> l = openNeighbors[ctemp];
                for (int k = 0; k < l.Count; k++)
                {
                    //If the cell is open.
                    if (ctemp.GetPlace() % ((int)this.dimension.Item1) != (int)(this.dimension.Item1 - 1)
                        && l[k].GetPlace() == ctemp.GetPlace() + 1)
                    {
                        c[a, b + 1] = 0;
                    }
                    
                    if (ctemp.GetPlace() <= (int)(this.dimension.Item1 * (this.dimension.Item1 - 1)) &&
                        l[k].GetPlace() == ctemp.GetPlace() + (int)(this.dimension.Item1))
                    {
                        c[a + 1, b] = 0;
                    }

                }
                //Go to the start of the next line.
                if (ctemp.GetPlace() % (int)(this.dimension.Item1) == ((int)this.dimension.Item1 - 1))
                {
                    a += 2; b = 0;
                    continue;
                }
                b += 2;
            }
            Tuple<int, int> t;
           while ((t = IsGotMorePaths(c)).Item1 != -1)
            {
                int i = t.Item1, j = t.Item2;
                c[i,j] = 0;
                if (i != 0 && c[i - 1, j] == 2)
                {
                    c[i - 1, j] = 0;
                    continue;
                }
                if (i != x - 1 && c[i + 1, j] == 2)
                {
                    c[i + 1, j] = 0;
                    continue;
                }
                if (j != 0 && c[i, j - 1] == 2)
                {
                    c[i, j - 1] = 0;
                    continue;
                }
                if (j != x - 1 && c[i, j + 1] == 2)
                {
                    c[i, j + 1] = 0;
                    continue;
                }
            }
            return c;
        }

        private Tuple<int,int> IsGotMorePaths(int[,] c)
        {
            int numOfOpenTwos = 0;
            int x = (int)(this.dimension.Item1);
            for (int i = 0; i < x; i+=2)
            {
                for (int j = 0; j < x; j+=2)
                {
                    numOfOpenTwos = 0;
                    if (c[i,j] == 2) {
                       if (matrixOfCells[i,j] == this.Begin || matrixOfCells[i, j] == this.End)
                        {
                            continue;
                        }
                        
                        if (i != 0 && c[i-1,j] == 2)
                        {
                            numOfOpenTwos++;
                        }
                        if (i != x -1 && c[i + 1, j] == 2)
                        {
                            numOfOpenTwos++;
                        }
                        if (j != 0 && c[i, j - 1] == 2)
                        {
                            numOfOpenTwos++;
                        }
                        if (j != x - 1 && c[i, j + 1] == 2)
                        {
                            numOfOpenTwos++;
                        }
                        if (numOfOpenTwos < 2)
                        {
                            return new Tuple<int, int>(i, j);
                        }

                    }
                }
            }
            return new Tuple<int, int>(-1, -1); ;
        }

        /// <summary>
        /// The function get the int's matrix and convert it to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string s = "";
            int x = (int)((this.dimension.Item1 * 2) - 1);
            int[,] c = ToMatrix();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    s += c[i, j];
                }
                s += '\n';
            }
            return s;
        }

        /// <summary>
        /// Return the open neighbors of the given cell.
        /// </summary>
        /// <param name="src"></param> the given cell.
        /// <returns></returns>
        public List<ICell> GetNeighbors(ICell src)
        {
            foreach (ICell item in myNodes)
            {
                if (src == item) { return openNeighbors[item]; }
            }
            return null;
        }

        /// <summary>
        /// Return the begin cell.
        /// </summary>
        /// <returns></returns>
        public ICell GetBeginning()
        {
            return this.Begin;
        }

        /// <summary>
        /// Check if two given cells are neighbors.
        /// </summary>
        /// <param name="src"></param> first cell.
        /// <param name="dst"></param> second cell.
        /// <returns></returns>
        public bool IsNeighbors(ICell src, ICell dst)
        {
            List<ICell> tempList = null;
            foreach (ICell item in myNodes)
            {
                if (src == item) { tempList = openNeighbors[item]; }
            }
            if (tempList == null) { return false; }
            foreach (ICell item in tempList)
            {
                if (dst == item) { return true; }
            }
            return false;
        }
    }
}
