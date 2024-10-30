using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Model
{
    internal class AccountModel : IModel
    {
        public int id { get; set; }
        public string customerid { get; set; }
        public DateTime date_opened { get; set; }
        public float balance { get; set; }
        public bool IsValidate()
        {
            return true;
        }
    }

}
