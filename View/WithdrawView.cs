using Bank.Controller;
using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bank.View
{
    public partial class WithdrawView : Form, IView
    {
        private TransactionController transactionController;
        private AccountController accountController;
        private AccountModel selectedAccount;

        public WithdrawView()
        {
            InitializeComponent();
            accountController = new AccountController();
            transactionController = new TransactionController();
            LoadAccounts(); 
        }

        void IView.GetDataFromText()
        {
            if (selectedAccount != null)
            {
                if (float.TryParse(txtAmount.Text, out float amount) && amount > 0)
                {
                    bool success = transactionController.Withdraw(selectedAccount.id, amount, "E01");
                    if (success)
                    {
                        MessageBox.Show("Rút tiền thành công!", "Thông báo");
                        
                        selectedAccount.balance = transactionController.GetAccountBalance(selectedAccount.id);

                        // Cập nhật số dư hiển thị
                        UpdateBalanceDisplay();
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Rút tiền thất bại.", "Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Số tiền không hợp lệ.", "Lỗi");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tài khoản.", "Lỗi");
            }
        }
        private void ClearInputFields()
        {
            txtAmount.Text = string.Empty;  // Xóa số tiền đã nhập
        }

        private void UpdateBalanceDisplay()
        {
            if (selectedAccount != null)
            {
                txtBalance.Text = selectedAccount.balance.ToString("N0");
            }
        }
        void IView.SetDataToText()
        {
            if (cmbAccount.SelectedItem != null)
            {
                selectedAccount = (AccountModel)cmbAccount.SelectedItem;
                txtBalance.Text = selectedAccount.balance.ToString("N0"); // Hiển thị số dư hiện tại
            }
        }

        private void LoadAccounts()
        {
            var accounts = accountController.Items.OfType<AccountModel>().ToList();
            cmbAccount.DataSource = accounts;
            cmbAccount.DisplayMember = "id"; 
        }

        private void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((IView)this).SetDataToText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((IView)this).GetDataFromText();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WithdrawView_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }
    }
}
