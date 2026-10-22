using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        private static HttpClient Client => ApiClientService.Client;

        private static readonly JsonSerializerOptions JsonOptions =
            new(JsonSerializerDefaults.Web);

        private UserManagementDto? _selectedUser;
        private bool _isBusy;

        public FormUserManagement()
        {
            InitializeComponent();
            ConfigureGrid();

            cboRole.SelectedIndex = 1;
            UpdateButtons();
        }

        // ================= KHỞI TẠO =================

        private void ConfigureGrid()
        {
            dgvUsers.Columns.Clear();

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                FillWeight = 35
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                DataPropertyName = "Username",
                HeaderText = "Tên đăng nhập",
                FillWeight = 100
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                DataPropertyName = "FullName",
                HeaderText = "Họ và tên",
                FillWeight = 130
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Role",
                DataPropertyName = "Role",
                HeaderText = "Vai trò",
                FillWeight = 85
            });

            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                DataPropertyName = "IsActive",
                HeaderText = "Hoạt động",
                FillWeight = 65
            });
        }

        private async void FormUserManagement_Load(
            object? sender,
            EventArgs e)
        {
            await RunActionAsync(() => LoadUsersAsync());
        }

        // ================= DANH SÁCH =================

        private async Task LoadUsersAsync(int? selectedId = null)
        {
            string json = await SendApiAsync(
                HttpMethod.Get,
                "users");

            var users = JsonSerializer
                .Deserialize<List<UserManagementDto>>(json, JsonOptions)
                ?? new List<UserManagementDto>();

            dgvUsers.DataSource = users;

            dgvUsers.ClearSelection();
            dgvUsers.CurrentCell = null;
            ClearInputs();

            if (selectedId.HasValue)
            {
                foreach (DataGridViewRow row in dgvUsers.Rows)
                {
                    if (row.DataBoundItem is UserManagementDto user
                        && user.Id == selectedId.Value)
                    {
                        dgvUsers.CurrentCell = row.Cells["Username"];
                        row.Selected = true;
                        DisplaySelectedUser(user);
                        break;
                    }
                }
            }
        }

        private void dgvUsers_SelectionChanged(
            object? sender,
            EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                _selectedUser = null;
                lblSelectedUser.Text = "Chưa chọn tài khoản.";
                UpdateButtons();
                return;
            }

            if (dgvUsers.SelectedRows[0].DataBoundItem
                is UserManagementDto user)
            {
                DisplaySelectedUser(user);
            }
        }

        private void DisplaySelectedUser(UserManagementDto user)
        {
            _selectedUser = user;

            txtUsername.Text = user.Username;
            txtFullName.Text = user.FullName;

            // Không hiển thị hoặc tải mật khẩu từ server.
            txtPassword.Clear();

            cboRole.SelectedItem = user.Role;

            lblSelectedUser.Text =
                $"Đã chọn: {user.Username} (ID: {user.Id})\n" +
                $"Trạng thái: {(user.IsActive ? "Hoạt động" : "Đã khóa")}";

            btnToggleLock.Text = user.IsActive
                ? "Khóa tài khoản"
                : "Mở khóa tài khoản";

            UpdateButtons();
        }

        private void ClearInputs()
        {
            _selectedUser = null;

            txtUsername.Clear();
            txtFullName.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = 1;

            lblSelectedUser.Text = "Chưa chọn tài khoản.";
            btnToggleLock.Text = "Khóa / Mở khóa tài khoản";

            UpdateButtons();
        }

        // ================= TẠO TÀI KHOẢN =================

        private async void btnAddUser_Click(
            object? sender,
            EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(fullName))
            {
                ShowWarning("Vui lòng nhập tên đăng nhập và họ tên!");
                return;
            }

            if (!ValidatePassword(password))
            {
                return;
            }

            if (cboRole.SelectedItem == null)
            {
                ShowWarning("Vui lòng chọn vai trò!");
                return;
            }

            var request = new
            {
                Username = username,
                FullName = fullName,
                Password = password,
                Role = cboRole.SelectedItem.ToString()
            };

            await RunActionAsync(async () =>
            {
                await SendApiAsync(
                    HttpMethod.Post,
                    "users",
                    request);

                MessageBox.Show(
                    "Tạo tài khoản thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadUsersAsync();
            });
        }

        // ================= ĐẶT LẠI MẬT KHẨU =================

        private async void btnResetPassword_Click(
            object? sender,
            EventArgs e)
        {
            var selected = _selectedUser;

            if (selected == null)
            {
                ShowWarning("Vui lòng chọn tài khoản trong danh sách!");
                return;
            }

            string newPassword = txtPassword.Text;

            if (!ValidatePassword(newPassword))
            {
                return;
            }

            var confirm = MessageBox.Show(
                $"Đặt lại mật khẩu cho tài khoản \"{selected.Username}\"?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            await RunActionAsync(async () =>
            {
                await SendApiAsync(
                    HttpMethod.Put,
                    $"users/{selected.Id}/password",
                    new { NewPassword = newPassword });

                txtPassword.Clear();

                MessageBox.Show(
                    "Đặt lại mật khẩu thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadUsersAsync(selected.Id);
            });
        }

        // ================= KHÓA / MỞ KHÓA =================

        private async void btnToggleLock_Click(
            object? sender,
            EventArgs e)
        {
            var selected = _selectedUser;

            if (selected == null)
            {
                ShowWarning("Vui lòng chọn tài khoản trong danh sách!");
                return;
            }

            bool newStatus = !selected.IsActive;

            if (!newStatus
                && string.Equals(
                    selected.Username,
                    SessionManager.CurrentUsername,
                    StringComparison.OrdinalIgnoreCase))
            {
                ShowWarning(
                    "Bạn không thể tự khóa tài khoản đang đăng nhập!");
                return;
            }

            string action = newStatus ? "mở khóa" : "khóa";

            var confirm = MessageBox.Show(
                $"Bạn muốn {action} tài khoản \"{selected.Username}\"?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            await RunActionAsync(async () =>
            {
                await SendApiAsync(
                    HttpMethod.Patch,
                    $"users/{selected.Id}/status",
                    new { IsActive = newStatus });

                MessageBox.Show(
                    $"Đã {action} tài khoản!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                await LoadUsersAsync(selected.Id);
            });
        }

        private async void btnLoad_Click(
            object? sender,
            EventArgs e)
        {
            await RunActionAsync(() => LoadUsersAsync());
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
                    "Bạn chưa đăng nhập hoặc phiên làm việc đã kết thúc.");
            }

            using var request = new HttpRequestMessage(method, endpoint);

            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    SessionManager.JwtToken);

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
                    "Phiên đăng nhập không còn hợp lệ hoặc tài khoản đã bị khóa. " +
                    "Vui lòng đăng xuất và đăng nhập lại.");
            }

            if ((int)response.StatusCode == 403)
            {
                throw new InvalidOperationException(
                    "Bạn không có quyền quản trị tài khoản.");
            }

            string message = ReadApiError(content);

            throw new InvalidOperationException(
                $"HTTP {(int)response.StatusCode}: {message}");
        }

        private static string ReadApiError(string content)
        {
            try
            {
                using var document = JsonDocument.Parse(content);
                var root = document.RootElement;

                if (root.ValueKind == JsonValueKind.Object)
                {
                    if (root.TryGetProperty("message", out var message))
                    {
                        return message.GetString()
                            ?? "Yêu cầu không thành công.";
                    }

                    // Lỗi validation do [ApiController] trả về.
                    if (root.TryGetProperty("errors", out var errors)
                        && errors.ValueKind == JsonValueKind.Object)
                    {
                        var messages = new List<string>();

                        foreach (var property in errors.EnumerateObject())
                        {
                            if (property.Value.ValueKind
                                != JsonValueKind.Array)
                            {
                                continue;
                            }

                            foreach (var item in property.Value.EnumerateArray())
                            {
                                if (item.ValueKind == JsonValueKind.String)
                                {
                                    messages.Add(
                                        item.GetString() ?? string.Empty);
                                }
                            }
                        }

                        if (messages.Count > 0)
                        {
                            return string.Join(
                                Environment.NewLine,
                                messages);
                        }
                    }

                    if (root.TryGetProperty("title", out var title))
                    {
                        return title.GetString()
                            ?? "Yêu cầu không thành công.";
                    }
                }
            }
            catch (JsonException)
            {
                // Server có thể trả về nội dung không phải JSON.
            }

            return "Yêu cầu không thành công. Kiểm tra API trên Swagger.";
        }

        // ================= TRẠNG THÁI GIAO DIỆN =================

        private async Task RunActionAsync(Func<Task> action)
        {
            if (_isBusy)
            {
                return;
            }

            _isBusy = true;
            UpdateButtons();

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
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                _isBusy = false;

                if (!IsDisposed)
                {
                    UpdateButtons();
                }
            }
        }

        private void UpdateButtons()
        {
            bool hasSelection = _selectedUser != null;

            btnAddUser.Enabled = !_isBusy;
            btnLoad.Enabled = !_isBusy;

            btnResetPassword.Enabled = !_isBusy && hasSelection;
            btnToggleLock.Enabled = !_isBusy && hasSelection;

            dgvUsers.Enabled = !_isBusy;
            txtUsername.Enabled = !_isBusy;
            txtFullName.Enabled = !_isBusy;
            txtPassword.Enabled = !_isBusy;
            cboRole.Enabled = !_isBusy;

            UseWaitCursor = _isBusy;
        }

        private static bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)
                || password.Length < 6
                || password.Length > 200)
            {
                ShowWarning("Mật khẩu phải có từ 6 đến 200 ký tự!");
                return false;
            }

            return true;
        }

        private static void ShowWarning(string message)
        {
            MessageBox.Show(
                message,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    public class UserManagementDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
