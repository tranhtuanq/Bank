using Bank.Controller;
using Bank.Model;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bank.View
{
    public partial class CustomerView : Form, IView
    {
        private CustomerController controller;
        private CustomerModel model;

        public CustomerView()
        {
            InitializeComponent();
            controller = new CustomerController();
            model = new CustomerModel();
            controller.Load(); // Tải danh sách khách hàng
            LoadCustomers(); // Cập nhật DataGridView
            dataGridView.CellClick += dataGridView1_CellContentClick; 
        }

        void IView.GetDataFromText()
        {
            model.id = txtID.Text.Trim();
            model.name = txtName.Text.Trim();
            model.phone = txtPhone.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.city = txtCity.Text.Trim();
            model.pin = txtPIN.Text.Trim();
        }

        void IView.SetDataToText()
        {
            txtID.Text = model.id;
            txtName.Text = model.name;
            txtPhone.Text = model.phone;
            txtEmail.Text = model.email;
            txtCity.Text = model.city;
            txtPIN.Text = model.pin;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((IView)this).GetDataFromText();

            if (string.IsNullOrEmpty(model.id))
            {
                MessageBox.Show("ID không thể để trống.");
                return;
            }

            // Kiểm tra xem ID có tồn tại trong danh sách không
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
            else // Nếu ID không tồn tại, tạo mới
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

            // Nạp lại danh sách khách hàng từ cơ sở dữ liệu
            controller.Load();
            LoadCustomers(); // Cập nhật lại DataGridView
            ClearInputFields();
        }

        private void LoadCustomers()
        {
            var customers = controller.Items.OfType<CustomerModel>().ToList();

            if (customers.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu khách hàng để hiển thị.");
                dataGridView.DataSource = null; 
                return;
            }

            dataGridView.DataSource = customers.Select(c => new
            {
                ID = c.id,
                Name = c.name,
                Phone = c.phone,
                Email = c.email,
                City = c.city,
                PIN = c.pin
            }).ToList();
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10);
        }

        private void ClearInputFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtCity.Clear();
            txtPIN.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string id = dataGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                    var customer = new CustomerModel { id = id };

                    if (controller.Delete(customer))
                    {
                        MessageBox.Show("Xóa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công.");
                    }

                    // Nạp lại danh sách khách hàng từ cơ sở dữ liệu
                    controller.Load();
                    LoadCustomers(); // Cập nhật lại DataGridView
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
                txtName.Text = selectedRow.Cells["Name"].Value.ToString();
                txtPhone.Text = selectedRow.Cells["Phone"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                txtCity.Text = selectedRow.Cells["City"].Value.ToString();
                txtPIN.Text = selectedRow.Cells["PIN"].Value.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form
        }

        private void CustomerView_Load(object sender, EventArgs e)
        {
            LoadCustomers(); // Tải dữ liệu khi form được mở
        }
    }
}
