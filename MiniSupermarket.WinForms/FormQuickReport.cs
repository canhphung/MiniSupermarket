using System;
using System.Globalization;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private static readonly CultureInfo VietnameseCulture =
            CultureInfo.GetCultureInfo("vi-VN");

        public FormQuickReport()
        {
            InitializeComponent();
        }

        private static bool IsAdmin()
        {
            return string.Equals(
                SessionManager.CurrentRole?.Trim(),
                "Admin",
                StringComparison.OrdinalIgnoreCase);
        }

        private void FormQuickReport_Load(
            object? sender,
            EventArgs e)
        {
            dtpReportDate.Value = DateTime.Today;

            bool allowed = IsAdmin();

            btnRunReport.Enabled = allowed;
            dtpReportDate.Enabled = allowed;

            if (!allowed)
            {
                lblReportNote.Text =
                    "Bạn không có quyền xem dữ liệu tài chính của siêu thị.";
            }
        }

        private void btnRunReport_Click(
            object? sender,
            EventArgs e)
        {
            // Bảo vệ cấp Action: kiểm tra lại khi thực hiện thao tác.
            if (!IsAdmin())
            {
                MessageBox.Show(
                    "Bạn không có quyền xem dữ liệu tài chính của siêu thị!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);

                return;
            }

            DateTime selectedDate = dtpReportDate.Value.Date;

            // Dữ liệu minh họa, chưa lấy từ database.
            int totalOrders;
            decimal totalRevenue;
            string bestSeller;

            if (selectedDate == DateTime.Today)
            {
                totalOrders = 12;
                totalRevenue = 1850000m;
                bestSeller = "Coca-Cola lon 330ml";
            }
            else if (selectedDate == DateTime.Today.AddDays(-1))
            {
                totalOrders = 8;
                totalRevenue = 1240000m;
                bestSeller = "Mì Hảo Hảo";
            }
            else
            {
                totalOrders = 0;
                totalRevenue = 0m;
                bestSeller = "Chưa có dữ liệu";
            }

            lblTotalOrders.Text = totalOrders.ToString();

            lblTotalRevenue.Text =
                totalRevenue.ToString("N0", VietnameseCulture) + " đ";

            lblBestSeller.Text = bestSeller;

            lblReportNote.Text =
                $"Báo cáo ngày {selectedDate:dd/MM/yyyy}.\n" +
                "DỮ LIỆU MẪU — chưa kết nối API doanh thu.";
        }

        private void FormQuickReport_Load_1(object sender, EventArgs e)
        {

        }
    }
}