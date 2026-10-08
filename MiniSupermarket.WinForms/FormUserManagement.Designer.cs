using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer? components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpUsers = new GroupBox();
            dgvUsers = new DataGridView();

            grpInfo = new GroupBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblRole = new Label();
            cboRole = new ComboBox();
            lblSelectedUser = new Label();

            btnAddUser = new Button();
            btnResetPassword = new Button();
            btnToggleLock = new Button();
            btnLoad = new Button();

            grpUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            grpInfo.SuspendLayout();
            SuspendLayout();

            //
            // grpUsers
            //
            grpUsers.Controls.Add(dgvUsers);
            grpUsers.Dock = DockStyle.Fill;
            grpUsers.Location = new Point(16, 16);
            grpUsers.Name = "grpUsers";
            grpUsers.Padding = new Padding(12);
            grpUsers.Size = new Size(648, 568);
            grpUsers.TabIndex = 0;
            grpUsers.TabStop = false;
            grpUsers.Text = "Danh sách tài khoản";

            //
            // dgvUsers
            //
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.AllowUserToResizeRows = false;
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.TabIndex = 0;
            dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;

            //
            // grpInfo
            //
            grpInfo.Controls.Add(lblUsername);
            grpInfo.Controls.Add(txtUsername);
            grpInfo.Controls.Add(lblFullName);
            grpInfo.Controls.Add(txtFullName);
            grpInfo.Controls.Add(lblPassword);
            grpInfo.Controls.Add(txtPassword);
            grpInfo.Controls.Add(lblRole);
            grpInfo.Controls.Add(cboRole);
            grpInfo.Controls.Add(lblSelectedUser);
            grpInfo.Controls.Add(btnAddUser);
            grpInfo.Controls.Add(btnResetPassword);
            grpInfo.Controls.Add(btnToggleLock);
            grpInfo.Controls.Add(btnLoad);
            grpInfo.Dock = DockStyle.Right;
            grpInfo.Location = new Point(664, 16);
            grpInfo.Name = "grpInfo";
            grpInfo.Size = new Size(320, 568);
            grpInfo.TabIndex = 1;
            grpInfo.TabStop = false;
            grpInfo.Text = "Thông tin và thao tác";

            //
            // lblUsername
            //
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(20, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Text = "Tên đăng nhập";

            //
            // txtUsername
            //
            txtUsername.Location = new Point(20, 52);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(280, 23);
            txtUsername.TabIndex = 0;

            //
            // lblFullName
            //
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 90);
            lblFullName.Name = "lblFullName";
            lblFullName.Text = "Họ và tên";

            //
            // txtFullName
            //
            txtFullName.Location = new Point(20, 112);
            txtFullName.MaxLength = 150;
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(280, 23);
            txtFullName.TabIndex = 1;

            //
            // lblPassword
            //
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(20, 150);
            lblPassword.Name = "lblPassword";
            lblPassword.Text = "Mật khẩu tạo mới / đặt lại";

            //
            // txtPassword
            //
            txtPassword.Location = new Point(20, 172);
            txtPassword.MaxLength = 200;
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(280, 23);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;

            //
            // lblRole
            //
            lblRole.AutoSize = true;
            lblRole.Location = new Point(20, 210);
            lblRole.Name = "lblRole";
            lblRole.Text = "Vai trò khi tạo tài khoản";

            //
            // cboRole
            //
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.Items.AddRange(new object[]
            {
                "Admin",
                "Cashier",
                "Warehouse"
            });
            cboRole.Location = new Point(20, 232);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(280, 23);
            cboRole.TabIndex = 3;

            //
            // lblSelectedUser
            //
            lblSelectedUser.ForeColor = Color.FromArgb(75, 85, 99);
            lblSelectedUser.Location = new Point(20, 275);
            lblSelectedUser.Name = "lblSelectedUser";
            lblSelectedUser.Size = new Size(280, 45);
            lblSelectedUser.Text = "Chưa chọn tài khoản.";

            //
            // btnAddUser
            //
            btnAddUser.BackColor = Color.FromArgb(41, 100, 180);
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.ForeColor = Color.White;
            btnAddUser.Location = new Point(20, 330);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(280, 36);
            btnAddUser.TabIndex = 4;
            btnAddUser.Text = "Tạo tài khoản mới";
            btnAddUser.UseVisualStyleBackColor = false;
            btnAddUser.Click += btnAddUser_Click;

            //
            // btnResetPassword
            //
            btnResetPassword.Location = new Point(20, 376);
            btnResetPassword.Name = "btnResetPassword";
            btnResetPassword.Size = new Size(280, 36);
            btnResetPassword.TabIndex = 5;
            btnResetPassword.Text = "Đặt lại mật khẩu";
            btnResetPassword.UseVisualStyleBackColor = true;
            btnResetPassword.Click += btnResetPassword_Click;

            //
            // btnToggleLock
            //
            btnToggleLock.Location = new Point(20, 422);
            btnToggleLock.Name = "btnToggleLock";
            btnToggleLock.Size = new Size(280, 36);
            btnToggleLock.TabIndex = 6;
            btnToggleLock.Text = "Khóa / Mở khóa tài khoản";
            btnToggleLock.UseVisualStyleBackColor = true;
            btnToggleLock.Click += btnToggleLock_Click;

            //
            // btnLoad
            //
            btnLoad.Location = new Point(20, 468);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(280, 36);
            btnLoad.TabIndex = 7;
            btnLoad.Text = "Tải lại / Xóa ô nhập";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            //
            // FormUserManagement
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1000, 600);
            Controls.Add(grpUsers);
            Controls.Add(grpInfo);
            Name = "FormUserManagement";
            Padding = new Padding(16);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản trị tài khoản";
            Load += FormUserManagement_Load;

            grpUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpUsers = null!;
        private DataGridView dgvUsers = null!;

        private GroupBox grpInfo = null!;
        private Label lblUsername = null!;
        private TextBox txtUsername = null!;
        private Label lblFullName = null!;
        private TextBox txtFullName = null!;
        private Label lblPassword = null!;
        private TextBox txtPassword = null!;
        private Label lblRole = null!;
        private ComboBox cboRole = null!;
        private Label lblSelectedUser = null!;

        private Button btnAddUser = null!;
        private Button btnResetPassword = null!;
        private Button btnToggleLock = null!;
        private Button btnLoad = null!;
    }
}