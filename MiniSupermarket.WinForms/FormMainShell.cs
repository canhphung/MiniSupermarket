using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        private Form? _activeForm;

        private string _currentRole = string.Empty;

        private static readonly Color SidebarColor =
            Color.FromArgb(24, 30, 48);

        private static readonly Color ActiveButtonColor =
            Color.FromArgb(41, 100, 180);

        public FormMainShell()
        {
            InitializeComponent();

            // Designer trước đó chưa gắn sự kiện cho ba nút này.
            btnProduct.Click += btnProduct_Click;
            btnReports.Click += btnReports_Click;
            btnUserManage.Click += btnUserManage_Click;
        }

        // ================= KHỞI TẠO =================

        private void FormMainShell_Load(object? sender, EventArgs e)
        {
            _currentRole = NormalizeRole(SessionManager.CurrentRole);

            lblUserInfo.Text =
                $"Nhân viên: {SessionManager.CurrentUsername}" +
                $" | Vai trò: [{_currentRole}]";

            if (!ApplyRolePermissions(_currentRole))
            {
                MessageBox.Show(
                    "Tài khoản chưa được cấp quyền hạn hợp lệ!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                ClearSession();

                // Trả về màn hình đăng nhập.
                DialogResult = DialogResult.Retry;
                Close();
                return;
            }

            OpenDefaultScreenByRole(_currentRole);
        }

        private static string NormalizeRole(string? role)
        {
            return (role ?? string.Empty).Trim().ToUpperInvariant();
        }

        // ================= PHÂN QUYỀN =================

        private bool ApplyRolePermissions(string role)
        {
            // Mặc định khóa toàn bộ menu chức năng.
            btnPOS.Visible = false;
            btnCategory.Visible = false;
            btnProduct.Visible = false;
            btnCustomer.Visible = false;
            btnReports.Visible = false;
            btnUserManage.Visible = false;

            switch (role)
            {
                case "ADMIN":
                    btnPOS.Visible = true;
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    btnCustomer.Visible = true;
                    btnReports.Visible = true;
                    btnUserManage.Visible = true;
                    return true;

                case "CASHIER":
                    btnPOS.Visible = true;
                    btnCustomer.Visible = true;
                    return true;

                case "WAREHOUSE":
                    btnCategory.Visible = true;
                    btnProduct.Visible = true;
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Kiểm tra quyền khi thực hiện điều hướng.
        /// </summary>
        private bool CanAccess(Button menuButton)
        {
            switch (_currentRole)
            {
                case "ADMIN":
                    return menuButton == btnPOS
                        || menuButton == btnCategory
                        || menuButton == btnProduct
                        || menuButton == btnCustomer
                        || menuButton == btnReports
                        || menuButton == btnUserManage;

                case "CASHIER":
                    return menuButton == btnPOS
                        || menuButton == btnCustomer;

                case "WAREHOUSE":
                    return menuButton == btnCategory
                        || menuButton == btnProduct;

                default:
                    return false;
            }
        }

        private bool EnsureAccess(Button menuButton)
        {
            if (CanAccess(menuButton))
            {
                return true;
            }

            MessageBox.Show(
                "Bạn không có quyền truy cập chức năng này!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return false;
        }

        private void OpenDefaultScreenByRole(string role)
        {
            switch (role)
            {
                case "ADMIN":
                case "WAREHOUSE":
                    OpenChildForm<FormCategoryManagement>(
                        "QUẢN LÝ DANH MỤC NHÓM HÀNG",
                        btnCategory);
                    break;

                case "CASHIER":
                    OpenChildForm<FormCustomerManagement>(
                        "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM",
                        btnCustomer);
                    break;
            }
        }

        // ================= ĐIỀU HƯỚNG =================

        /// <summary>
        /// Nhúng form con vào vùng nội dung.
        /// Chỉ tạo form mới khi chuyển sang loại màn hình khác.
        /// </summary>
        private void OpenChildForm<TForm>(
            string screenTitle,
            Button menuButton)
            where TForm : Form, new()
        {
            if (!EnsureAccess(menuButton))
            {
                return;
            }

            // Đang ở cùng màn hình: giữ nguyên dữ liệu và form hiện tại.
            if (_activeForm is TForm && !_activeForm.IsDisposed)
            {
                lblTitle.Text = screenTitle;
                HighlightActiveButton(menuButton);
                _activeForm.BringToFront();
                return;
            }

            // Khởi tạo trước để giữ màn hình hiện tại nếu constructor lỗi.
            TForm childForm;

            try
            {
                childForm = new TForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể khởi tạo màn hình: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (_activeForm != null && !_activeForm.IsDisposed)
            {
                _activeForm.Close();

                // Form cũ có thể hủy đóng, ví dụ khi chưa lưu dữ liệu.
                if (!_activeForm.IsDisposed)
                {
                    childForm.Dispose();
                    return;
                }
            }

            _activeForm = null;
            panelMainContent.Controls.Clear();
            panelMainContent.Tag = null;

            try
            {
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // Form Khách hàng hiện có layout rộng hơn vùng nội dung.
                // Cho phép cuộn để truy cập các control nằm ngoài vùng nhìn.
                childForm.AutoScroll = true;

                panelMainContent.Controls.Add(childForm);
                panelMainContent.Tag = childForm;

                _activeForm = childForm;

                childForm.Show();
                childForm.BringToFront();

                lblTitle.Text = screenTitle;
                HighlightActiveButton(menuButton);
            }
            catch (Exception ex)
            {
                panelMainContent.Controls.Remove(childForm);
                panelMainContent.Tag = null;
                _activeForm = null;

                childForm.Dispose();

                lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";
                ResetMenuColors();

                MessageBox.Show(
                    "Không thể mở màn hình: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ResetMenuColors()
        {
            foreach (Control control in panelSidebar.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = SidebarColor;
                }
            }
        }

        private void HighlightActiveButton(Button activeButton)
        {
            ResetMenuColors();
            activeButton.BackColor = ActiveButtonColor;
        }

        // ================= SỰ KIỆN MENU =================

        private void btnCategory_Click(object? sender, EventArgs e)
        {
            OpenChildForm<FormCategoryManagement>(
                "QUẢN LÝ DANH MỤC NHÓM HÀNG",
                btnCategory);
        }

        private void btnCustomer_Click(object? sender, EventArgs e)
        {
            OpenChildForm<FormCustomerManagement>(
                "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM",
                btnCustomer);
        }

        private void btnPOS_Click(object? sender, EventArgs e)
        {
            OpenChildForm<FormPOS>(
                "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)",
                btnPOS);
        }

        private void btnProduct_Click(object? sender, EventArgs e)
        {
            OpenChildForm<FormProductManagement>(
                "QUẢN LÝ SẢN PHẨM & KHO HÀNG",
                btnProduct);
        }

        private void btnReports_Click(object? sender, EventArgs e)
        {
            // Kiểm tra quyền ngay trong sự kiện điều hướng.
            if (!string.Equals(
                SessionManager.CurrentRole?.Trim(),
                "Admin",
                StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Bạn không có quyền xem dữ liệu tài chính của siêu thị!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);

                return;
            }

            OpenChildForm<FormQuickReport>(
                "BÁO CÁO DOANH THU & HIỆU SUẤT",
                btnReports);
        }

        private void btnUserManage_Click(object? sender, EventArgs e)
        {
            OpenChildForm<FormUserManagement>(
                "QUẢN TRỊ TÀI KHOẢN VÀ PHÂN QUYỀN",
                btnUserManage);
        }

        private void ShowPendingFeature(
            Button menuButton,
            string featureName)
        {
            if (!EnsureAccess(menuButton))
            {
                return;
            }

            MessageBox.Show(
                $"Chức năng \"{featureName}\" chưa được triển khai.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ================= ĐĂNG XUẤT =================

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất phiên làm việc?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            // Cho phép form con kiểm tra dữ liệu chưa lưu trước khi đóng.
            if (_activeForm != null && !_activeForm.IsDisposed)
            {
                _activeForm.Close();

                if (!_activeForm.IsDisposed)
                {
                    return;
                }
            }

            _activeForm = null;
            panelMainContent.Tag = null;

            ClearSession();

            // FormLogin sẽ nhận Retry và hiển thị lại.
            DialogResult = DialogResult.Retry;
            Close();
        }

        private static void ClearSession()
        {
            SessionManager.JwtToken = string.Empty;
            SessionManager.CurrentUsername = string.Empty;
            SessionManager.CurrentRole = string.Empty;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_activeForm != null && !_activeForm.IsDisposed)
            {
                _activeForm.Dispose();
            }

            _activeForm = null;
            panelMainContent.Tag = null;

            ClearSession();

            base.OnFormClosed(e);
        }
    }
}