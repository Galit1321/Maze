using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    interface IModelable
    {
        
            void connect(string ip, int port);
            void disconnect();
            void start();

            //proprties here
            int Port { get; set; }
            string ip { get; set; }
            string Order { get; set; }
            
            //active method
            void move(string direction);
            string getClue();
            

        
    }
}
