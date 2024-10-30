using Bank.View;
using System;
using System.Windows.Forms;

namespace Bank
{
    public partial class MainWindows : Form
    {
        private bool isLoggedIn = false; // Trạng thái đăng nhập
        private string userRole; // Vai trò của người dùng

        public MainWindows()
        {
            InitializeComponent();
            UpdateMenuItems(); // Cập nhật menu ngay từ đầu
        }

        private void UpdateMenuItems()
        {
            // Vô hiệu hóa các mục menu dựa trên trạng thái đăng nhập
            menu_file_logout.Enabled = isLoggedIn; 
            menu_system_employee.Enabled = isLoggedIn && userRole == "Admin";
            menu_system_account.Enabled = isLoggedIn && (userRole == "Admin" || userRole == "User"); 
            menu_system_branch.Enabled = isLoggedIn && userRole == "Admin"; 
            menu_system_customer.Enabled = isLoggedIn && (userRole == "Admin" || userRole == "User"); 
            menu_service_deposit.Enabled = isLoggedIn && (userRole == "Admin" || userRole == "User");
            menu_service_withdraw.Enabled = isLoggedIn && (userRole == "Admin" || userRole == "User") ;
            menu_service_transfer.Enabled = isLoggedIn && (userRole == "Admin" || userRole == "User");
        }


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Có thể xử lý sự kiện nhấp chuột vào menu nếu cần
        }

        private void menu_file_login_click(object sender, EventArgs e)
        {
            LoginView loginView = new LoginView();
            if (loginView.ShowDialog() == DialogResult.OK) // Kiểm tra nếu đăng nhập thành công
            {
                isLoggedIn = true; // Cập nhật trạng thái đăng nhập
                userRole = loginView.UserRole; // Lấy vai trò người dùng từ LoginView
                UpdateMenuItems(); // Cập nhật menu dựa trên trạng thái đăng nhập và vai trò
            }
        }

        
        private void menu_file_logout_click(object sender, EventArgs e)
        {
            isLoggedIn = false; // Đặt lại trạng thái đăng nhập
            userRole = null; // Xóa vai trò người dùng
            UpdateMenuItems(); // Cập nhật menu để vô hiệu hóa các tùy chọn
            MessageBox.Show("Bạn đã đăng xuất thành công."); // Thông báo đăng xuất thành công
        }


        private void menu_system_employee_click(object sender, EventArgs e)
        {
            if (isLoggedIn && userRole == "Admin") // Kiểm tra quyền truy cập
            {
                EmployeeView employeeView = new EmployeeView();
                employeeView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào tính năng này.");
            }
        }

        private void menu_system_account_click(object sender, EventArgs e)
        {
            if (isLoggedIn ) // Kiểm tra quyền truy cập
            {
                AccountView accountView = new AccountView();
                accountView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào tính năng này.");
            }
        }

        private void menu_system_branch_click(object sender, EventArgs e)
        {
            if (isLoggedIn && userRole == "Admin") // Kiểm tra quyền truy cập
            {
                BranchView branchView = new BranchView();
                branchView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào tính năng này.");
            }
        }

        private void menu_system_customer_click(object sender, EventArgs e)
        {
            if (isLoggedIn ) // Kiểm tra quyền truy cập
            {
                CustomerView customerView = new CustomerView();
                customerView.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền truy cập vào tính năng này.");
            }
        }

        private void menu_service_deposit_click(object sender, EventArgs e)
        {
            DepositView depositView = new DepositView();
            depositView.ShowDialog();
        }


        private void menu_service_withdraw_click(object sender, EventArgs e)
        {
            WithdrawView withdrawView = new WithdrawView();
            withdrawView.ShowDialog();
        }

        private void menu_service_transfer_click(object sender, EventArgs e)
        {
          TransferView transferView = new TransferView();
            transferView.ShowDialog();
        }

        private void menu_report_transactionreport_Click(object sender, EventArgs e)
        {
            TransactionReportView transactionReportView = new TransactionReportView();
            transactionReportView.ShowDialog();
        }
    }
}
