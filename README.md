# 🛒 MiniSupermarket — Hệ thống quản lý siêu thị mini

> Môn học: Lập trình Ứng dụng .NET Core (229162)  
> Bài thực hành: **buoi-04**  
> Kiến trúc: ASP.NET Core Web API + Windows Forms + SQL Server

## 1. Giới thiệu

MiniSupermarket là ứng dụng quản lý siêu thị mini dùng mô hình **Client–Server**. WinForms gửi HTTP/JSON đến ASP.NET Core API; backend xử lý xác thực, nghiệp vụ và lưu dữ liệu vào SQL Server.

**Phạm vi tại nhánh này:** Mở rộng hệ thống thành ứng dụng bán hàng có giao diện quản trị, quản lý sản phẩm/tài khoản, điểm bán (POS) và thống kê nhanh.

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

- **Kế thừa buổi 03:** danh mục, vai trò, khách hàng, đăng nhập và JWT.
- **Quản lý sản phẩm:** màn hình `FormProductManagement`, API `ProductsController`.
- **Quản lý người dùng:** `FormUserManagement`, `UsersController`, vai trò và trạng thái tài khoản.
- **Giao diện chính:** `FormMainShell` với menu theo vai trò; dùng `UiTheme`.
- **POS:** `FormPOS`, tra cứu/quét mã sản phẩm, giỏ hàng và thanh toán tiền mặt.
- **Thanh toán:** `POST /api/orders/checkout` lưu `Order`/`OrderItem`, trừ tồn kho trong một giao dịch EF Core, kiểm tra tồn kho/đơn giá và tính tiền thừa.
- **Thống kê nhanh:** `FormQuickReport` và API doanh thu theo ngày dành cho Admin.

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
│   ├── FormMainShell.cs
│   ├── FormProductManagement.cs
│   ├── FormUserManagement.cs
│   ├── FormPOS.cs
│   ├── FormQuickReport.cs
│   └── UI/UiTheme.cs
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
   git checkout buoi-04
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

Sau khi tạo database, kiểm tra dữ liệu người dùng, vai trò và sản phẩm trước khi thử POS. Không sử dụng thông tin đăng nhập demo của buổi 03 nếu `AuthController` trong nhánh này đã xác thực từ bảng `Users`.

## 7. API tiêu biểu

| Endpoint | Chức năng / quyền |
|---|---|
| `POST /api/auth/login` | Đăng nhập |
| `/api/categories`, `/api/roles`, `/api/customers` | Quản lý dữ liệu cơ bản |
| `/api/products` | Quản lý sản phẩm |
| `/api/users` | Quản lý tài khoản |
| `POST /api/orders/checkout` | Thanh toán; Admin/Cashier |
| `GET /api/reports/daily?date=YYYY-MM-DD` | Doanh thu ngày; Admin |

Đối với endpoint được bảo vệ, gửi header `Authorization: Bearer <token>`. Nhóm endpoint, HTTP method và dữ liệu yêu cầu cụ thể có thể kiểm tra trực tiếp bằng Swagger.

## 8. Lưu ý kỹ thuật

- Tiền mặt được kiểm tra đủ trước khi hoàn tất thanh toán; sản phẩm được trừ kho trong transaction.
- Phân quyền chính: **Admin**, **Cashier**, **Warehouse**; quyền cụ thể nằm trong `[Authorize]` và giao diện.
- Báo cáo theo ngày quy đổi múi giờ Việt Nam (UTC+7).
- Đây là phiên bản sử dụng EF Core cho nghiệp vụ thanh toán; ADO.NET/Stored Procedures được bổ sung ở buổi 05.

## 9. Tác giả

- Phùng Đức Cảnh
- Môn học: Lập trình Ứng dụng .NET Core

> README mô tả **trạng thái mã nguồn của chính nhánh buoi-04**, không phải chức năng dự kiến của các buổi sau.
