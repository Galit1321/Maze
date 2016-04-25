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
        public string Name { get; set; }
        public string Move { get; set; }
        [JsonConstructor]
        public Play(string name,string move)
        {
            this.Name = name;
            this.Move = move;
        }
    }
}
