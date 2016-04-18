using Mazelib;
using Server.Model;
using Server.Modle;
using Server.View;
using Server.ViewS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            DataBase db = DataBase.Instance;
            db.Read();
            ServerView view = new ServerView();
            ServerModel model = new ServerModel();
            Presenter present = new Presenter(view, model);
            Connect connect = new Connect();
            view.ConnectClient(connect.InitConnect());
        }
    }
}
