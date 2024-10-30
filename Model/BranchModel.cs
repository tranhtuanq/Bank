using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Model
{
    internal class BranchModel:IModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string city { get; set; }

        public bool IsValidate()
        {
            return true;
        }
    }
}
