using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Server.Modle
{
    /// <summary>
    /// Serialize the answer
    /// </summary>
    public class SerializeAnswer
    {
        
        public int Type { get; set; }
        public IAnswer Content { get; set; }

        /// <summary>
        /// empty constructor
        /// </summary>
        public SerializeAnswer() {}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="type"> type of answer </param>
        /// <param name="content"> the content </param>
        public SerializeAnswer(int type, IAnswer content)
        {
            this.Type = type;
            this.Content = content;
        }

        /// <summary>
        /// do the serialization
        /// </summary>
        /// <returns> returns the answer </returns>
        public string Serialize()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            return ser.Serialize(this.Content);
        }
    }
}
