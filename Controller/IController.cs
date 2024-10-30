using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Controller
{
    internal interface IController
    {
        List<IModel> Items { get; }
        public bool Create(IModel model);
        public bool Update(IModel model);
        public bool Delete(IModel id);
        public IModel Read(IModel id);
        public bool Load();
        public bool IsExist(string id);

    }
}
