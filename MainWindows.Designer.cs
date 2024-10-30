namespace Bank
{
    partial class MainWindows
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindows));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu_file = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_file_login = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_file_logout = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_system = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_system_employee = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_system_account = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_system_branch = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_system_customer = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_service_deposit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_service_withdraw = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_service_transfer = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_report_transactionreport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file,
            this.menu_system,
            this.serviceToolStripMenuItem,
            this.reportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1043, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // menu_file
            // 
            this.menu_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_file_login,
            this.menu_file_logout});
            this.menu_file.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_file.Name = "menu_file";
            this.menu_file.Size = new System.Drawing.Size(57, 29);
            this.menu_file.Text = "File";
            // 
            // menu_file_login
            // 
            this.menu_file_login.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_login.Image")));
            this.menu_file_login.Name = "menu_file_login";
            this.menu_file_login.Size = new System.Drawing.Size(158, 30);
            this.menu_file_login.Text = "Login";
            this.menu_file_login.Click += new System.EventHandler(this.menu_file_login_click);
            // 
            // menu_file_logout
            // 
            this.menu_file_logout.Image = ((System.Drawing.Image)(resources.GetObject("menu_file_logout.Image")));
            this.menu_file_logout.Name = "menu_file_logout";
            this.menu_file_logout.Size = new System.Drawing.Size(158, 30);
            this.menu_file_logout.Text = "Logout";
            this.menu_file_logout.Click += new System.EventHandler(this.menu_file_logout_click);
            // 
            // menu_system
            // 
            this.menu_system.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_system_employee,
            this.menu_system_account,
            this.menu_system_branch,
            this.menu_system_customer});
            this.menu_system.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_system.Name = "menu_system";
            this.menu_system.Size = new System.Drawing.Size(92, 29);
            this.menu_system.Text = "System";
            // 
            // menu_system_employee
            // 
            this.menu_system_employee.Image = ((System.Drawing.Image)(resources.GetObject("menu_system_employee.Image")));
            this.menu_system_employee.Name = "menu_system_employee";
            this.menu_system_employee.Size = new System.Drawing.Size(185, 30);
            this.menu_system_employee.Text = "Employee";
            this.menu_system_employee.Click += new System.EventHandler(this.menu_system_employee_click);
            // 
            // menu_system_account
            // 
            this.menu_system_account.Image = ((System.Drawing.Image)(resources.GetObject("menu_system_account.Image")));
            this.menu_system_account.Name = "menu_system_account";
            this.menu_system_account.Size = new System.Drawing.Size(185, 30);
            this.menu_system_account.Text = "Account";
            this.menu_system_account.Click += new System.EventHandler(this.menu_system_account_click);
            // 
            // menu_system_branch
            // 
            this.menu_system_branch.Image = ((System.Drawing.Image)(resources.GetObject("menu_system_branch.Image")));
            this.menu_system_branch.Name = "menu_system_branch";
            this.menu_system_branch.Size = new System.Drawing.Size(185, 30);
            this.menu_system_branch.Text = "Branch";
            this.menu_system_branch.Click += new System.EventHandler(this.menu_system_branch_click);
            // 
            // menu_system_customer
            // 
            this.menu_system_customer.Image = ((System.Drawing.Image)(resources.GetObject("menu_system_customer.Image")));
            this.menu_system_customer.Name = "menu_system_customer";
            this.menu_system_customer.Size = new System.Drawing.Size(185, 30);
            this.menu_system_customer.Text = "Customer";
            this.menu_system_customer.Click += new System.EventHandler(this.menu_system_customer_click);
            // 
            // serviceToolStripMenuItem
            // 
            this.serviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_service_deposit,
            this.menu_service_withdraw,
            this.menu_service_transfer});
            this.serviceToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serviceToolStripMenuItem.Name = "serviceToolStripMenuItem";
            this.serviceToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.serviceToolStripMenuItem.Text = "Service";
            // 
            // menu_service_deposit
            // 
            this.menu_service_deposit.Image = ((System.Drawing.Image)(resources.GetObject("menu_service_deposit.Image")));
            this.menu_service_deposit.Name = "menu_service_deposit";
            this.menu_service_deposit.Size = new System.Drawing.Size(180, 30);
            this.menu_service_deposit.Text = "Deposit";
            this.menu_service_deposit.Click += new System.EventHandler(this.menu_service_deposit_click);
            // 
            // menu_service_withdraw
            // 
            this.menu_service_withdraw.Image = ((System.Drawing.Image)(resources.GetObject("menu_service_withdraw.Image")));
            this.menu_service_withdraw.Name = "menu_service_withdraw";
            this.menu_service_withdraw.Size = new System.Drawing.Size(180, 30);
            this.menu_service_withdraw.Text = "Withdraw";
            this.menu_service_withdraw.Click += new System.EventHandler(this.menu_service_withdraw_click);
            // 
            // menu_service_transfer
            // 
            this.menu_service_transfer.Image = ((System.Drawing.Image)(resources.GetObject("menu_service_transfer.Image")));
            this.menu_service_transfer.Name = "menu_service_transfer";
            this.menu_service_transfer.Size = new System.Drawing.Size(180, 30);
            this.menu_service_transfer.Text = "Transfer";
            this.menu_service_transfer.Click += new System.EventHandler(this.menu_service_transfer_click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_report_transactionreport});
            this.reportToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(83, 29);
            this.reportToolStripMenuItem.Text = "Report";
            // 
            // menu_report_transactionreport
            // 
            this.menu_report_transactionreport.Image = ((System.Drawing.Image)(resources.GetObject("menu_report_transactionreport.Image")));
            this.menu_report_transactionreport.Name = "menu_report_transactionreport";
            this.menu_report_transactionreport.Size = new System.Drawing.Size(263, 30);
            this.menu_report_transactionreport.Text = "Transaction Report";
            this.menu_report_transactionreport.Click += new System.EventHandler(this.menu_report_transactionreport_Click);
            // 
            // MainWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1043, 649);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindows";
            this.Text = "MainWindows";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_file;
        private System.Windows.Forms.ToolStripMenuItem menu_file_login;
        private System.Windows.Forms.ToolStripMenuItem menu_file_logout;
        private System.Windows.Forms.ToolStripMenuItem menu_system;
        private System.Windows.Forms.ToolStripMenuItem menu_system_employee;
        private System.Windows.Forms.ToolStripMenuItem menu_system_account;
        private System.Windows.Forms.ToolStripMenuItem menu_system_branch;
        private System.Windows.Forms.ToolStripMenuItem menu_system_customer;
        private System.Windows.Forms.ToolStripMenuItem serviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_service_deposit;
        private System.Windows.Forms.ToolStripMenuItem menu_service_withdraw;
        private System.Windows.Forms.ToolStripMenuItem menu_service_transfer;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_report_transactionreport;
    }
}