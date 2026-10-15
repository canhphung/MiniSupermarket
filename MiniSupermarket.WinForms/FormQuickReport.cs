using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private static readonly CultureInfo VietnameseCulture =
            CultureInfo.GetCultureInfo("vi-VN");

        private static readonly HttpClient Client = new()
        {
            BaseAddress = new Uri("https://localhost:7203/api/"),
            Timeout = TimeSpan.FromSeconds(15)
        };

        private bool _isBusy;

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
            // Ngày hiện tại tại Việt Nam.
            dtpReportDate.Value = DateTime.UtcNow.AddHours(7).Date;

            ClearReport();
            SetBusy(false);

            lblReportNote.Text = IsAdmin()
                ? "Chọn ngày và bấm Xem báo cáo."
                : "Bạn không có quyền xem báo cáo doanh thu.";
        }

        private async void btnRunReport_Click(
            object? sender,
            EventArgs e)
        {
            if (_isBusy)
            {
                return;
            }

            if (!IsAdmin())
            {
                MessageBox.Show(
                    "Chỉ Admin được xem báo cáo doanh thu!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DateTime selectedDate = dtpReportDate.Value.Date;

            ClearReport();
            SetBusy(true);
            lblReportNote.Text = "Đang tải báo cáo...";

            try
            {
                var report = await LoadReportAsync(selectedDate);

                if (IsDisposed || Disposing)
                {
                    return;
                }

                lblTotalOrders.Text =
                    report.TotalOrders.ToString("N0", VietnameseCulture);

                lblTotalRevenue.Text =
                    report.TotalRevenue.ToString(
                        "N0", VietnameseCulture) + " đ";

                lblBestSeller.Text = report.BestSeller;

                lblReportNote.Text =
                    $"Báo cáo ngày {selectedDate:dd/MM/yyyy}, giờ Việt Nam.\n"
                    + (report.TotalOrders == 0
                        ? "Ngày này chưa có hóa đơn thanh toán."
                        : "Dữ liệu được tổng hợp từ hóa đơn trong hệ thống.");
            }
            catch (Exception ex)
            {
                if (IsDisposed || Disposing)
                {
                    return;
                }

                ClearReport();
                lblReportNote.Text = "Không thể tải báo cáo. Hãy thử lại.";

                MessageBox.Show(
                    ex.Message,
                    "Lỗi tải báo cáo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed && !Disposing)
                {
                    SetBusy(false);
                }
            }
        }

        private static async Task<DailyRevenueDto> LoadReportAsync(
            DateTime selectedDate)
        {
            if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                throw new InvalidOperationException(
                    "Bạn chưa đăng nhập. Hãy đăng nhập lại.");
            }

            string dateText = selectedDate.ToString(
                "yyyy-MM-dd", CultureInfo.InvariantCulture);

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"reports/daily?date={dateText}");

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    SessionManager.JwtToken);

            using var response = await Client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new InvalidOperationException(
                    "Phiên đăng nhập không hợp lệ. Hãy đăng nhập lại.");
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new InvalidOperationException(
                    "Bạn không có quyền xem báo cáo doanh thu.");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException(
                    $"Không tải được báo cáo. HTTP {(int)response.StatusCode}.");
            }

            return await response.Content
                .ReadFromJsonAsync<DailyRevenueDto>()
                ?? throw new InvalidOperationException(
                    "Dữ liệu báo cáo không hợp lệ.");
        }

        private void ClearReport()
        {
            lblTotalOrders.Text = "—";
            lblTotalRevenue.Text = "—";
            lblBestSeller.Text = "—";
        }

        private void SetBusy(bool busy)
        {
            _isBusy = busy;

            bool enabled = !busy && IsAdmin();

            btnRunReport.Enabled = enabled;
            dtpReportDate.Enabled = enabled;
            UseWaitCursor = busy;
        }
    }

    public class DailyRevenueDto
    {
        public DateOnly Date { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public string BestSeller { get; set; } = string.Empty;

        public long BestSellerQuantity { get; set; }
    }
}