using Bank.Controller;
using Bank.Model;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Bank.View
{
    public partial class TransferView : Form, IView
    {
        private TransactionController transactionController;
        private AccountController accountController;
        private AccountModel fromAccount;
        private AccountModel toAccount;

        public TransferView()
        {
            InitializeComponent();
            accountController = new AccountController();
            transactionController = new TransactionController();
            LoadAccounts(); // Tải danh sách tài khoản
        }

        // Giao diện nhập dữ liệu
        void IView.GetDataFromText()
        {
            if (fromAccount != null && toAccount != null && fromAccount.id != toAccount.id)
            {
                if (float.TryParse(txtAmount.Text, out float amount) && amount > 0)
                {
                    bool success = transactionController.Transfer(fromAccount.id, toAccount.id, amount, "E01");
                    if (success)
                    {
                        MessageBox.Show("Chuyển khoản thành công!", "Thông báo");
                        
                        fromAccount.balance = transactionController.GetAccountBalance(fromAccount.id);

                        // Cập nhật số dư hiển thị
                        UpdateBalanceDisplay();
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Chuyển khoản thất bại.", "Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Số tiền không hợp lệ.", "Lỗi");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản hợp lệ.", "Lỗi");
            }
        }
        private void UpdateBalanceDisplay()
        {
            if (fromAccount != null)
            {
                txtBalance.Text = fromAccount.balance.ToString("N0"); 
            }
        }
        // Cập nhật dữ liệu từ tài khoản
        void IView.SetDataToText()
        {
            if (cmbFromAccount.SelectedItem != null)
            {
                fromAccount = (AccountModel)cmbFromAccount.SelectedItem;
                txtBalance.Text = fromAccount.balance.ToString("N0");
            }

            if (cmbToAccount.SelectedItem != null)
            {
                toAccount = (AccountModel)cmbToAccount.SelectedItem;
            }
        }

        
        private void LoadAccounts()
        {
            var accounts = accountController.Items.OfType<AccountModel>().ToList();
            cmbFromAccount.DataSource = accounts.ToList(); 
            cmbFromAccount.DisplayMember = "id"; 

            cmbToAccount.DataSource = accounts.ToList(); 
            cmbToAccount.DisplayMember = "id"; 
        }

        // Cập nhật danh sách tài khoản cho ToAccount
        private void UpdateToAccountList()
        {
            if (fromAccount != null)
            {
                var availableAccounts = accountController.Items
                    .OfType<AccountModel>()
                    .Where(account => account.id != fromAccount.id)
                    .ToList();

                cmbToAccount.DataSource = availableAccounts; 
                cmbToAccount.DisplayMember = "id";
            }
        }

        // Xử lý sự kiện khi người dùng chọn tài khoản nguồn
        private void cmbFromAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((IView)this).SetDataToText();
            UpdateToAccountList(); 
        }

        // Xử lý sự kiện khi người dùng chọn tài khoản đích
        private void cmbToAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((IView)this).SetDataToText();
        }

        // Sự kiện khi bấm nút chuyển khoản
        private void btnSave_Click(object sender, EventArgs e)
        {
            ((IView)this).GetDataFromText();
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void ClearInputFields()
        {
            txtAmount.Text = string.Empty;
        }
    }
}
