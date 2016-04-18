using Server.Model;
using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Modle
{
    public delegate void updateView(int id, string massege);
    public delegate void updateModel(int id, string massage);
    /// <summary>
    /// presenter. the connection between view and model
    /// </summary>
    class Presenter
    {
        public IView view;
        public IModel model;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="view"> view </param>
        /// <param name="model"> model </param>
        public Presenter(IView view, IModel model)
        {
            this.view = view;
            this.model = model;
            InitEvent();
        }

        /// <summary>
        /// Init an event
        /// </summary>
        private void InitEvent()
        {

            this.view.ViewChanged += delegate(int id, string result)
            {
                this.model.DoWork(id, result);
            };

            this.model.ModelChanged += delegate(int id , string s)
            {
                this.view.DisplayData(id, s);
            };
        }
    }
}
