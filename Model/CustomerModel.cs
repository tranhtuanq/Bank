using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Model
{
    internal class CustomerModel: IModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string pin { get; set; }
        public bool IsValidate()
        {
            return true;
        }
    }
}
