using Bank.Model;
using Bank.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bank.Controller
{
    internal class CustomerController : IController
    {
        private List<IModel> customers;
        private DatabaseHelper dbHelper;
        private string connectionString; 

        public List<IModel> Items => customers;

        public CustomerController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BankManagementDB"].ConnectionString; 
            dbHelper = new DatabaseHelper(connectionString);
            customers = new List<IModel>();
            Load();
        }

        public bool Create(IModel model)
        {
            var customer = model as CustomerModel;
            if (customer.IsValidate())
            {
                string query = "INSERT INTO Customer (id, name, phone, email, city, pin) VALUES (@ID, @Name, @Phone, @Email, @City, @Pin)";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", customer.id),
                    new SqlParameter("@Name", customer.name),
                    new SqlParameter("@Phone", customer.phone),
                    new SqlParameter("@Email", customer.email),
                    new SqlParameter("@City", customer.city),
                    new SqlParameter("@Pin", customer.pin)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            return false;
        }

        public bool Update(IModel model)
        {
            var customer = model as CustomerModel;
            if (customer.IsValidate())
            {
                string query = "UPDATE Customer SET name = @Name, phone = @Phone, email = @Email, city = @City, pin = @Pin WHERE id = @ID";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", customer.id),
                    new SqlParameter("@Name", customer.name),
                    new SqlParameter("@Phone", customer.phone),
                    new SqlParameter("@Email", customer.email),
                    new SqlParameter("@City", customer.city),
                    new SqlParameter("@Pin", customer.pin)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            return false;
        }

        public bool Delete(IModel model)
        {
            var customer = model as CustomerModel;
            if (customer != null)
            {
                string query = "DELETE FROM Customer WHERE id = @ID";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", customer.id)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    customers.Remove(customer);
                    return true;
                }
            }
            return false;
        }

        public IModel Read(IModel model)
        {
            var customer = model as CustomerModel;
            string query = "SELECT * FROM Customer WHERE id = @ID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", customer.id)
            };

            DataTable result = dbHelper.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
            {
                return new CustomerModel
                {
                    id = result.Rows[0]["id"].ToString(),
                    name = result.Rows[0]["name"].ToString(),
                    phone = result.Rows[0]["phone"].ToString(),
                    email = result.Rows[0]["email"].ToString(),
                    city = result.Rows[0]["city"].ToString(),
                    pin = result.Rows[0]["pin"].ToString()
                };
            }
            return null;
        }

        public bool Load()
        {
            customers.Clear();
            string query = "SELECT * FROM Customer";

            DataTable result = dbHelper.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                var customer = new CustomerModel
                {
                    id = row["id"].ToString(),
                    name = row["name"].ToString(),
                    phone = row["phone"].ToString(),
                    email = row["email"].ToString(),
                    city = row["city"].ToString(),
                    pin = row["pin"].ToString()
                };
                customers.Add(customer);
            }

            Console.WriteLine($"Đã nạp {customers.Count} khách hàng từ cơ sở dữ liệu.");

            return customers.Count > 0;
        }

        public bool IsExist(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false; 
            }

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                string query = "SELECT COUNT(*) FROM Customer WHERE id = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
