using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Model
{
    internal class TransactionModel : IModel
    {
        public int id { get; set; }
        public int? from_account_id { get; set; }
        public string branch_id { get; set; }
        public DateTime? date_of_trans { get; set; }
        public float? amount { get; set; }
        public int? pin { get; set; }
        public int? to_account_id { get; set; }
        public string employee_id { get; set; }
        public TransactionModel() { }

        public bool IsValidate()
        {
            return true;
        }
    }
}
