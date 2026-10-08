using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private static readonly HttpClient Client = new()
        {
            BaseAddress = new Uri("https://localhost:7203/api/")
        };

        private static readonly JsonSerializerOptions JsonOptions =
            new(JsonSerializerDefaults.Web);

        private static readonly CultureInfo MoneyCulture =
            CultureInfo.GetCultureInfo("vi-VN");

        private readonly List<PosCartItemDto> _cart = new();

        private string _verifiedCustomerPhone = string.Empty;
        private bool _isBusy;

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
        }

        private static bool HasPermission()
        {
            string role = SessionManager.CurrentRole.Trim();

            return role.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                || role.Equals("Cashier", StringComparison.OrdinalIgnoreCase);
        }

        private void FormPOS_Load(object? sender, EventArgs e)
        {
            UpdateCartDisplay();
            SetBusy(false);
            txtBarcode.Focus();
        }

        private void SetupCartGrid()
        {
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductId",
                HeaderText = "Mã SP",
                Width = 60
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên sản phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Đơn giá",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    FormatProvider = MoneyCulture
                }
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "SL",
                Width = 45
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPrice",
                HeaderText = "Thành tiền",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N0",
                    FormatProvider = MoneyCulture
                }
            });
        }

        // ================= QUÉT BARCODE =================

        private async void txtBarcode_KeyDown(
            object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;

            string barcode = txtBarcode.Text.Trim();

            if (string.IsNullOrWhiteSpace(barcode))
            {
                return;
            }

            await RunActionAsync(async () =>
            {
                string json = await SendApiAsync(
                    HttpMethod.Get,
                    $"products/barcode/{Uri.EscapeDataString(barcode)}");

                var product = JsonSerializer.Deserialize<PosProductDto>(
                    json, JsonOptions)
                    ?? throw new InvalidOperationException(
                        "Dữ liệu sản phẩm không hợp lệ.");

                var existing = _cart.FirstOrDefault(
                    item => item.ProductId == product.ProductId);

                int nextQuantity = (existing?.Quantity ?? 0) + 1;

                if (nextQuantity > product.StockQuantity)
                {
                    throw new InvalidOperationException(
                        $"\"{product.ProductName}\" chỉ còn " +
                        $"{product.StockQuantity} sản phẩm!");
                }

                if (existing != null)
                {
                    if (existing.UnitPrice != product.Price)
                    {
                        throw new InvalidOperationException(
                            "Giá sản phẩm đã thay đổi. Hãy hủy giỏ và quét lại!");
                    }

                    existing.Quantity++;
                }
                else
                {
                    _cart.Add(new PosCartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                txtBarcode.Clear();
                UpdateCartDisplay();
            });

            txtBarcode.Focus();
        }

        // ================= TRA KHÁCH HÀNG =================

        private void txtCustomerPhone_TextChanged(
            object? sender, EventArgs e)
        {
            _verifiedCustomerPhone = string.Empty;

            lblCustomerName.Text =
                string.IsNullOrWhiteSpace(txtCustomerPhone.Text)
                    ? "Khách vãng lai"
                    : "Nhấn Enter để tra cứu khách hàng.";
        }

        private async void txtCustomerPhone_KeyDown(
            object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;

            await RunActionAsync(LookupCustomerAsync);
        }

        private async Task LookupCustomerAsync()
        {
            string phone = txtCustomerPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                _verifiedCustomerPhone = string.Empty;
                lblCustomerName.Text = "Khách vãng lai";
                return;
            }

            // Dùng endpoint search đang có trong CustomersController.
            string json = await SendApiAsync(
                HttpMethod.Get,
                $"customers/search?keyword={Uri.EscapeDataString(phone)}");

            var customers =
                JsonSerializer.Deserialize<List<PosCustomerDto>>(
                    json, JsonOptions)
                ?? new List<PosCustomerDto>();

            // Search có thể trả kết quả gần đúng: chọn số điện thoại khớp.
            var customer = customers.FirstOrDefault(
                item => item.PhoneNumber == phone);

            if (customer == null)
            {
                _verifiedCustomerPhone = string.Empty;
                lblCustomerName.Text = "Không tìm thấy khách hàng.";

                throw new InvalidOperationException(
                    "Số điện thoại chưa được đăng ký. " +
                    "Bạn có thể để trống để bán cho khách vãng lai.");
            }

            _verifiedCustomerPhone = phone;

            lblCustomerName.Text =
                $"{customer.CustomerName}\n" +
                $"Hạng: {customer.MembershipRank}";
        }

        // ================= TÍNH TIỀN =================

        private static string FormatMoney(decimal amount)
        {
            return amount.ToString("N0", MoneyCulture) + " đ";
        }

        private bool TryGetCash(out decimal cash)
        {
            return decimal.TryParse(
                txtCashReceived.Text.Trim(),
                NumberStyles.Number,
                MoneyCulture,
                out cash)
                && cash >= 0;
        }

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart;

            lblTotalAmount.Text =
                FormatMoney(_cart.Sum(item => item.TotalPrice));

            CalculateChange();
        }

        private void txtCashReceived_TextChanged(
            object? sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            decimal total = _cart.Sum(item => item.TotalPrice);

            if (!TryGetCash(out decimal cash))
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.Black;
                return;
            }

            decimal change = cash - total;

            lblChange.Text = change >= 0
                ? FormatMoney(change)
                : "Chưa đủ tiền!";

            lblChange.ForeColor = change >= 0
                ? Color.FromArgb(22, 140, 95)
                : Color.Red;
        }

        // ================= THANH TOÁN =================

        private async void btnCheckout_Click(
            object? sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                if (_cart.Count == 0)
                {
                    throw new InvalidOperationException(
                        "Giỏ hàng đang trống!");
                }

                decimal total = _cart.Sum(item => item.TotalPrice);

                if (!TryGetCash(out decimal cash) || cash < total)
                {
                    throw new InvalidOperationException(
                        "Vui lòng nhập đủ tiền khách đưa!");
                }

                string phone = txtCustomerPhone.Text.Trim();

                if (!string.IsNullOrWhiteSpace(phone)
                    && phone != _verifiedCustomerPhone)
                {
                    await LookupCustomerAsync();
                }

                var request = new
                {
                    CustomerPhone = phone,
                    CashReceived = cash,

                    Items = _cart.Select(item => new
                    {
                        item.ProductId,
                        item.Quantity,
                        ExpectedUnitPrice = item.UnitPrice
                    }).ToList()
                };

                string json = await SendApiAsync(
                    HttpMethod.Post,
                    "orders/checkout",
                    request);

                var result =
                    JsonSerializer.Deserialize<PosCheckoutResponse>(
                        json, JsonOptions)
                    ?? throw new InvalidOperationException(
                        "Phản hồi thanh toán không hợp lệ.");

                MessageBox.Show(
                    $"Đã lưu hóa đơn #{result.OrderId}.\n" +
                    $"Tổng tiền: {FormatMoney(result.TotalAmount)}\n" +
                    $"Tiền thừa: {FormatMoney(result.ChangeAmount)}",
                    "Thanh toán thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                ResetCart();
            });

            txtBarcode.Focus();
        }

        protected override bool ProcessCmdKey(
            ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                if (!_isBusy && btnCheckout.Enabled)
                {
                    btnCheckout.PerformClick();
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ================= HỦY GIỎ =================

        private void btnClearCart_Click(object? sender, EventArgs e)
        {
            if (_isBusy || !HasPermission())
            {
                return;
            }

            if (_cart.Count > 0)
            {
                var confirm = MessageBox.Show(
                    "Bạn muốn hủy toàn bộ giỏ hàng?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                {
                    return;
                }
            }

            ResetCart();
            txtBarcode.Focus();
        }

        private void ResetCart()
        {
            _cart.Clear();
            txtBarcode.Clear();
            txtCashReceived.Clear();
            txtCustomerPhone.Clear();

            _verifiedCustomerPhone = string.Empty;
            lblCustomerName.Text = "Khách vãng lai";

            UpdateCartDisplay();
        }

        // ================= GỌI API =================

        private static async Task<string> SendApiAsync(
            HttpMethod method,
            string endpoint,
            object? body = null)
        {
            if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                throw new InvalidOperationException(
                    "Bạn chưa đăng nhập.");
            }

            using var request = new HttpRequestMessage(method, endpoint);

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer", SessionManager.JwtToken);

            if (body != null)
            {
                request.Content = JsonContent.Create(body);
            }

            using var response = await Client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return content;
            }

            if ((int)response.StatusCode == 401)
            {
                throw new InvalidOperationException(
                    "Phiên đăng nhập không hợp lệ. Hãy đăng nhập lại.");
            }

            if ((int)response.StatusCode == 403)
            {
                throw new InvalidOperationException(
                    "Bạn không có quyền thực hiện thao tác này.");
            }

            string error = $"API trả về HTTP {(int)response.StatusCode}.";

            try
            {
                using var document = JsonDocument.Parse(content);

                if (document.RootElement.TryGetProperty(
                    "message", out var message))
                {
                    error = message.GetString() ?? error;
                }
                else if (document.RootElement.TryGetProperty(
                    "title", out var title))
                {
                    error = title.GetString() ?? error;
                }
            }
            catch (JsonException)
            {
                // Backend có thể trả nội dung không phải JSON.
            }

            throw new InvalidOperationException(error);
        }

        // ================= CHỐNG BẤM LẶP =================

        private async Task RunActionAsync(Func<Task> action)
        {
            if (_isBusy)
            {
                return;
            }

            if (!HasPermission())
            {
                MessageBox.Show(
                    "Chỉ Admin và Cashier được sử dụng POS!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                return;
            }

            SetBusy(true);

            try
            {
                await action();
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            finally
            {
                if (!IsDisposed)
                {
                    SetBusy(false);
                }
            }
        }

        private void SetBusy(bool busy)
        {
            _isBusy = busy;
            bool enabled = !busy && HasPermission();

            txtBarcode.Enabled = enabled;
            txtCustomerPhone.Enabled = enabled;
            txtCashReceived.Enabled = enabled;
            btnCheckout.Enabled = enabled;
            btnClearCart.Enabled = enabled;

            UseWaitCursor = busy;
        }

        private void FormPOS_FormClosing(
            object? sender, FormClosingEventArgs e)
        {
            // Tránh chuyển màn hình khi request đang xử lý.
            if (_isBusy)
            {
                e.Cancel = true;
            }
        }
    }

    public class PosCartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }

    public class PosProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    public class PosCustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string MembershipRank { get; set; } = string.Empty;
    }

    public class PosCheckoutResponse
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CashReceived { get; set; }
        public decimal ChangeAmount { get; set; }
    }
}