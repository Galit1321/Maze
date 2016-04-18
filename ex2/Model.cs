using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Server.View;
using System.Threading.Tasks;

namespace ex2
{
    class Model : IModelable
    {
        
        public string ip
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Maze
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Port
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Pair posetion
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void connect(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public string createMaze()
        {
            
        }

        public void disconnect()
        {
            client.Send("close");
        }

        public string getClue()
        {
            throw new NotImplementedException();
        }

        public void move(string direction)
        {
            throw new NotImplementedException();
        }

        public void start()
        {
            throw new NotImplementedException();
        }
    }
}
