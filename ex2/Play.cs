using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ex2
{
    public class Play
    {
        /// <summary>
        /// classof move the yriv made
        /// </summary>
        public string Name { get; set; }
        public string Move { get; set; }
        /// <summary>
        /// constrcor 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="move"></param>
        public Play(string name,string move)
        {
            this.Name = name;
            this.Move = move;
        }
    }
}
