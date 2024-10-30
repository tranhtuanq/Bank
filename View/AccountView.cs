using Bank.Controller;
using Bank.Model;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bank.View
{
    public partial class AccountView : Form, IView
    {
        private AccountController controller;
        private AccountModel model;

        public AccountView()
        {
            InitializeComponent();
            controller = new AccountController();
            model = new AccountModel();
            controller.Load();
            LoadAccounts();
            dataGridView.CellClick += dataGridView1_CellContentClick;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10);
        }

        void IView.GetDataFromText()
        {
            if (int.TryParse(txtID.Text.Trim(), out int id))
            {
                model.id = id;
            }
            else
            {
                MessageBox.Show("ID phải là số nguyên.");
                model.id = 0;
            }

            model.customerid = txtCustomerId.Text.Trim();
            model.date_opened = dateTimePicker.Value;
            if (float.TryParse(txtBalance.Text.Trim(), out float balance))
            {
                model.balance = balance;
            }
            else
            {
                MessageBox.Show("Số dư phải là số.");
                model.balance = 0;
            }
        }

        void IView.SetDataToText()
        {
            txtID.Text = model.id.ToString();
            txtCustomerId.Text = model.customerid;
            dateTimePicker.Value = model.date_opened;
            txtBalance.Text = model.balance.ToString("N0");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((IView)this).GetDataFromText();

            if (model.id == 0)
            {
                MessageBox.Show("ID không thể để trống.");
                return;
            }

            if (controller.IsExist(model.id))
            {
                if (controller.Update(model))
                {
                    MessageBox.Show("Cập nhật thành công!");
                }
                else
                {
                    MessageBox.Show("Cập nhật không thành công.");
                }
            }
            else
            {
                if (controller.Create(model))
                {
                    MessageBox.Show("Tạo mới thành công!");
                }
                else
                {
                    MessageBox.Show("Tạo mới không thành công.");
                }
            }

            controller.Load();
            LoadAccounts();
            ClearInputFields();
        }

        private void LoadAccounts()
        {
            var accounts = controller.Items.OfType<AccountModel>().ToList();

            if (accounts.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu tài khoản để hiển thị.");
                dataGridView.DataSource = null; 
                return;
            }

            // Gán dữ liệu cho DataGridView
            dataGridView.DataSource = accounts.Select(a => new
            {
                ID = a.id,
                CustomerId = a.customerid,
                DateOpened = a.date_opened,
                Balance = a.balance 
            }).ToList();

            
            dataGridView.Columns["Balance"].DefaultCellStyle.Format = "N0";
            
        }


        private void ClearInputFields()
        {
            txtID.Clear();
            txtCustomerId.Clear();
            txtBalance.Clear();
            dateTimePicker.Value = DateTime.Now;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["ID"].Value);
                    var account = new AccountModel { id = id };

                    if (controller.Delete(account))
                    {
                        MessageBox.Show("Xóa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công.");
                    }

                    controller.Load();
                    LoadAccounts();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridView.Rows[e.RowIndex];
                txtID.Text = selectedRow.Cells["ID"].Value.ToString();
                txtCustomerId.Text = selectedRow.Cells["CustomerId"].Value.ToString();
                dateTimePicker.Value = Convert.ToDateTime(selectedRow.Cells["DateOpened"].Value);

                // Kiểm tra và định dạng giá trị Balance
                if (selectedRow.Cells["Balance"].Value != null)
                {
                    // Chuyển đổi giá trị Balance sang float
                    if (float.TryParse(selectedRow.Cells["Balance"].Value.ToString(), out float balance))
                    {
                        // Định dạng với N0 (không có phần thập phân)
                        txtBalance.Text = balance.ToString("N0"); 
                    }
                    else
                    {
                        txtBalance.Text = "0"; 
                    }
                }
                else
                {
                    txtBalance.Text = "0";
                }
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AccountView_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }
    }
}
