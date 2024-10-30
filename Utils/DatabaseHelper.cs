using System;
using System.Data;
using System.Data.SqlClient;

namespace Bank.Utils
{
    public class DatabaseHelper
    {
        private string _connectionString;
        

        // Constructor để khởi tạo với chuỗi kết nối
        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Phương thức mở kết nối
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Phương thức để thực thi câu lệnh không trả về kết quả (INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Thêm phương thức để thực thi với transaction
        public int ExecuteNonQuery(string query, SqlTransaction transaction, SqlParameter[] parameters = null)
        {
            SqlCommand cmd = transaction.Connection.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandText = query;
           

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            return cmd.ExecuteNonQuery();
        }

        // Phương thức để thực thi câu lệnh trả về kết quả (SELECT)
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);  // Đổ dữ liệu vào DataTable
                        return resultTable;
                    }
                }
            }
        }

        // Phương thức để thực thi câu lệnh trả về một giá trị duy nhất (ví dụ: COUNT, MAX, MIN)
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteScalar();  // Trả về giá trị đầu tiên của cột đầu tiên
                }
            }
        }
    }
}
