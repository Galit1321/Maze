using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerExe1.src.view;
using ServerExe1.src.Presentor;
using ServerExe1.src.model;


namespace ServerEx1
{
    class Program
    {
        static void Main(string[] args)
        {
            IModel model = new MainModel();
            IMainView view = new ConnectClients();
            IMainPresentor controller = new MainPresentor(new ConvertJsonFormat(), view, model);
            controller.Start();

        }
    }
}
