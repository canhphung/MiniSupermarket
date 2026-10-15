using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniSupermarket.WinForms.UI;

namespace MiniSupermarket.WinForms;

public partial class FormQuickReport : Form
{
    private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
    private static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri("https://localhost:7203/api/"),
        Timeout = TimeSpan.FromSeconds(15)
    };
    private bool _isBusy;
    private DateTimePicker dtpToDate = null!;
    private DataGridView dgvCashiers = null!, dgvTopProducts = null!, dgvOrders = null!;
    private Button btnCancelOrder = null!;

    public FormQuickReport()
    {
        InitializeComponent();
        BuildDetailUi();
    }

    private static bool IsAdmin() => string.Equals(SessionManager.CurrentRole?.Trim(), "Admin", StringComparison.OrdinalIgnoreCase);

    private void BuildDetailUi()
    {
        pageTitle.Text = "Báo cáo & Hóa đơn";
        pageSubtitle.Text = "Doanh thu theo ngày, thu ngân, mặt hàng và quản lý hóa đơn.";
        reportLayout.RowStyles[1].Height = 150;
        reportLayout.RowStyles[2].Height = 64;
        lblReportNote.Padding = new Padding(12, 6, 12, 6);
        pageLayout.MinimumSize = new System.Drawing.Size(860, 620);
        filterLayout.Controls.Clear();
        filterLayout.ColumnStyles.Clear();
        filterLayout.ColumnCount = 3;
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
        dtpToDate = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" };
        filterLayout.Controls.Add(DateField("Từ ngày", dtpReportDate), 0, 0);
        filterLayout.Controls.Add(DateField("Đến ngày", dtpToDate), 1, 0);
        btnRunReport.Margin = new Padding(12, 10, 0, 0);
        filterLayout.Controls.Add(btnRunReport, 2, 0);

        var tabs = new TabControl { Dock = DockStyle.Fill, Margin = new Padding(0) };
        dgvCashiers = Grid();
        AddColumn(dgvCashiers, "CashierUsername", "Thu ngân");
        AddColumn(dgvCashiers, "TotalOrders", "Hóa đơn", "N0");
        AddColumn(dgvCashiers, "TotalRevenue", "Doanh thu (đ)", "N0");
        AddColumn(dgvCashiers, "TotalPointsAwarded", "Điểm đã tặng", "N0");
        dgvTopProducts = Grid();
        AddColumn(dgvTopProducts, "ProductId", "Mã SP");
        AddColumn(dgvTopProducts, "ProductName", "Sản phẩm");
        AddColumn(dgvTopProducts, "TotalQuantity", "Số lượng bán", "N0");
        AddColumn(dgvTopProducts, "TotalRevenue", "Doanh thu (đ)", "N0");
        dgvOrders = Grid();
        AddColumn(dgvOrders, "OrderId", "Mã HĐ");
        AddColumn(dgvOrders, "CreatedAtVietnam", "Thời gian", "dd/MM HH:mm");
        AddColumn(dgvOrders, "CashierUsername", "Thu ngân");
        AddColumn(dgvOrders, "CustomerPhone", "Điện thoại");
        AddColumn(dgvOrders, "TotalAmount", "Tổng tiền (đ)", "N0");
        AddColumn(dgvOrders, "RewardPointsAwarded", "Điểm tặng", "N0");
        var cashierTab = new TabPage("Theo thu ngân") { BackColor = UiTheme.Surface, Padding = new Padding(12) };
        var topTab = new TabPage("Top 5 sản phẩm") { BackColor = UiTheme.Surface, Padding = new Padding(12) };
        var orderTab = new TabPage("Hóa đơn & Hủy đơn") { BackColor = UiTheme.Surface, Padding = new Padding(12) };
        cashierTab.Controls.Add(dgvCashiers);
        topTab.Controls.Add(dgvTopProducts);
        var orderLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
        orderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        orderLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
        orderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        btnCancelOrder = new Button { Text = "Hủy hóa đơn đã chọn", Tag = "danger", Dock = DockStyle.Right, Width = 210, Enabled = false };
        btnCancelOrder.Click += CancelOrder_Click;
        orderLayout.Controls.Add(btnCancelOrder, 0, 0);
        orderLayout.Controls.Add(dgvOrders, 0, 1);
        orderTab.Controls.Add(orderLayout);
        tabs.TabPages.AddRange(new[] { cashierTab, topTab, orderTab });
        reportLayout.Controls.Add(tabs, 0, 3);
        dgvOrders.SelectionChanged += (_, _) => UpdateCancelButton();
        UiTheme.Apply(tabs);
    }

    private static Control DateField(string caption, DateTimePicker picker)
    {
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 1, Margin = new Padding(0, 0, 12, 0) };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 22));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.Controls.Add(new Label { Text = caption, Dock = DockStyle.Fill, ForeColor = UiTheme.Muted }, 0, 0);
        picker.Dock = DockStyle.Fill;
        picker.Margin = new Padding(0);
        layout.Controls.Add(picker, 0, 1);
        return layout;
    }

    private static DataGridView Grid() => new()
    {
        Dock = DockStyle.Fill, AutoGenerateColumns = false, ReadOnly = true,
        AllowUserToAddRows = false, AllowUserToDeleteRows = false, MultiSelect = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    };
    private static void AddColumn(DataGridView grid, string property, string caption, string format = "")
        => grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = property, HeaderText = caption,
            DefaultCellStyle = new DataGridViewCellStyle { Format = format, FormatProvider = VietnameseCulture }
        });

    private void FormQuickReport_Load(object? sender, EventArgs e)
    {
        dtpReportDate.Value = dtpToDate.Value = DateTime.UtcNow.AddHours(7).Date;
        ClearReport();
        SetBusy(false);
        lblReportNote.Text = IsAdmin() ? "Chọn khoảng ngày và bấm Xem báo cáo." : "Bạn không có quyền xem báo cáo.";
    }

    private async void btnRunReport_Click(object? sender, EventArgs e) => await RunAsync(LoadReportAsync);

    private async Task LoadReportAsync()
    {
        if (dtpReportDate.Value.Date > dtpToDate.Value.Date) throw new InvalidOperationException("Ngày bắt đầu phải trước hoặc bằng ngày kết thúc.");
        string range = $"fromDate={dtpReportDate.Value:yyyy-MM-dd}&toDate={dtpToDate.Value:yyyy-MM-dd}";
        var cashiersTask = SendAsync<List<CashierReportRow>>(HttpMethod.Get, "reports/revenue-by-date-range?" + range);
        var topTask = SendAsync<List<TopProductRow>>(HttpMethod.Get, "reports/top-products?" + range);
        var ordersTask = SendAsync<List<OrderReportRow>>(HttpMethod.Get, "orders?" + range);
        await Task.WhenAll(cashiersTask, topTask, ordersTask);
        if (IsDisposed || Disposing) return;
        var cashiers = await cashiersTask;
        var top = await topTask;
        var orders = await ordersTask;
        dgvCashiers.DataSource = cashiers;
        dgvTopProducts.DataSource = top;
        dgvOrders.DataSource = orders;
        dgvOrders.ClearSelection();
        lblTotalOrders.Text = cashiers.Sum(c => c.TotalOrders).ToString("N0", VietnameseCulture);
        lblTotalRevenue.Text = cashiers.Sum(c => c.TotalRevenue).ToString("N0", VietnameseCulture) + " đ";
        lblBestSeller.Text = top.FirstOrDefault()?.ProductName ?? "Chưa có dữ liệu";
        lblReportNote.Text = $"Từ {dtpReportDate.Value:dd/MM/yyyy} đến {dtpToDate.Value:dd/MM/yyyy}, giờ Việt Nam.\n"
            + "Bảng hóa đơn hiển thị tối đa 200 dòng mới nhất; chỉ số tổng tính toàn bộ khoảng ngày.";
    }

    private async void CancelOrder_Click(object? sender, EventArgs e)
    {
        if (_isBusy || !IsAdmin() || dgvOrders.SelectedRows.Count == 0
            || dgvOrders.SelectedRows[0].DataBoundItem is not OrderReportRow order) return;
        if (MessageBox.Show($"Hủy hóa đơn #{order.OrderId}?\nHệ thống sẽ hoàn kho, thu hồi điểm thưởng và xóa hóa đơn.",
            "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
        await RunAsync(async () =>
        {
            await SendAsync<CancelReportRow>(HttpMethod.Delete, $"checkout/cancel/{order.OrderId}");
            await LoadReportAsync();
        });
    }

    private static async Task<T> SendAsync<T>(HttpMethod method, string endpoint)
    {
        if (string.IsNullOrWhiteSpace(SessionManager.JwtToken)) throw new InvalidOperationException("Hãy đăng nhập lại.");
        using var request = new HttpRequestMessage(method, endpoint);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
        using var response = await Client.SendAsync(request);
        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new InvalidOperationException("Phiên đăng nhập đã hết hạn. Hãy đăng nhập lại.");
        if (response.StatusCode == HttpStatusCode.Forbidden) throw new InvalidOperationException("Bạn không có quyền thực hiện thao tác này.");
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<ReportApiError>();
            throw new InvalidOperationException(error?.Message ?? $"HTTP {(int)response.StatusCode}");
        }
        return await response.Content.ReadFromJsonAsync<T>() ?? throw new InvalidOperationException("Dữ liệu API không hợp lệ.");
    }

    private async Task RunAsync(Func<Task> action)
    {
        if (_isBusy || !IsAdmin()) return;
        ClearReport();
        SetBusy(true);
        lblReportNote.Text = "Đang xử lý...";
        try { await action(); }
        catch (Exception ex)
        {
            if (IsDisposed || Disposing) return;
            ClearReport();
            lblReportNote.Text = "Không thể hoàn tất yêu cầu. Hãy tải lại báo cáo để kiểm tra trạng thái.";
            MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        finally { if (!IsDisposed && !Disposing) SetBusy(false); }
    }

    private void ClearReport()
    {
        lblTotalOrders.Text = lblTotalRevenue.Text = lblBestSeller.Text = "—";
        dgvCashiers.DataSource = dgvTopProducts.DataSource = dgvOrders.DataSource = null;
    }
    private void SetBusy(bool busy)
    {
        _isBusy = busy;
        btnRunReport.Enabled = dtpReportDate.Enabled = dtpToDate.Enabled = !busy && IsAdmin();
        dgvOrders.Enabled = !busy && IsAdmin();
        UpdateCancelButton();
        UseWaitCursor = busy;
    }
    private void UpdateCancelButton() => btnCancelOrder.Enabled = !_isBusy && IsAdmin() && dgvOrders.SelectedRows.Count > 0;
}

public record CashierReportRow(string CashierUsername, long TotalOrders, decimal TotalRevenue, long TotalPointsAwarded);
public record TopProductRow(int ProductId, string ProductName, long TotalQuantity, decimal TotalRevenue);
public record OrderReportRow(int OrderId, DateTime CreatedAtUtc, string CashierUsername,
    string? CustomerPhone, decimal TotalAmount, int RewardPointsAwarded)
{
    public DateTime CreatedAtVietnam => CreatedAtUtc.AddHours(7);
}
public record CancelReportRow(int OrderId, int RewardPointsDeducted);
public record ReportApiError(string Message);
