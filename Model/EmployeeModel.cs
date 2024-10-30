using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bank.Model
{
    internal class EmployeeModel : IModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public int password { get; set; }
        public string role { get; set; }

        public EmployeeModel() { }

        public bool IsValidate()
        {
            return true;
        }
    }
}