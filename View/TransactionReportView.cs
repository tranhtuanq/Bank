using Bank.Controller;
using Bank.Model;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Bank.View
{
    public partial class TransactionReportView : Form
    {
        private AccountController accountController;
        private TransactionController transactionController; 
        private AccountModel selectedAccount;
        private PrintDocument printDocument;

        public TransactionReportView()
        {
            InitializeComponent();
            accountController = new AccountController();
            transactionController = new TransactionController();
            LoadAccounts();
            // Khởi tạo đối tượng PrintDocument và thêm sự kiện PrintPage
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
        }

        
        private void LoadAccounts()
        {
            var accounts = accountController.Items.OfType<AccountModel>().ToList();
            cmbAccount.DataSource = accounts;
            cmbAccount.DisplayMember = "id"; 
        }

        // Xử lý sự kiện khi chọn tài khoản trong ComboBox
        private void cmbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAccount.SelectedItem != null)
            {
                selectedAccount = (AccountModel)cmbAccount.SelectedItem; 
                LoadTransactions(selectedAccount.id); // Tải giao dịch cho tài khoản đã chọn
            }
        }

        // Tải giao dịch theo ID tài khoản và hiển thị trong DataGridView
        private void LoadTransactions(int accountId)
        {
            var transactions = transactionController.GetTransactionsByAccountId(accountId);
            dataGridView.DataSource = transactions.Select(t => new
            {
                t.id,
                t.from_account_id,
                t.to_account_id,
                t.date_of_trans,
                t.amount
            }).ToList();
        }

        // Xử lý sự kiện nút Print (in báo cáo)
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại PrintPreviewDialog trước khi in
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.ShowDialog();
        }

        // Xử lý sự kiện nút Close (đóng cửa sổ)
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Định dạng in cho tiêu đề và nội dung
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font contentFont = new Font("Arial", 12);
            int y = 100; // Vị trí y bắt đầu in

            // In tiêu đề
            e.Graphics.DrawString("Transaction Report", headerFont, Brushes.Black, 100, y);
            y += 40; // Di chuyển xuống dưới sau khi in tiêu đề

            // Tính toán độ rộng tối đa cho mỗi cột
            int[] columnWidths = new int[dataGridView.Columns.Count];
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                columnWidths[i] = (int)e.Graphics.MeasureString(dataGridView.Columns[i].HeaderText, contentFont).Width + 20; // Thêm một chút khoảng cách
            }

            // Tính toán vị trí x cho mỗi cột
            int[] columnXPositions = new int[dataGridView.Columns.Count];
            columnXPositions[0] = 100; // Bắt đầu từ vị trí x 100
            for (int i = 1; i < dataGridView.Columns.Count; i++)
            {
                columnXPositions[i] = columnXPositions[i - 1] + columnWidths[i - 1];
            }

            // In các tiêu đề cột
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                e.Graphics.DrawString(dataGridView.Columns[i].HeaderText, contentFont, Brushes.Black, columnXPositions[i], y);
            }
            y += 30;

            // In từng hàng từ DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null) // Kiểm tra hàng có giá trị
                {
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        string cellValue = row.Cells[i].Value.ToString();

                        // Chỉ định riêng cho cột date_of_trans
                        if (dataGridView.Columns[i].Name == "date_of_trans")
                        {
                            // Chuyển đổi giá trị sang định dạng ngày
                            DateTime dateValue;
                            if (DateTime.TryParse(cellValue, out dateValue))
                            {
                                cellValue = dateValue.ToString("dd/MM/yyyy"); 
                            }
                        }

                        e.Graphics.DrawString(cellValue, contentFont, Brushes.Black, columnXPositions[i], y);
                    }
                    y += 30;

                    // Kiểm tra nếu vị trí in vượt quá kích thước trang thì tiếp tục in trang tiếp theo
                    if (y > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        y = 100;
                        return;
                    }
                }
            }

            e.HasMorePages = false;
        }




    }
}
