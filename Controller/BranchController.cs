using Bank.Model;
using Bank.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bank.Controller
{
    internal class BranchController : IController
    {
        private List<IModel> branches;
        private DatabaseHelper dbHelper;
        private string connectionString;

        public List<IModel> Items => branches;

        public BranchController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BankManagementDB"].ConnectionString;
            dbHelper = new DatabaseHelper(connectionString);
            branches = new List<IModel>();
            Load();
        }

        public bool Create(IModel model)
        {
            var branch = model as BranchModel;
            if (branch.IsValidate())
            {
                string query = "INSERT INTO Branch (id, name, city) VALUES (@ID, @Name, @City)";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", branch.id),
                    new SqlParameter("@Name", branch.name),
                    new SqlParameter("@City", branch.city)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            return false;
        }

        public bool Update(IModel model)
        {
            var branch = model as BranchModel;
            if (branch.IsValidate())
            {
                string query = "UPDATE Branch SET name = @Name, city = @City WHERE id = @ID";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", branch.id),
                    new SqlParameter("@Name", branch.name),
                    new SqlParameter("@City", branch.city)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            return false;
        }

        public bool Delete(IModel model)
        {
            var branch = model as BranchModel;
            if (branch != null)
            {
                string query = "DELETE FROM Branch WHERE id = @ID";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", branch.id)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    branches.Remove(branch);
                    return true;
                }
            }
            return false;
        }

        public IModel Read(IModel model)
        {
            var branch = model as BranchModel;
            string query = "SELECT * FROM Branch WHERE id = @ID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", branch.id)
            };

            DataTable result = dbHelper.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
            {
                return new BranchModel
                {
                    id = result.Rows[0]["id"].ToString(),
                    name = result.Rows[0]["name"].ToString(),
                    city = result.Rows[0]["city"].ToString()
                };
            }
            return null;
        }

        public bool Load()
        {
            branches.Clear();
            string query = "SELECT * FROM Branch";

            DataTable result = dbHelper.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                var branch = new BranchModel
                {
                    id = row["id"].ToString(),
                    name = row["name"].ToString(),
                    city = row["city"].ToString()
                };
                branches.Add(branch);
            }

            Console.WriteLine($"Đã nạp {branches.Count} chi nhánh từ cơ sở dữ liệu.");
            return branches.Count > 0;
        }

        public bool IsExist(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Branch WHERE id = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
