# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

> **Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)
> **Nhánh:** `buoi-02`
> **Nội dung:** Xây dựng ASP.NET Core Web API và ứng dụng WinForms Client

---

## 🏗️ 1. Kiến trúc hệ thống

Dự án **MiniSupermarket** được xây dựng theo mô hình **Client - Server**, tách biệt phần Backend và ứng dụng Desktop Client.

```text
┌─────────────────────────────┐
│ MiniSupermarket.WinForms    │
│        Desktop Client       │
│                             │
│ - Đăng nhập                 │
│ - Quản lý danh mục          │
│ - Quản lý vai trò           │
└──────────────┬──────────────┘
               │
               │ HTTP / JSON
               ▼
┌─────────────────────────────┐
│    MiniSupermarket.API      │
│    ASP.NET Core Web API     │
│                             │
│ - Categories API            │
│ - Roles API                 │
│ - Authentication API        │
└─────────────────────────────┘
```

### Backend – `MiniSupermarket.API`

ASP.NET Core Web API chịu trách nhiệm:

* Xử lý các yêu cầu HTTP từ Client.
* Quản lý dữ liệu của hệ thống.
* Cung cấp các RESTful API.
* Xử lý các chức năng liên quan đến danh mục, vai trò và đăng nhập.

### Frontend – `MiniSupermarket.WinForms`

Ứng dụng Windows Forms đóng vai trò Desktop Client.

WinForms sử dụng `HttpClient` và `System.Net.Http.Json` để giao tiếp với Web API, nhận dữ liệu JSON và hiển thị lên giao diện.

---

## 🛠️ 2. Công nghệ sử dụng

| Thành phần                | Công nghệ                                  |
| ------------------------- | ------------------------------------------ |
| Ngôn ngữ                  | C#                                         |
| Framework                 | .NET 8.0                                   |
| Backend                   | ASP.NET Core Web API                       |
| Frontend                  | Windows Forms                              |
| API                       | RESTful API                                |
| Giao tiếp Client – Server | `HttpClient`                               |
| Xử lý JSON                | `System.Net.Http.Json`, `System.Text.Json` |
| Truy vấn dữ liệu          | LINQ                                       |
| Kiểm thử API              | Swagger UI                                 |

---

## 📂 3. Cấu trúc Solution

```text
MiniSupermarketSystem/
│
├── MiniSupermarket.API/
│   │
│   ├── Controllers/
│   │   ├── CategoriesController.cs
│   │   ├── RolesController.cs
│   │   └── ...
│   │
│   ├── Models/
│   │   ├── Category.cs
│   │   ├── Role.cs
│   │   └── ...
│   │
│   └── Program.cs
│
└── MiniSupermarket.WinForms/
    │
    ├── FormLogin.cs
    ├── FormLogin.Designer.cs
    │
    ├── FormCategoryManagement.cs
    ├── FormCategoryManagement.Designer.cs
    │
    ├── FormRoleManagement.cs
    ├── FormRoleManagement.Designer.cs
    │
    └── Program.cs
```

---

# 📚 4. Tiến độ thực hành

## ✅ Buổi 1 – Category Management

Buổi 1 tập trung xây dựng chức năng **quản lý danh mục sản phẩm** và kết nối WinForms với ASP.NET Core Web API.

### Backend

Xây dựng API quản lý danh mục hỗ trợ các thao tác:

* Lấy danh sách danh mục.
* Tìm kiếm danh mục.
* Thêm danh mục.
* Cập nhật danh mục.
* Xóa danh mục.

### WinForms

Xây dựng `FormCategoryManagement` để:

* Hiển thị danh sách danh mục trên `DataGridView`.
* Thêm danh mục.
* Sửa danh mục.
* Xóa danh mục.
* Tìm kiếm danh mục.
* Đồng bộ dữ liệu với Web API thông qua `HttpClient`.

---

## ✅ Buổi 2 – Role Management & Login

Buổi 2 mở rộng hệ thống với chức năng **quản lý vai trò người dùng** và **đăng nhập hệ thống**.

### 👥 Quản lý vai trò

Xây dựng `FormRoleManagement` để quản lý các Role trong hệ thống.

Các chức năng chính:

* Hiển thị danh sách Role.
* Thêm Role.
* Chỉnh sửa Role.
* Xóa Role.
* Chọn dữ liệu trực tiếp từ `DataGridView`.
* Gửi request từ WinForms tới `/api/roles`.

Ví dụ luồng xử lý:

```text
FormRoleManagement
        │
        │ GET /api/roles
        ▼
MiniSupermarket.API
        │
        │ JSON
        ▼
DataGridView
```

---

### 🔐 Đăng nhập hệ thống

Bổ sung `FormLogin` làm màn hình đăng nhập cho ứng dụng WinForms.

Giao diện bao gồm:

* Ô nhập tài khoản `txtUser`.
* Ô nhập mật khẩu `txtPass`.
* Mật khẩu được ẩn bằng `UseSystemPasswordChar`.
* Nút `btnLogin` – **Đăng nhập hệ thống**.

Luồng đăng nhập:

```text
Người dùng
    │
    ▼
FormLogin
    │
    │ Username + Password
    ▼
ASP.NET Core Web API
    │
    ├── Sai → Thông báo lỗi
    │
    └── Đúng
          │
          ▼
     Mở giao diện quản lý
```

---

## 🔄 5. Luồng giao tiếp Client – Server

Ví dụ khi WinForms cần lấy danh sách Role:

```text
[FormRoleManagement]
        │
        │ HttpClient
        │ GET /api/roles
        ▼
[ASP.NET Core Web API]
        │
        │ Xử lý request
        ▼
[RolesController]
        │
        │ JSON Response
        ▼
[FormRoleManagement]
        │
        ▼
[DataGridView]
```

Nhờ kiến trúc này, phần giao diện WinForms không truy cập dữ liệu trực tiếp mà giao tiếp với Backend thông qua REST API.

---

## 🚀 6. Chạy dự án

### Bước 1 – Clone repository

```bash
git clone https://github.com/canhphung/MiniSupermarket.git
```

Chuyển sang nhánh Buổi 2:

```bash
cd MiniSupermarket
git checkout buoi-02
```

### Bước 2 – Chạy Web API

Mở project:

```text
MiniSupermarket.API
```

Khởi chạy API và kiểm tra các endpoint bằng **Swagger UI**.

### Bước 3 – Kiểm tra địa chỉ API

Đảm bảo `BaseAddress` trong WinForms trùng với địa chỉ API đang chạy.

Ví dụ:

```csharp
private static readonly HttpClient _client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7123/api/")
};
```

### Bước 4 – Chạy WinForms

Khởi chạy project:

```text
MiniSupermarket.WinForms
```

Ứng dụng sẽ giao tiếp với ASP.NET Core Web API thông qua `HttpClient`.

---

## 📌 7. Các chức năng hiện tại

| Chức năng            | Backend API | WinForms |
| -------------------- | :---------: | :------: |
| Quản lý danh mục     |      ✅      |     ✅    |
| Tìm kiếm danh mục    |      ✅      |     ✅    |
| Quản lý Role         |      ✅      |     ✅    |
| Đăng nhập            |      ✅      |     ✅    |
| Giao tiếp HTTP/JSON  |      ✅      |     ✅    |
| Swagger kiểm thử API |      ✅      |     —    |

---

## 👤 8. Tác giả

> **Họ và tên:** Phùng Đức Cảnh
> **Mã sinh viên:** 2124110137
> **Lớp học phần:** CCQ2411D
