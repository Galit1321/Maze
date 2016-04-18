using Mazelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    /// <summary>
    /// Vertex, a part of the maze
    /// </summary>
    public class Vertex : IVertex
    {
        private IVertex[] adj;
        private int value;
        public Location location { get; set; }
        private Random rnd = new Random();
        public bool visted { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="location"> location </param>
        /// <param name="value"> value </param>
        public Vertex(Location location, int value)
        {
            this.adj = new IVertex[4]{null,null,null,null};
            this.location = location;
            this.value = value;
            visted = false;
        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="other"> other vertex to copy </param>
        public Vertex(Vertex other)
        {
            this.adj = new IVertex[4] { null, null, null, null };
            this.location = new Location(other.location);
            this.value = other.value;
            visted = false;
        }

        /// <summary>
        /// get the number of possible neighbours
        /// </summary>
        /// <returns> returns the number of neighbours </returns>
        public int NumOfNeighbs()
        {
            int num = 4;
            for (int i = 0; i < 4; i++)
            {
                if (this.adj[i] ==(null))
                {
                    num--;
                }
                else if (this.adj[i].GetValue() == -1)
                {
                    num--;
                }
            }
            return num;
        }

        /// <summary>
        /// add new neighbour given the side
        /// </summary>
        /// <param name="neighbor"> the vertex to add </param>
        /// <param name="type"> the location to add the new neighbour </param>
        public void AddNeighbor(IVertex neighbor, int type)
        {
            if (type == (int)direction.UP)
            {
                this.adj[(int)direction.UP] = neighbor;
            }
            else if (type == (int)direction.RIGHT)
            {
                this.adj[(int)direction.RIGHT] = neighbor;
            }
            else if (type == (int)direction.DOWN)
            {
                this.adj[(int)direction.DOWN] = neighbor;
            }
            else
            {
                this.adj[(int)direction.LEFT] = neighbor;
            }
        }

        /// <summary>
        /// get the walls of this vertex
        /// </summary>
        /// <returns> returns an array of 4 walls, 1 for wall and 0 if there is not wall </returns>
        public List<int> GetWall()
        {
            List<int> wall = new List<int>();
            if (adj[(int)direction.UP] == null)
            {
                wall.Add((int)direction.UP);
            }
            if (adj[(int)direction.RIGHT] == null)
            {
                wall.Add((int)direction.RIGHT);
            }
            if (adj[(int)direction.DOWN] == null)
            {
                wall.Add((int)direction.DOWN);
            }
            if (adj[(int)direction.LEFT] == null)
            {
                wall.Add((int)direction.LEFT);
            }
            return wall;
        }

        /// <summary>
        /// get the possible neighbours
        /// </summary>
        /// <returns> returns an array of neighbours </returns>
        public IVertex[] GetAdj()
        {
            return this.adj;
        }

        /// <summary>
        /// get the value of vertex
        /// </summary>
        /// <returns> returns the value </returns>
        public int GetValue()
        {
            return this.value;
        }

        /// <summary>
        /// get the location
        /// </summary>
        /// <returns> returns this vertex location </returns>
        public Location GetLocation()
        {
            return this.location;
        }

        /// <summary>
        /// check if this vertex is visited at the search
        /// </summary>
        /// <returns> returns true if visited </returns>
        public bool IsViste()
        {
            return this.visted;
        }

        /// <summary>
        /// set if visited
        /// </summary>
        /// <param name="val"> the value to set </param>
        public void SetVisit(bool val)
        {
            this.visted = val;
        }

        /// <summary>
        /// compare with other vertwx
        /// </summary>
        /// <param name="other"> other vertex to compare </param>
        /// <returns> returns true if equals </returns>
        public override bool Equals(Object ot)
        {
            Vertex other = ot as Vertex;
            if (this.location.Equals(other.location))
            {
                return true;
            }
            return false;
        }
    }

}
