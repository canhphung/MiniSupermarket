using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {

        // Khởi tạo HttpClient trỏ đến địa chỉ của Web API Backend
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7203/api/")
        };

        public FormLogin()
        {
            InitializeComponent();
        }

        // Sự kiện khi người dùng bấm nút Đăng nhập
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text;

            // Kiểm tra ràng buộc cơ bản phía Client
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Đóng gói dữ liệu gửi lên endpoint POST /api/auth/login
                var loginData = new { Username = username, Password = password };
                var response = await _client.PostAsJsonAsync("auth/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    // Đọc kết quả đăng nhập.
                    var jsonString = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(jsonString);

                    // Lưu thông tin phiên.
                    SessionManager.JwtToken =
                        doc.RootElement.GetProperty("token").GetString()
                        ?? string.Empty;

                    SessionManager.CurrentRole =
                        doc.RootElement.GetProperty("role").GetString()
                        ?? string.Empty;

                    SessionManager.CurrentUsername = username;

                    // Mở giao diện chính.
                    this.Hide();

                    DialogResult result;

                    using (var mainForm = new FormMainShell())
                    {
                        result = mainForm.ShowDialog();
                    }

                    if (result == DialogResult.Retry)
                    {
                        // Người dùng đăng xuất: trở về form đăng nhập hiện tại.
                        txtPass.Clear();
                        this.Show();
                        this.Activate();
                        txtUser.Focus();
                    }
                    else
                    {
                        // Người dùng đóng cửa sổ chính: kết thúc ứng dụng.
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối đến Server: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
