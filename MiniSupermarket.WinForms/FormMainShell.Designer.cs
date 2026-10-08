using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
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
            panelSidebar = new Panel();
            lblLogo = new Label();

            btnPOS = new Button();
            btnCategory = new Button();
            btnProduct = new Button();
            btnCustomer = new Button();
            btnReports = new Button();
            btnUserManage = new Button();

            panelSidebarBottom = new Panel();
            btnLogout = new Button();

            panelWorkspace = new Panel();
            panelTopHeader = new Panel();
            lblTitle = new Label();
            lblUserInfo = new Label();

            panelMainContent = new Panel();

            panelSidebar.SuspendLayout();
            panelSidebarBottom.SuspendLayout();
            panelWorkspace.SuspendLayout();
            panelTopHeader.SuspendLayout();
            SuspendLayout();

            //
            // panelSidebar
            //
            panelSidebar.BackColor = Color.FromArgb(24, 30, 48);

            // Thứ tự thêm control bảo đảm các nút Dock.Top
            // hiển thị theo thứ tự từ POS đến Quản trị Tài khoản.
            panelSidebar.Controls.Add(btnUserManage);
            panelSidebar.Controls.Add(btnReports);
            panelSidebar.Controls.Add(btnCustomer);
            panelSidebar.Controls.Add(btnProduct);
            panelSidebar.Controls.Add(btnCategory);
            panelSidebar.Controls.Add(btnPOS);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Controls.Add(panelSidebarBottom);

            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(230, 681);
            panelSidebar.TabIndex = 0;

            //
            // lblLogo
            //
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font(
                "Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Padding = new Padding(14, 0, 0, 0);
            lblLogo.Size = new Size(230, 90);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "🛒 MiniMart POS";
            lblLogo.TextAlign = ContentAlignment.MiddleLeft;

            //
            // btnPOS
            //
            btnPOS.BackColor = Color.FromArgb(24, 30, 48);
            btnPOS.Cursor = Cursors.Hand;
            btnPOS.Dock = DockStyle.Top;
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(35, 44, 66);
            btnPOS.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(41, 100, 180);
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.Font = new Font("Segoe UI", 10F);
            btnPOS.ForeColor = Color.White;
            btnPOS.Location = new Point(0, 90);
            btnPOS.Name = "btnPOS";
            btnPOS.Padding = new Padding(14, 0, 0, 0);
            btnPOS.Size = new Size(230, 45);
            btnPOS.TabIndex = 1;
            btnPOS.Text = "🛒 Bán hàng (POS)";
            btnPOS.TextAlign = ContentAlignment.MiddleLeft;
            btnPOS.UseVisualStyleBackColor = false;
            btnPOS.Click += btnPOS_Click;

            //
            // btnCategory
            //
            btnCategory.BackColor = Color.FromArgb(24, 30, 48);
            btnCategory.Cursor = Cursors.Hand;
            btnCategory.Dock = DockStyle.Top;
            btnCategory.FlatAppearance.BorderSize = 0;
            btnCategory.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(35, 44, 66);
            btnCategory.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(41, 100, 180);
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.Font = new Font("Segoe UI", 10F);
            btnCategory.ForeColor = Color.White;
            btnCategory.Location = new Point(0, 135);
            btnCategory.Name = "btnCategory";
            btnCategory.Padding = new Padding(14, 0, 0, 0);
            btnCategory.Size = new Size(230, 45);
            btnCategory.TabIndex = 2;
            btnCategory.Text = "📁 Quản lý Danh mục";
            btnCategory.TextAlign = ContentAlignment.MiddleLeft;
            btnCategory.UseVisualStyleBackColor = false;
            btnCategory.Click += btnCategory_Click;

            //
            // btnProduct
            //
            btnProduct.BackColor = Color.FromArgb(24, 30, 48);
            btnProduct.Cursor = Cursors.Hand;
            btnProduct.Dock = DockStyle.Top;
            btnProduct.FlatAppearance.BorderSize = 0;
            btnProduct.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(35, 44, 66);
            btnProduct.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(41, 100, 180);
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.Font = new Font("Segoe UI", 10F);
            btnProduct.ForeColor = Color.White;
            btnProduct.Location = new Point(0, 180);
            btnProduct.Name = "btnProduct";
            btnProduct.Padding = new Padding(14, 0, 0, 0);
            btnProduct.Size = new Size(230, 45);
            btnProduct.TabIndex = 3;
            btnProduct.Text = "📦 Quản lý Sản phẩm";
            btnProduct.TextAlign = ContentAlignment.MiddleLeft;
            btnProduct.UseVisualStyleBackColor = false;

            //
            // btnCustomer
            //
            btnCustomer.BackColor = Color.FromArgb(24, 30, 48);
            btnCustomer.Cursor = Cursors.Hand;
            btnCustomer.Dock = DockStyle.Top;
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(35, 44, 66);
            btnCustomer.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(41, 100, 180);
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.Font = new Font("Segoe UI", 10F);
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Location = new Point(0, 225);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Padding = new Padding(14, 0, 0, 0);
            btnCustomer.Size = new Size(230, 45);
            btnCustomer.TabIndex = 4;
            btnCustomer.Text = "👥 Quản lý Khách hàng";
            btnCustomer.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomer.UseVisualStyleBackColor = false;
            btnCustomer.Click += btnCustomer_Click;

            //
            // btnReports
            //
            btnReports.BackColor = Color.FromArgb(24, 30, 48);
            btnReports.Cursor = Cursors.Hand;
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(35, 44, 66);
            btnReports.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(41, 100, 180);
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 10F);
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 270);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(14, 0, 0, 0);
            btnReports.Size = new Size(230, 45);
            btnReports.TabIndex = 5;
            btnReports.Text = "📊 Báo cáo Doanh thu";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = false;

            //
            // btnUserManage
            //
            btnUserManage.BackColor = Color.FromArgb(24, 30, 48);
            btnUserManage.Cursor = Cursors.Hand;
            btnUserManage.Dock = DockStyle.Top;
            btnUserManage.FlatAppearance.BorderSize = 0;
            btnUserManage.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(35, 44, 66);
            btnUserManage.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(41, 100, 180);
            btnUserManage.FlatStyle = FlatStyle.Flat;
            btnUserManage.Font = new Font("Segoe UI", 10F);
            btnUserManage.ForeColor = Color.White;
            btnUserManage.Location = new Point(0, 315);
            btnUserManage.Name = "btnUserManage";
            btnUserManage.Padding = new Padding(14, 0, 0, 0);
            btnUserManage.Size = new Size(230, 45);
            btnUserManage.TabIndex = 6;
            btnUserManage.Text = "🛡 Quản trị Tài khoản";
            btnUserManage.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManage.UseVisualStyleBackColor = false;

            //
            // panelSidebarBottom
            //
            panelSidebarBottom.BackColor = Color.FromArgb(19, 24, 39);
            panelSidebarBottom.Controls.Add(btnLogout);
            panelSidebarBottom.Dock = DockStyle.Bottom;
            panelSidebarBottom.Location = new Point(0, 621);
            panelSidebarBottom.Name = "panelSidebarBottom";
            panelSidebarBottom.Padding = new Padding(0, 8, 0, 7);
            panelSidebarBottom.Size = new Size(230, 60);
            panelSidebarBottom.TabIndex = 7;

            //
            // btnLogout
            //
            btnLogout.BackColor = Color.FromArgb(19, 24, 39);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(150, 45, 45);
            btnLogout.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(120, 35, 35);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 8);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(14, 0, 0, 0);
            btnLogout.Size = new Size(230, 45);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "🚪 Đăng xuất";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;

            //
            // panelWorkspace
            //
            panelWorkspace.Controls.Add(panelMainContent);
            panelWorkspace.Controls.Add(panelTopHeader);
            panelWorkspace.Dock = DockStyle.Fill;
            panelWorkspace.Location = new Point(230, 0);
            panelWorkspace.Name = "panelWorkspace";
            panelWorkspace.Size = new Size(1034, 681);
            panelWorkspace.TabIndex = 1;

            //
            // panelTopHeader
            //
            panelTopHeader.BackColor = Color.White;
            panelTopHeader.Controls.Add(lblTitle);
            panelTopHeader.Controls.Add(lblUserInfo);
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Location = new Point(0, 0);
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Size = new Size(1034, 60);
            panelTopHeader.TabIndex = 0;

            //
            // lblTitle
            //
            lblTitle.AutoEllipsis = true;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font(
                "Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.FromArgb(24, 30, 48);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(20, 0, 8, 0);
            lblTitle.Size = new Size(694, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            //
            // lblUserInfo
            //
            lblUserInfo.AutoEllipsis = true;
            lblUserInfo.Dock = DockStyle.Right;
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.ForeColor = Color.FromArgb(75, 85, 99);
            lblUserInfo.Location = new Point(694, 0);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Padding = new Padding(8, 0, 20, 0);
            lblUserInfo.Size = new Size(340, 60);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Xin chào: ...";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;

            //
            // panelMainContent
            //
            panelMainContent.BackColor = Color.FromArgb(244, 245, 247);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(0, 60);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(1034, 621);
            panelMainContent.TabIndex = 1;

            //
            // FormMainShell
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1264, 681);
            Controls.Add(panelWorkspace);
            Controls.Add(panelSidebar);
            Font = new Font("Segoe UI", 9F);
            Name = "FormMainShell";
            Size = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Mini";
            Load += FormMainShell_Load;

            panelSidebar.ResumeLayout(false);
            panelSidebarBottom.ResumeLayout(false);
            panelWorkspace.ResumeLayout(false);
            panelTopHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar = null!;
        private Label lblLogo = null!;

        private Button btnPOS = null!;
        private Button btnCategory = null!;
        private Button btnProduct = null!;
        private Button btnCustomer = null!;
        private Button btnReports = null!;
        private Button btnUserManage = null!;

        private Panel panelSidebarBottom = null!;
        private Button btnLogout = null!;

        private Panel panelWorkspace = null!;
        private Panel panelTopHeader = null!;
        private Label lblTitle = null!;
        private Label lblUserInfo = null!;

        private Panel panelMainContent = null!;
    }
}