using Bank.Model;
using Bank.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Bank.Controller
{
    internal class TransactionController : IController
    {
        private List<IModel> transactions;
        private DatabaseHelper dbHelper;
        private string connectionString;

        public List<IModel> Items => transactions;

        public TransactionController()
        {
            connectionString = ConfigurationManager.ConnectionStrings["BankManagementDB"].ConnectionString;
            dbHelper = new DatabaseHelper(connectionString);
            transactions = new List<IModel>();
            Load();
        }

        public bool Create(IModel model)
        {
            var transaction = model as TransactionModel;
            if (transaction.IsValidate())
            {
                string query = "INSERT INTO [Transaction] (to_account_id, date_of_trans, amount) VALUES (@ToAccountId, @DateOfTrans, @Amount)";
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@ToAccountId", transaction.to_account_id),
                    new SqlParameter("@DateOfTrans", transaction.date_of_trans),
                    new SqlParameter("@Amount", transaction.amount)
                };

                try
                {
                    int result = dbHelper.ExecuteNonQuery(query, parameters);
                    return result > 0; // Trả về true nếu có ít nhất 1 hàng bị ảnh hưởng
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi ghi giao dịch: {ex.Message}", "Lỗi");
                    return false;
                }
            }
            return false;
        }

        public bool Deposit(int accountId, float amount, string employeeId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Kiểm tra xem employeeId có tồn tại không
                        var checkEmployeeCommand = new SqlCommand("SELECT 1 FROM Employee WHERE id = @EmployeeId", connection, transaction);
                        checkEmployeeCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                        var employeeExists = checkEmployeeCommand.ExecuteScalar() != null;

                        if (!employeeExists)
                        {
                            throw new Exception("Employee ID không tồn tại.");
                        }

                        // Tạo stored procedure cho cập nhật số dư
                        var command = new SqlCommand("sp_Deposit", connection, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        command.ExecuteNonQuery();

                        transaction.Commit();
                        
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Giao dịch không thành công: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public bool Withdraw(int accountId, float amount, string employeeId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        
                        var checkEmployeeCommand = new SqlCommand("SELECT 1 FROM Employee WHERE id = @EmployeeId", connection, transaction);
                        checkEmployeeCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                        var employeeExists = checkEmployeeCommand.ExecuteScalar() != null;

                        if (!employeeExists)
                        {
                            throw new Exception("Employee ID không tồn tại.");
                        }

                        var command = new SqlCommand("sp_Withdraw", connection, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        command.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Giao dịch không thành công: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        public bool Transfer(int fromAccountId, int toAccountId, float amount, string employeeId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        
                        var checkEmployeeCommand = new SqlCommand("SELECT 1 FROM Employee WHERE id = @EmployeeId", connection, transaction);
                        checkEmployeeCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                        var employeeExists = checkEmployeeCommand.ExecuteScalar() != null;

                        if (!employeeExists)
                        {
                            throw new Exception("Employee ID không tồn tại.");
                        }

                        var command = new SqlCommand("sp_Transfer", connection, transaction)
                        {
                            CommandType = CommandType.StoredProcedure
                        };
                        command.Parameters.AddWithValue("@FromAccountId", fromAccountId);
                        command.Parameters.AddWithValue("@ToAccountId", toAccountId);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        command.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Giao dịch không thành công: {ex.Message}");
                        return false;
                    }
                }
            }
        }
        public float GetAccountBalance(int accountId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT balance FROM Account WHERE id = @AccountId", connection);
                command.Parameters.AddWithValue("@AccountId", accountId);

                object result = command.ExecuteScalar();
                return result != null ? Convert.ToSingle(result) : 0; // Return 0 if no result
            }
        }


        public List<TransactionModel> GetTransactionsByAccountId(int accountId)
        {
            List<TransactionModel> transactions = new List<TransactionModel>();
            string query = "SELECT * FROM [Transaction] WHERE from_account_id = @AccountId OR to_account_id = @AccountId";

            var parameters = new SqlParameter[]
            {
        new SqlParameter("@AccountId", accountId)
            };

            DataTable result = dbHelper.ExecuteQuery(query, parameters);

            foreach (DataRow row in result.Rows)
            {
                var transaction = new TransactionModel
                {
                    id = row["id"] != DBNull.Value ? Convert.ToInt32(row["id"]) : 0, // Thay thế giá trị không hợp lệ
                    from_account_id = row["from_account_id"] != DBNull.Value ? Convert.ToInt32(row["from_account_id"]) : 0,
                    to_account_id = row["to_account_id"] != DBNull.Value ? Convert.ToInt32(row["to_account_id"]) : 0,
                    date_of_trans = row["date_of_trans"] != DBNull.Value ? Convert.ToDateTime(row["date_of_trans"]) : DateTime.MinValue,
                    amount = row["amount"] != DBNull.Value ? Convert.ToSingle(row["amount"]) : 0.0f
                };
                transactions.Add(transaction);
            }

            return transactions;
        }



       
        public bool Update(IModel model)
        {
            return true;
        }

        public bool Delete(IModel id)
        {
            return true;
        }

        public IModel Read(IModel id)
        {
            return null;
        }

        public bool Load()
        {
            return true;
        }

        public bool IsExist(string id)
        {
            return true;
        }
    }
}
