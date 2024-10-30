using Bank.Model; 
using Bank.Utils;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient; 

namespace Bank.View
{
    public partial class LoginView : Form
    {
        private DatabaseHelper dbHelper; 

        public LoginView()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["BankManagementDB"].ConnectionString; ; 
            dbHelper = new DatabaseHelper(connectionString); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userID = txtUserID.Text;
            string pin = txtPIN.Text;

            // Kiểm tra thông tin đăng nhập từ CSDL Employee
            string queryEmployee = "SELECT role FROM Employee WHERE id = @UserID AND password = @Password";
            var parametersEmployee = new SqlParameter[]
            {
        new SqlParameter("@UserID", userID),
        new SqlParameter("@Password", pin)
            };

            DataTable resultEmployee = dbHelper.ExecuteQuery(queryEmployee, parametersEmployee);
            if (resultEmployee.Rows.Count > 0)
            {
                // Lấy vai trò người dùng từ kết quả truy vấn Employee
                string role = resultEmployee.Rows[0]["role"].ToString();

                // Chỉ cho phép đăng nhập với vai trò là "User" hoặc "Admin"
                if (role == "User" || role == "Admin")
                {
                    MessageBox.Show("Đăng nhập thành công với vai trò: " + role, "Thông báo");
                    this.DialogResult = DialogResult.OK; // Đặt kết quả dialog thành OK
                    this.Tag = role; // Lưu vai trò để truy cập từ MainWindows
                    this.Close(); 
                }
            }
            else
            {
                MessageBox.Show("Sai UserID hoặc PIN. Vui lòng thử lại.", "Lỗi");
            }
        }



        private void Login_Load(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            txtUserID.TextChanged += new EventHandler(txtID_TextChanged);
            txtPIN.TextChanged += new EventHandler(txtPIN_TextChanged);
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtUserID.Text) && !string.IsNullOrWhiteSpace(txtPIN.Text);
        }

        private void txtPIN_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtUserID.Text) && !string.IsNullOrWhiteSpace(txtPIN.Text);
        }

        private void txtPIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public string UserRole => this.Tag as string; // Truy cập vai trò người dùng
    }
}
