using Bank.Controller;
using Bank.Model;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bank.View
{
    public partial class EmployeeView : Form, IView
    {
        private EmployeeController controller;
        private EmployeeModel model;

        public EmployeeView()
        {
            InitializeComponent();
            controller = new EmployeeController();
            model = new EmployeeModel();
            controller.Load(); // Tải danh sách nhân viên
            LoadEmployees(); // Cập nhật DataGridView
            dataGridView.CellClick += dataGridView1_CellContentClick; // Sự kiện click
        }

        void IView.GetDataFromText()
        {
            model.id = txtID.Text.Trim();
            model.name = txtName.Text.Trim();

            // Kiểm tra đầu vào cho mật khẩu
            if (int.TryParse(txtPassword.Text, out int password))
            {
                model.password = password;
            }
            else
            {
                MessageBox.Show("Mật khẩu phải là số.");
                model.password = 0; // Đặt mật khẩu về 0 nếu không hợp lệ
            }

            model.role = rbtnAdmin.Checked ? "Admin" : "User";
        }

        void IView.SetDataToText()
        {
            txtID.Text = model.id;
            txtName.Text = model.name;
            txtPassword.Text = model.password.ToString();
            rbtnAdmin.Checked = model.role == "Admin";
            rbtnUser.Checked = model.role == "User";
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
                // ID đã tồn tại, tiến hành cập nhật
                if (controller.Update(model)) // Cập nhật nhân viên trong controller
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
                // Tạo mới nhân viên
                if (controller.Create(model)) // Tạo mới nhân viên trong controller
                {
                    MessageBox.Show("Tạo mới thành công!");
                }
                else
                {
                    MessageBox.Show("Tạo mới không thành công.");
                }
            }

            // Nạp lại danh sách nhân viên từ cơ sở dữ liệu
            controller.Load();
            LoadEmployees(); // Cập nhật lại DataGridView

            // Xóa trắng các trường dữ liệu
            ClearInputFields();
        }



        private void LoadEmployees()
        {
            var employees = controller.Items.OfType<EmployeeModel>().ToList();

            if (employees.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu nhân viên để hiển thị.");
                dataGridView.DataSource = null; 
                return;
            }

            dataGridView.DataSource = employees.Select(e => new
            {
                ID = e.id,
                Name = e.name,
                Password = e.password,
                Role = e.role
            }).ToList();
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10);
        }

        private void ClearInputFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtPassword.Clear();
            rbtnUser.Checked = false;
            rbtnAdmin.Checked = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string id = dataGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                    var employee = new EmployeeModel { id = id };

                    if (controller.Delete(employee))
                    {
                        MessageBox.Show("Xóa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công.");
                    }

                    // Nạp lại danh sách nhân viên từ cơ sở dữ liệu
                    controller.Load();
                    LoadEmployees(); // Cập nhật lại DataGridView
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
                txtPassword.Text = selectedRow.Cells["Password"].Value.ToString();
                var role = selectedRow.Cells["Role"].Value.ToString();
                rbtnUser.Checked = role == "User";
                rbtnAdmin.Checked = role == "Admin";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void EmployeeView_Load(object sender, EventArgs e)
        {
            LoadEmployees(); 
        }
    }
}
