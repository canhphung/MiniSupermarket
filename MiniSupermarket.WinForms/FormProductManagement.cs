using System;
using System.Collections.Generic;
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
    public partial class FormProductManagement : Form
    {
        private static readonly HttpClient Client = new()
        {
            BaseAddress = new Uri("https://localhost:7203/api/")
        };

        private static readonly JsonSerializerOptions JsonOptions =
            new(JsonSerializerDefaults.Web);

        private readonly BindingSource _productSource = new();

        private bool _isBusy;

        public FormProductManagement()
        {
            InitializeComponent();
            SetupControls();
            SetupGrid();

            dgvProducts.DataSource = _productSource;

            // Gắn mỗi sự kiện một lần, không gắn thêm trong Designer.
            Load += FormProductManagement_Load;
            FormClosing += FormProductManagement_FormClosing;

            dgvProducts.CellClick += dgvProducts_CellClick;
            txtSearchBarcode.KeyDown += txtSearchBarcode_KeyDown;

            btnSearch.Click += btnSearch_Click;
            btnLoad.Click += btnLoad_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
        }

        private void SetupControls()
        {
            txtId.ReadOnly = true;

            txtBarcode.MaxLength = 50;
            txtProductName.MaxLength = 150;
            txtSearchBarcode.MaxLength = 50;

            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFilterCategory.DropDownStyle =
                ComboBoxStyle.DropDownList;

            nudPrice.Minimum = 0;
            nudPrice.Maximum = 9999999999999999.99m;
            nudPrice.DecimalPlaces = 2;
            nudPrice.ThousandsSeparator = true;

            nudStock.Minimum = 0;
            nudStock.Maximum = int.MaxValue;
            nudStock.DecimalPlaces = 0;
            nudStock.ThousandsSeparator = true;
        }

        private void SetupGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.MultiSelect = false;
            dgvProducts.RowHeadersVisible = false;

            dgvProducts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProductManagementDto.ProductId),
                HeaderText = "Mã SP",
                Width = 60
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProductManagementDto.Barcode),
                HeaderText = "Mã vạch",
                Width = 125
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName =
                    nameof(ProductManagementDto.ProductName),
                HeaderText = "Tên sản phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(ProductManagementDto.Price),
                HeaderText = "Đơn giá",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "N2",
                    FormatProvider = CultureInfo.GetCultureInfo("vi-VN")
                }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName =
                    nameof(ProductManagementDto.StockQuantity),
                HeaderText = "Tồn kho",
                Width = 75
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName =
                    nameof(ProductManagementDto.CategoryName),
                HeaderText = "Nhóm hàng",
                Width = 135
            });
        }

        private async void FormProductManagement_Load(
            object? sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                await LoadCategoriesToComboAsync();
                await LoadProductsAsync();
                ClearInputs();
            });
        }

        // ================= NẠP DỮ LIỆU =================

        private async Task LoadCategoriesToComboAsync()
        {
            string json = await SendApiAsync(
                HttpMethod.Get, "categories");

            var categories =
                JsonSerializer.Deserialize<List<ProductCategoryDto>>(
                    json, JsonOptions)
                ?? new List<ProductCategoryDto>();

            cboCategory.DisplayMember =
                nameof(ProductCategoryDto.CategoryName);
            cboCategory.ValueMember =
                nameof(ProductCategoryDto.CategoryId);
            cboCategory.DataSource = categories;

            // Dùng danh sách riêng để hai ComboBox không chọn theo nhau.
            var filterCategories = new List<ProductCategoryDto>
            {
                new()
                {
                    CategoryId = 0,
                    CategoryName = "Tất cả nhóm hàng"
                }
            };

            filterCategories.AddRange(categories.Select(c =>
                new ProductCategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName
                }));

            cboFilterCategory.DisplayMember =
                nameof(ProductCategoryDto.CategoryName);
            cboFilterCategory.ValueMember =
                nameof(ProductCategoryDto.CategoryId);
            cboFilterCategory.DataSource = filterCategories;
        }

        private async Task LoadProductsAsync()
        {
            string barcode = txtSearchBarcode.Text.Trim();

            int categoryId =
                (cboFilterCategory.SelectedItem as ProductCategoryDto)
                ?.CategoryId ?? 0;

            string endpoint =
                $"products?barcode={Uri.EscapeDataString(barcode)}";

            if (categoryId > 0)
            {
                endpoint += $"&categoryId={categoryId}";
            }

            string json = await SendApiAsync(
                HttpMethod.Get, endpoint);

            var products =
                JsonSerializer.Deserialize<List<ProductManagementDto>>(
                    json, JsonOptions)
                ?? new List<ProductManagementDto>();

            _productSource.DataSource = products;
            dgvProducts.ClearSelection();

            // Xóa chi tiết để không sửa nhầm sản phẩm từ kết quả cũ.
            ClearInputs();
        }

        // ================= CHỌN SẢN PHẨM =================

        private void dgvProducts_CellClick(
            object? sender, DataGridViewCellEventArgs e)
        {
            if (_isBusy || e.RowIndex < 0)
            {
                return;
            }

            if (dgvProducts.Rows[e.RowIndex].DataBoundItem
                is not ProductManagementDto product)
            {
                return;
            }

            txtId.Text = product.ProductId.ToString();
            txtBarcode.Text = product.Barcode;
            txtProductName.Text = product.ProductName;

            nudPrice.Value = Math.Clamp(
                product.Price, nudPrice.Minimum, nudPrice.Maximum);

            nudStock.Value = Math.Clamp(
                (decimal)product.StockQuantity,
                nudStock.Minimum,
                nudStock.Maximum);

            cboCategory.SelectedValue = product.CategoryId;
        }

        // ================= TÌM KIẾM / TẢI LẠI =================

        private async void btnSearch_Click(
            object? sender, EventArgs e)
        {
            await RunActionAsync(LoadProductsAsync);
        }

        private async void txtSearchBarcode_KeyDown(
            object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            e.Handled = true;

            await RunActionAsync(LoadProductsAsync);
        }

        private async void btnLoad_Click(
            object? sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                txtSearchBarcode.Clear();

                await LoadCategoriesToComboAsync();
                await LoadProductsAsync();
            });
        }

        // ================= THÊM / SỬA / XÓA =================

        private ProductManagementSaveRequest GetInput()
        {
            string barcode = txtBarcode.Text.Trim();
            string name = txtProductName.Text.Trim();

            if (string.IsNullOrWhiteSpace(barcode))
            {
                throw new InvalidOperationException(
                    "Vui lòng nhập mã vạch!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException(
                    "Vui lòng nhập tên sản phẩm!");
            }

            if (cboCategory.SelectedItem
                is not ProductCategoryDto category
                || category.CategoryId <= 0)
            {
                throw new InvalidOperationException(
                    "Vui lòng chọn nhóm hàng!");
            }

            return new ProductManagementSaveRequest
            {
                Barcode = barcode,
                ProductName = name,
                Price = nudPrice.Value,
                StockQuantity = (int)nudStock.Value,
                CategoryId = category.CategoryId
            };
        }

        private int GetSelectedId()
        {
            if (!int.TryParse(txtId.Text, out int id) || id <= 0)
            {
                throw new InvalidOperationException(
                    "Vui lòng chọn sản phẩm trong bảng!");
            }

            return id;
        }

        private async void btnAdd_Click(
            object? sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                var request = GetInput();

                await SendApiAsync(
                    HttpMethod.Post, "products", request);

                MessageBox.Show(
                    "Thêm sản phẩm thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadProductsAsync();
            });
        }

        private async void btnUpdate_Click(
            object? sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                int id = GetSelectedId();
                var request = GetInput();

                await SendApiAsync(
                    HttpMethod.Put, $"products/{id}", request);

                MessageBox.Show(
                    "Cập nhật thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadProductsAsync();
            });
        }

        private async void btnDelete_Click(
            object? sender, EventArgs e)
        {
            await RunActionAsync(async () =>
            {
                int id = GetSelectedId();

                var confirm = MessageBox.Show(
                    $"Xóa sản phẩm \"{txtProductName.Text}\"?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm != DialogResult.Yes)
                {
                    return;
                }

                await SendApiAsync(
                    HttpMethod.Delete, $"products/{id}");

                MessageBox.Show(
                    "Đã xóa sản phẩm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadProductsAsync();
            });
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();

            nudPrice.Value = 0;
            nudStock.Value = 0;

            cboCategory.SelectedIndex =
                cboCategory.Items.Count > 0 ? 0 : -1;
        }

        // ================= API KÈM JWT =================

        private static async Task<string> SendApiAsync(
            HttpMethod method,
            string endpoint,
            object? body = null)
        {
            if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                throw new InvalidOperationException(
                    "Bạn chưa đăng nhập!");
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

            string content =
                await response.Content.ReadAsStringAsync();

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
                    "Bạn không có quyền quản lý sản phẩm!");
            }

            string error =
                $"API trả về HTTP {(int)response.StatusCode}.";

            try
            {
                using var document = JsonDocument.Parse(content);
                var root = document.RootElement;

                if (root.TryGetProperty("message", out var message))
                {
                    error = message.GetString() ?? error;
                }
                else if (root.TryGetProperty("errors", out var errors))
                {
                    var messages = errors.EnumerateObject()
                        .SelectMany(property =>
                            property.Value.EnumerateArray()
                                .Select(value => value.GetString()
                                    ?? "Dữ liệu không hợp lệ."));

                    error = string.Join(Environment.NewLine, messages);
                }
                else if (root.TryGetProperty("title", out var title))
                {
                    error = title.GetString() ?? error;
                }
            }
            catch (JsonException)
            {
                // Giữ thông báo HTTP nếu phản hồi không phải JSON.
            }

            throw new InvalidOperationException(error);
        }

        // ================= PHÂN QUYỀN / CHỐNG BẤM LẶP =================

        private static bool HasPermission()
        {
            string role = SessionManager.CurrentRole?.Trim()
                ?? string.Empty;

            return role.Equals(
                "Admin", StringComparison.OrdinalIgnoreCase)
                || role.Equals(
                    "Warehouse", StringComparison.OrdinalIgnoreCase);
        }

        private async Task RunActionAsync(Func<Task> action)
        {
            if (_isBusy)
            {
                return;
            }

            if (!HasPermission())
            {
                SetBusy(false);

                MessageBox.Show(
                    "Chỉ Admin và Warehouse được quản lý sản phẩm!",
                    "Từ chối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            SetBusy(true);

            try
            {
                await action();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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

            txtSearchBarcode.Enabled = enabled;
            cboFilterCategory.Enabled = enabled;
            btnSearch.Enabled = enabled;

            dgvProducts.Enabled = enabled;

            txtBarcode.Enabled = enabled;
            txtProductName.Enabled = enabled;
            nudPrice.Enabled = enabled;
            nudStock.Enabled = enabled;
            cboCategory.Enabled = enabled;

            btnLoad.Enabled = enabled;
            btnAdd.Enabled = enabled;
            btnUpdate.Enabled = enabled;
            btnDelete.Enabled = enabled;

            UseWaitCursor = busy;
        }

        private void FormProductManagement_FormClosing(
            object? sender, FormClosingEventArgs e)
        {
            if (_isBusy)
            {
                e.Cancel = true;
            }
        }
    }

    public class ProductManagementDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class ProductCategoryDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }

    public class ProductManagementSaveRequest
    {
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }
}