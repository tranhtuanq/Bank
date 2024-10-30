using Bank.Controller;
using Bank.Model;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Bank.View
{
    public partial class BranchView : Form, IView
    {
        private BranchController controller;
        private BranchModel model;

        public BranchView()
        {
            InitializeComponent();
            controller = new BranchController();
            model = new BranchModel();
            controller.Load(); 
            LoadBranches(); 
            dataGridView.CellClick += dataGridView1_CellContentClick;
            
        }

        void IView.GetDataFromText()
        {
            model.id = txtID.Text.Trim();
            model.name = txtName.Text.Trim();
            model.city = txtCity.Text.Trim(); 
        }

        void IView.SetDataToText()
        {
            txtID.Text = model.id;
            txtName.Text = model.name;
            txtCity.Text = model.city; 
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

            controller.Load();
            LoadBranches(); 

            
            ClearInputFields();
        }

        private void LoadBranches()
        {
            var branches = controller.Items.OfType<BranchModel>().ToList();

            if (branches.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu chi nhánh để hiển thị.");
                dataGridView.DataSource = null; 
                return;
            }

            dataGridView.DataSource = branches.Select(b => new
            {
                ID = b.id,
                Name = b.name,
                City = b.city 
            }).ToList();
            
        }

        private void ClearInputFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtCity.Clear(); 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string id = dataGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                    var branch = new BranchModel { id = id };

                    if (controller.Delete(branch))
                    {
                        MessageBox.Show("Xóa thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công.");
                    }

                    // Nạp lại danh sách chi nhánh từ cơ sở dữ liệu
                    controller.Load();
                    LoadBranches(); // Cập nhật lại DataGridView
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
                txtCity.Text = selectedRow.Cells["City"].Value.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form
        }

        private void BranchView_Load(object sender, EventArgs e)
        {
            LoadBranches(); 
        }
    }
}
