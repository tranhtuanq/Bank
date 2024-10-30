using Bank.Model;
using Bank.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Bank.Controller
{
    internal class AccountController
    {
        private List<IModel> accounts; 
        private DatabaseHelper dbHelper; 
        private string connectionString; 

        public List<IModel> Items => accounts; 

        public AccountController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BankManagementDB"].ConnectionString;
            dbHelper = new DatabaseHelper(connectionString);
            accounts = new List<IModel>();
            Load(); 
        }

        public bool Create(IModel model)
        {
            var account = model as AccountModel; 
            if (account != null && account.IsValidate())
            {
                string query = "INSERT INTO Account (id, customerid, date_opened, balance) VALUES (@ID, @CustomerId, @Date_Opened, @Balance)";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", account.id),
                    new SqlParameter("@CustomerId", account.customerid),
                    new SqlParameter("@Date_Opened", account.date_opened),
                    new SqlParameter("@Balance", account.balance)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                return result > 0; 
            }
            return false;
        }

        public bool Update(IModel model)
        {
            var account = model as AccountModel;
            if (account != null && account.IsValidate())
            {
                string query = "UPDATE Account SET customerid = @CustomerId, date_opened = @Date_Opened, balance = @Balance WHERE id = @ID";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", account.id),
                    new SqlParameter("@CustomerId", account.customerid),
                    new SqlParameter("@Date_Opened", account.date_opened),
                    new SqlParameter("@Balance", account.balance)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                return result > 0; 
            }
            return false;
        }

        public bool Delete(IModel model)
        {
            var account = model as AccountModel;
            if (account != null)
            {
                string query = "DELETE FROM Account WHERE id = @ID"; 
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", account.id)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    accounts.Remove(account); 
                    return true; 
                }
            }
            return false; 
        }
        
        public IModel Read(IModel model)
        {
            var account = model as AccountModel; 
            string query = "SELECT * FROM Account WHERE id = @ID"; 
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", account.id)
            };

            DataTable result = dbHelper.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
            {
                return new AccountModel 
                {
                    id = Convert.ToInt32(result.Rows[0]["id"]),
                    customerid = result.Rows[0]["customerid"].ToString(),
                    date_opened = Convert.ToDateTime(result.Rows[0]["date_opened"]),
                    balance = Convert.ToSingle(result.Rows[0]["balance"])
                };
            }
            return null;
        }
        public bool Load()
        {
            accounts.Clear();
            string query = "SELECT * FROM Account";

            DataTable result = dbHelper.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                var account = new AccountModel
                {
                    id = Convert.ToInt32(row["id"]),
                    customerid = row["customerid"].ToString(),
                    date_opened = Convert.ToDateTime(row["date_opened"]),
                    balance = Convert.ToSingle(row["balance"])
                };
                accounts.Add(account); 
            }

            
            Console.WriteLine($"Đã nạp {accounts.Count} tài khoản từ cơ sở dữ liệu.");

            return accounts.Count > 0; 
        }

        public bool IsExist(int id) 
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Account WHERE id = @ID"; 
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id); 
                conn.Open();

                int count = (int)cmd.ExecuteScalar(); 
                return count > 0;
            }
        }
    }
}
