using Bank.Model;
using Bank.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bank.Controller
{
    internal class EmployeeController : IController
    {
        private List<IModel> employees;
        private DatabaseHelper dbHelper;
        private string connectionString; 

        public List<IModel> Items => employees;

        public EmployeeController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BankManagementDB"].ConnectionString; 
            dbHelper = new DatabaseHelper(connectionString);
            employees = new List<IModel>();
            Load();
        }

        public bool Create(IModel model)
        {
            var employee = model as EmployeeModel;
            if (employee.IsValidate())
            {

                string query = "INSERT INTO Employee (id, name, password, role) VALUES (@ID, @Name, @Password, @Role)";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", employee.id),
                    new SqlParameter("@Name", employee.name),
                    new SqlParameter("@Password", employee.password),
                    new SqlParameter("@Role", employee.role)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            return false;
        }

        public bool Update(IModel model)
        {
            var employee = model as EmployeeModel;
            if (employee.IsValidate())
            {
                string query = "UPDATE Employee SET name = @Name, password = @Password, role = @Role WHERE id = @ID";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", employee.id),
                    new SqlParameter("@Name", employee.name),
                    new SqlParameter("@Password", employee.password),
                    new SqlParameter("@Role", employee.role)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            return false;
        }

        public bool Delete(IModel model)
        {
            var employee = model as EmployeeModel;
            if (employee != null)
            {
                string query = "DELETE FROM Employee WHERE id = @ID";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", employee.id)
                };

                int result = dbHelper.ExecuteNonQuery(query, parameters);
                if (result > 0)
                {
                    employees.Remove(employee);
                    return true;
                }
            }
            return false;
        }

        public IModel Read(IModel model)
        {
            var employee = model as EmployeeModel;
            string query = "SELECT * FROM Employee WHERE id = @ID";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ID", employee.id)
            };

            DataTable result = dbHelper.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
            {
                return new EmployeeModel
                {
                    id = result.Rows[0]["id"].ToString(),
                    name = result.Rows[0]["name"].ToString(),
                    password = Convert.ToInt32(result.Rows[0]["password"]),
                    role = result.Rows[0]["role"].ToString()
                };
            }
            return null;
        }

        public bool Load()
        {
            employees.Clear();
            string query = "SELECT * FROM Employee";

            DataTable result = dbHelper.ExecuteQuery(query);
            foreach (DataRow row in result.Rows)
            {
                var employee = new EmployeeModel
                {
                    id = row["id"].ToString(),
                    name = row["name"].ToString(),
                    password = Convert.ToInt32(row["password"]),
                    role = row["role"].ToString()
                };
                employees.Add(employee);
            }

            // Thêm thông báo gỡ lỗi
            Console.WriteLine($"Đã nạp {employees.Count} nhân viên từ cơ sở dữ liệu.");

            return employees.Count > 0;
        }

        public bool IsExist(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false; 
            }

            using (SqlConnection conn = new SqlConnection(connectionString)) 
            {
                string query = "SELECT COUNT(*) FROM Employee WHERE id = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
