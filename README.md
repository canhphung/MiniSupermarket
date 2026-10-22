# 🛒 MiniSupermarket — Hệ thống quản lý siêu thị mini

> Môn học: Lập trình Ứng dụng .NET Core (229162)  
> Bài thực hành: **buoi-03**  
> Kiến trúc: ASP.NET Core Web API + Windows Forms + SQL Server

## 1. Giới thiệu

MiniSupermarket là ứng dụng quản lý siêu thị mini dùng mô hình **Client–Server**. WinForms gửi HTTP/JSON đến ASP.NET Core API; backend xử lý xác thực, nghiệp vụ và lưu dữ liệu vào SQL Server.

**Phạm vi tại nhánh này:** Tiếp tục danh mục/vai trò/đăng nhập từ buổi trước; bổ sung quản lý khách hàng và lưu dữ liệu qua EF Core.

## 2. Công nghệ

| Thành phần | Công nghệ |
|---|---|
| Ngôn ngữ/runtime | C#, .NET 8 |
| Backend | ASP.NET Core Web API |
| Desktop client | Windows Forms (.NET 8, Windows) |
| Cơ sở dữ liệu | Microsoft SQL Server |
| ORM | Entity Framework Core + LINQ |
| Bảo mật | JWT Bearer, phân quyền theo vai trò |
| Tài liệu/kiểm thử API | Swagger UI (môi trường Development) |


## 3. Chức năng theo tiến độ

- **Danh mục:** xem danh sách, tìm kiếm và CRUD qua API/WinForms.
- **Vai trò:** quản lý Role qua API/WinForms.
- **Đăng nhập:** `POST /api/auth/login`, sinh JWT cho tài khoản demo.
- **Khách hàng:** xem danh sách, xem chi tiết, tìm theo tên hoặc số điện thoại, thêm/sửa/xóa. Khi thêm/sửa kiểm tra số điện thoại trùng.
- **Dữ liệu:** sử dụng `SupermarketDbContext`, entity và EF Core migration.

## 4. Kiến trúc và thư mục

```text
MiniSupermarket/
├── MiniSupermarket.API/
│   ├── Controllers/       # REST endpoints
│   ├── Models/            # Đối tượng miền nghiệp vụ
│   ├── Data/              # EF Core DbContext
│   ├── Migrations/        # Lịch sử thay đổi SQL Server
│   ├── Program.cs         # DI, xác thực, Swagger, middleware
│   └── appsettings.json   # Cấu hình; thay connection string theo máy
├── MiniSupermarket.WinForms/
│   ├── FormLogin.cs
│   ├── FormCategoryManagement.cs
│   ├── FormRoleManagement.cs
│   ├── FormCustomerManagement.cs
│   └── Program.cs
└── MiniSupermarket.sln
```

```text
Windows Forms ── HttpClient + JWT ──> ASP.NET Core Web API
                                            │
                                            ▼
                                       SQL Server
```

## 5. Yêu cầu môi trường

- Windows 10/11, Visual Studio 2022 hỗ trợ .NET Desktop Development và ASP.NET/Web hoặc .NET SDK 8.
- SQL Server / SQL Server Express và công cụ quản lý SQL (ví dụ SSMS).
- Kết nối được SQL Server từ máy chạy API; phần WinForms cần chạy trên Windows.

## 6. Cài đặt và chạy

1. Clone repository và chuyển đúng nhánh:

   ```powershell
   git clone https://github.com/canhphung/MiniSupermarket.git
   cd MiniSupermarket
   git checkout buoi-03
   ```

2. Mở `MiniSupermarket.sln`. Sửa `ConnectionStrings:DefaultConnection` trong `MiniSupermarket.API/appsettings.json` cho phù hợp SQL Server của máy. Không mặc định dùng chung tài khoản `sa` hoặc mật khẩu được commit vào repo; ưu tiên User Secrets / biến môi trường.

3. Restore, build và cập nhật migration:

   ```powershell
   dotnet restore
   dotnet build
   dotnet ef database update --project MiniSupermarket.API --startup-project MiniSupermarket.API
   ```

   Nếu chưa có `dotnet ef`, cài `dotnet tool install --global dotnet-ef --version 8.*` (hoặc `dotnet tool update --global dotnet-ef --version 8.*`). Chỉ chạy migration trên database đúng nhánh, nên sao lưu trước nếu có dữ liệu quan trọng.

4. Chạy API:

   ```powershell
   dotnet run --project MiniSupermarket.API
   ```

   Xem URL HTTP/HTTPS thực tế trên terminal hoặc `Properties/launchSettings.json`. Trong môi trường Development, mở `<API_BASE_URL>/swagger` để xem endpoint.

5. Kiểm tra địa chỉ API trong mã WinForms; phải trùng host, cổng và scheme thực sự đang chạy. Sau đó mở/chạy project `MiniSupermarket.WinForms` trong Visual Studio.

**Đăng nhập demo của nhánh:** `admin / 123456` và `cashier / 123456` được hardcode trong `AuthController` (chỉ dùng cho mục đích thực hành; cần thay bằng xác thực từ database nếu triển khai thực tế).

## 7. API tiêu biểu

| Endpoint | Chức năng |
|---|---|
| `POST /api/auth/login` | Đăng nhập demo, lấy JWT |
| `GET /api/customers` | Danh sách khách hàng |
| `GET /api/customers/{id}` | Chi tiết |
| `GET /api/customers/search?keyword=...` | Tìm kiếm |
| `POST /api/customers` | Thêm khách hàng |
| `PUT /api/customers/{id}` | Sửa |
| `DELETE /api/customers/{id}` | Xóa |
| `/api/categories`, `/api/roles` | Nghiệp vụ danh mục và vai trò |

Đối với endpoint được bảo vệ, gửi header `Authorization: Bearer <token>`. Nhóm endpoint, HTTP method và dữ liệu yêu cầu cụ thể có thể kiểm tra trực tiếp bằng Swagger.

## 8. Lưu ý kỹ thuật

- Mọi action của `CustomersController` yêu cầu JWT (`[Authorize]`).
- JWT secret mặc định chỉ thích hợp cho thực hành; không commit secret dùng trong production.
- Buổi này **chưa có** POS/thanh toán và quản lý sản phẩm hoàn chỉnh như buổi 04.

## 9. Tác giả

- Phùng Đức Cảnh
- Môn học: Lập trình Ứng dụng .NET Core

> README mô tả **trạng thái mã nguồn của chính nhánh buoi-03**, không phải chức năng dự kiến của các buổi sau.
