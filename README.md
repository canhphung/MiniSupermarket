# 🛒 MiniSupermarket — Hệ thống quản lý siêu thị mini

> Môn học: Lập trình Ứng dụng .NET Core (229162)  
> Bài thực hành: **buoi-05**  
> Kiến trúc: ASP.NET Core Web API + Windows Forms + SQL Server

## 1. Giới thiệu

MiniSupermarket là ứng dụng quản lý siêu thị mini dùng mô hình **Client–Server**. WinForms gửi HTTP/JSON đến ASP.NET Core API; backend xử lý xác thực, nghiệp vụ và lưu dữ liệu vào SQL Server.

**Phạm vi tại nhánh này:** Thực hành ADO.NET kết nối/đọc dữ liệu, giao dịch SQL và Stored Procedures cho thanh toán và báo cáo, trên nền chức năng buổi 04.

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
| Truy cập dữ liệu bổ sung | ADO.NET (`Microsoft.Data.SqlClient`, `DataReader`, `DataAdapter`, `DataTable`) |
| Báo cáo | SQL Server Stored Procedures |


## 3. Chức năng theo tiến độ

- **Kế thừa buổi 04:** quản trị, khách hàng, sản phẩm, POS và thống kê.
- **ADO.NET:** `SqlConnectionFactory`, `AdoProductService`, `AdoCheckoutService`, `AdoReportService`.
- **Truy vấn:** sử dụng `SqlCommand` + tham số, `SqlDataReader`, `ExecuteScalar`; ví dụ dữ liệu ngắt kết nối `SqlDataAdapter.Fill` vào `DataTable`.
- **POS theo ADO.NET:** giao dịch dùng `SqlTransaction`, xác thực người bán, kiểm tra giá và số lượng, ghi đơn/chi tiết, cập nhật tồn kho; có nghiệp vụ hủy đơn theo API.
- **Báo cáo:** doanh thu theo thu ngân, theo khoảng ngày, top 5 sản phẩm và danh sách đơn hàng.
- **Stored Procedures:** `sp_GetRevenueByDateRange`, `sp_GetRevenueByCashier`, `sp_GetTop5SellingProducts`.
- **SQL thực hành:** `Database/StoredProcedures.sql`, `Database/VerifyBuoi05.sql`.

## 4. Kiến trúc và thư mục

```text
MiniSupermarket/
├── MiniSupermarket.API/
│   ├── Controllers/       # REST endpoints
│   ├── Models/            # Đối tượng miền nghiệp vụ
│   ├── Data/              # EF Core DbContext
│   ├── Migrations/        # Lịch sử thay đổi SQL Server
│   ├── DTOs/              # Dữ liệu checkout/báo cáo
│   ├── Services/          # ADO.NET và quy tắc nghiệp vụ
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
   git checkout buoi-05
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

**Quan trọng:** Sau migration, kiểm tra các stored procedure đã tồn tại trong database. Tệp `MiniSupermarket.API/Database/StoredProcedures.sql` chứa câu lệnh `CREATE OR ALTER`; thực thi trong đúng database nếu migration chưa tạo chúng. Có thể dùng `VerifyBuoi05.sql` để đối chiếu dữ liệu/báo cáo.

## 7. API tiêu biểu

| Endpoint | Chức năng |
|---|---|
| `POST /api/orders/checkout` | Thanh toán ADO.NET |
| `DELETE /api/orders/{orderId}` | Hủy đơn (Admin) |
| `GET /api/reports/revenue-by-cashier` | Doanh thu theo thu ngân |
| `GET /api/reports/revenue-by-date-range?fromDate=...&toDate=...` | Doanh thu khoảng ngày |
| `GET /api/reports/top-products` | Top sản phẩm |
| `GET /api/ado-products` | Danh sách sản phẩm theo ADO.NET |
| `GET /api/ado-products/disconnected-cache` | Ví dụ `DataAdapter`/`DataTable` |
| `GET /api/ado-products/category-counts` | Thống kê theo danh mục |

Đối với endpoint được bảo vệ, gửi header `Authorization: Bearer <token>`. Nhóm endpoint, HTTP method và dữ liệu yêu cầu cụ thể có thể kiểm tra trực tiếp bằng Swagger.

## 8. Lưu ý kỹ thuật

- **Không phải toàn bộ dữ liệu đã chuyển sang ADO.NET:** các controller quản lý cơ bản vẫn có EF Core.
- Mọi SQL giá trị động trong service ADO.NET nên được truyền qua tham số; không nối trực tiếp đầu vào người dùng vào SQL.
- Việc hủy đơn và phục hồi tồn kho cần luôn thực hiện trong transaction.
- Các stored procedure truy vấn dựa vào đúng tên bảng và cột đã migrate; nếu SQL Server báo thiếu bảng/procedure, kiểm tra database và migration trước.

## 9. Tác giả

- Phùng Đức Cảnh
- Môn học: Lập trình Ứng dụng .NET Core

> README mô tả **trạng thái mã nguồn của chính nhánh buoi-05**, không phải chức năng dự kiến của các buổi sau.
