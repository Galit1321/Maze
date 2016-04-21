using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class Play
    {
        public string Name { get; set; }
        public string Move { get; set; }

        public Play(string name,string move)
        {
            this.Name = name;
            this.Move = move;
        }
    }
}
