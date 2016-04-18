using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using Server.Modle;

namespace Server.Model
{
    /// <summary>
    /// this class is the data base of the project
    /// </summary>
    public class DataBase
    {
        public Dictionary<string, MazeSolved> mazeSolve { get; set; }
        public Dictionary<string, Game> Games { get; set; }
        public string fileName { get; set; } 
        private static volatile DataBase instance;
        private static object syncRoot = new Object();
        /// <summary>
        /// constructor
        /// </summary>
        private DataBase()
        {
            mazeSolve = new Dictionary<string, MazeSolved>();
            Games = new Dictionary<string, Game>();
            this.fileName = "File.json";
        }

        /// <summary>
        /// this class is sningelton
        /// </summary>
        public static DataBase Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataBase();
                    }
                }

                return instance;
            }
        }
        /// <summary>
        /// Write
        /// </summary>
        public void Write()
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();  
            File.WriteAllText(this.fileName,  ser.Serialize(mazeSolve));
        }

        /// <summary>
        /// Read
        /// </summary>
        public void Read()
        {
            if (File.Exists(this.fileName))
            {
                string input = File.ReadAllText(this.fileName);

                if (input != "")
                {
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    this.mazeSolve = ser.Deserialize<Dictionary<string, MazeSolved>>(input);
                }
            }
        }
    }
}
