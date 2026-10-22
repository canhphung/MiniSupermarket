# 🛒 MiniSupermarket — Hệ thống quản lý siêu thị mini

> Môn học: Lập trình Ứng dụng .NET Core (229162)  
> Bài thực hành: **buoi-06**  
> Kiến trúc: ASP.NET Core Web API + Windows Forms + SQL Server

## 1. Giới thiệu

MiniSupermarket là ứng dụng quản lý siêu thị mini dùng mô hình **Client–Server**. WinForms gửi HTTP/JSON đến ASP.NET Core API; backend xử lý xác thực, nghiệp vụ và lưu dữ liệu vào SQL Server.

**Phạm vi tại nhánh này:** Chuẩn hóa giao diện WinForms–API, thêm phân trang cho nghiệp vụ danh sách, cải thiện xử lý lỗi/validation và cập nhật cơ sở dữ liệu.

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
| Truy cập dữ liệu | EF Core kết hợp ADO.NET / SQL Server Stored Procedures |
| Chuẩn hóa client | `ApiClientService`, `PagedViewResult` |


## 3. Chức năng theo tiến độ

- **Kế thừa buổi 03–05:** JWT, quản lý dữ liệu, POS, ADO.NET, báo cáo/stored procedures.
- **Phân trang:** bổ sung `PagedResponseDto`, helper `Paging` và `PagedViewResult` để phục vụ danh sách; cập nhật các màn hình liên quan ở WinForms.
- **Chuẩn hóa gọi API:** `ApiClientService` thống nhất `HttpClient`, bearer token, lỗi HTTP 401/403 và thông báo lỗi API.
- **Cấu hình URL:** WinForms đọc biến môi trường `MINISUPERMARKET_API_URL`, có giá trị fallback nếu chưa thiết lập.
- **Cải thiện UX và validation:** sửa các form Login, Product, Customer, Role, User, POS và Quick Report.
- **Migration:** có migration `20261022110648_InitialCreate`; cần kiểm tra và áp dụng schema đúng của nhánh.
- **ADO.NET:** giữ các service truy vấn sản phẩm, giao dịch thanh toán/hủy và báo cáo từ buổi 05.

## 4. Kiến trúc và thư mục

```text
MiniSupermarket/
├── MiniSupermarket.API/
│   ├── Controllers/       # REST endpoints
│   ├── Models/            # Đối tượng miền nghiệp vụ
│   ├── Data/              # EF Core DbContext
│   ├── Migrations/        # Lịch sử thay đổi SQL Server
│   ├── DTOs/              # Checkout, báo cáo, phân trang
│   ├── Services/          # ADO.NET, paging, business rules
│   ├── Database/          # Stored procedures và SQL kiểm tra
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
│   ├── Models/PagedViewResult.cs
│   ├── Services/ApiClientService.cs
│   └── GlobalUsings.cs
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
   git checkout buoi-06
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

**Thiết lập URL API trên Windows PowerShell (mỗi phiên terminal):**

```powershell
$env:MINISUPERMARKET_API_URL = "https://localhost:7203/api/"
```

Giá trị trên chỉ là ví dụ: dùng địa chỉ API thực tế từ `launchSettings.json`/terminal. Nếu dùng HTTPS dev, có thể cần `dotnet dev-certs https --trust`.

**Lưu ý database:** Đây là nhánh có thay đổi migration; nên dùng database thực hành sạch hoặc sao lưu trước. Không tự chạy lẫn migration từ các nhánh khác nhau trên cùng một database nếu lịch sử migration không tương thích.

## 7. API tiêu biểu

| Endpoint | Chức năng |
|---|---|
| `POST /api/auth/login` | Đăng nhập JWT |
| `/api/categories`, `/api/customers`, `/api/roles`, `/api/users`, `/api/products` | Quản lý dữ liệu (một số danh sách có phân trang) |
| `POST /api/orders/checkout` hoặc `POST /api/checkout` | Thanh toán |
| `DELETE /api/orders/{orderId}` hoặc `DELETE /api/checkout/cancel/{orderId}` | Hủy đơn (Admin) |
| `/api/reports/revenue-by-cashier`, `/api/reports/revenue-by-date-range`, `/api/reports/top-products` | Báo cáo |
| `GET /api/ado-products/barcode/{barcode}` | Tra cứu mã vạch |
| `GET /api/ado-products/count` | Đếm sản phẩm |
| `GET /api/ado-products/filter-stock?minStock=50` | Lọc mặt hàng tồn kho thấp |
| `GET /api/ado-products/disconnected-cache` | Snapshot DataAdapter/DataTable |
| `PATCH /api/ado-products/{productId}/stock` | Cập nhật tồn kho |

Đối với endpoint được bảo vệ, gửi header `Authorization: Bearer <token>`. Nhóm endpoint, HTTP method và dữ liệu yêu cầu cụ thể có thể kiểm tra trực tiếp bằng Swagger.

## 8. Lưu ý kỹ thuật

- **API URL có thể cấu hình được**, không nên sửa hardcode từng form. Token được gửi qua `SessionTokenHandler`.
- Trả về `401` khi hết/không hợp lệ phiên đăng nhập, `403` khi không đủ quyền; client đã có xử lý thông báo.
- Chuỗi kết nối trong `appsettings.json` của nhánh chứa tài khoản SQL mẫu: cần thay theo máy và không đưa mật khẩu thật lên GitHub.
- Phân trang không đồng nghĩa mọi endpoint đều phân trang: kiểm tra tham số `page`, `pageSize` ở từng controller.
- Khi test, xác nhận database có các bảng `Customers`, `Orders`, `OrderItems` và các stored procedure trước khi dùng chức năng liên quan.

## 9. Tác giả

- Phùng Đức Cảnh
- Môn học: Lập trình Ứng dụng .NET Core

> README mô tả **trạng thái mã nguồn của chính nhánh buoi-06**, không phải chức năng dự kiến của các buổi sau.
