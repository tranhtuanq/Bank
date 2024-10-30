using Bank.Controller;
using Bank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bank.View
{
    public partial class DepositView : Form, IView
    {
        private TransactionController transactionController;
        private AccountController accountController;
        private AccountModel selectedAccount;

        public DepositView()
        {
            InitializeComponent();
            accountController = new AccountController();
            transactionController = new TransactionController();
            LoadAccounts(); // Tải danh sách tài khoản
        }

        void IView.GetDataFromText()
        {
            if (selectedAccount != null)
            {
                if (float.TryParse(txtAmount.Text, out float amount) && amount > 0)
                {
                    bool success = transactionController.Deposit(selectedAccount.id, amount, "E01");
                    if (success)
                    {
                        MessageBox.Show("Gửi tiền thành công!", "Thông báo");

                        // Update the balance from the controller after the transaction
                        selectedAccount.balance = transactionController.GetAccountBalance(selectedAccount.id); 

                        // Cập nhật số dư hiển thị
                        UpdateBalanceDisplay();
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Gửi tiền thất bại.", "Lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Số tiền không hợp lệ. Vui lòng nhập một số dương.", "Lỗi");
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

        void IView.SetDataToText()
        {
            if (cmbAccount.SelectedItem != null)
            {
                selectedAccount = (AccountModel)cmbAccount.SelectedItem;
                txtBalance.Text = selectedAccount.balance.ToString("N0"); // Hiển thị số dư (định dạng tiền tệ)
            }
        }

        private void LoadAccounts()
        {
            var accounts = accountController.Items.OfType<AccountModel>().ToList();
            cmbAccount.DataSource = accounts;
            cmbAccount.DisplayMember = "id"; // Hiển thị ID tài khoản
        }

        private void UpdateBalanceDisplay()
        {
            if (selectedAccount != null)
            {
                txtBalance.Text = selectedAccount.balance.ToString("N0"); // Cập nhật số dư
            }
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

        private void DepositView_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }
    }
}
