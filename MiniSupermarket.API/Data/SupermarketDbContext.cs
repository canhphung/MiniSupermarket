using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    // DbContext đại diện cho phiên làm việc với cơ sở dữ liệu SQL Server
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options)
            : base(options) { }

        // Khai báo các bảng dữ liệu ánh xạ từ Model
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Cấu hình dữ liệu mồi ban đầu
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // 15 CATEGORY
            // =========================
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Đồ uống", Description = "Nước ngọt, nước suối, trà, cà phê, nước tăng lực" },
                new Category { CategoryId = 2, CategoryName = "Thực phẩm ăn liền", Description = "Cơm, mì, sandwich, burger, bánh mì và đồ ăn nóng" },
                new Category { CategoryId = 3, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack, bánh quy, kẹo và chocolate" },
                new Category { CategoryId = 4, CategoryName = "Sữa & sản phẩm từ sữa", Description = "Sữa tươi, sữa chua, phô mai và đồ uống từ sữa" },
                new Category { CategoryId = 5, CategoryName = "Thực phẩm khô & đóng gói", Description = "Mì gói, ngũ cốc, đồ hộp và thực phẩm đóng gói" },
                new Category { CategoryId = 6, CategoryName = "Rau củ", Description = "Rau xanh, củ, quả và thực phẩm tươi" },
                new Category { CategoryId = 7, CategoryName = "Trái cây", Description = "Trái cây trong nước và nhập khẩu" },
                new Category { CategoryId = 8, CategoryName = "Thịt & Hải sản", Description = "Thịt heo, bò, gà, cá và hải sản" },
                new Category { CategoryId = 9, CategoryName = "Gia vị", Description = "Nước mắm, nước tương, muối, đường và gia vị nấu ăn" },
                new Category { CategoryId = 10, CategoryName = "Đông lạnh", Description = "Thực phẩm đông lạnh và chế biến sẵn" },
                new Category { CategoryId = 11, CategoryName = "Chăm sóc cá nhân", Description = "Dầu gội, sữa tắm, kem đánh răng và sản phẩm cá nhân" },
                new Category { CategoryId = 12, CategoryName = "Đồ gia dụng", Description = "Dụng cụ và vật dụng sử dụng trong gia đình" },
                new Category { CategoryId = 13, CategoryName = "Vệ sinh nhà cửa", Description = "Nước lau sàn, nước rửa chén và chất tẩy rửa" },
                new Category { CategoryId = 14, CategoryName = "Mẹ & Bé", Description = "Sản phẩm dành cho mẹ và trẻ nhỏ" },
                new Category { CategoryId = 15, CategoryName = "Văn phòng phẩm", Description = "Bút, vở, giấy và dụng cụ học tập" }
            );

            // =========================
            // 15 PRODUCT
            // =========================
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Barcode = "893000000001",
                    ProductName = "Coca-Cola lon 330ml",
                    Price = 12000m,
                    StockQuantity = 50,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Barcode = "893000000002",
                    ProductName = "Cơm gà sốt tiêu đen",
                    Price = 35000m,
                    StockQuantity = 20,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Barcode = "893000000003",
                    ProductName = "Bánh Oreo 133g",
                    Price = 18000m,
                    StockQuantity = 35,
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 4,
                    Barcode = "893000000004",
                    ProductName = "Sữa tươi Vinamilk 180ml",
                    Price = 9000m,
                    StockQuantity = 50,
                    CategoryId = 4
                },
                new Product
                {
                    ProductId = 5,
                    Barcode = "893000000005",
                    ProductName = "Mì Hảo Hảo tôm chua cay",
                    Price = 4500m,
                    StockQuantity = 100,
                    CategoryId = 5
                },
                new Product
                {
                    ProductId = 6,
                    Barcode = "893000000006",
                    ProductName = "Rau cải xanh 500g",
                    Price = 15000m,
                    StockQuantity = 30,
                    CategoryId = 6
                },
                new Product
                {
                    ProductId = 7,
                    Barcode = "893000000007",
                    ProductName = "Táo Fuji 1kg",
                    Price = 65000m,
                    StockQuantity = 25,
                    CategoryId = 7
                },
                new Product
                {
                    ProductId = 8,
                    Barcode = "893000000008",
                    ProductName = "Thịt ba rọi heo 500g",
                    Price = 85000m,
                    StockQuantity = 20,
                    CategoryId = 8
                },
                new Product
                {
                    ProductId = 9,
                    Barcode = "893000000009",
                    ProductName = "Nước mắm Nam Ngư 500ml",
                    Price = 28000m,
                    StockQuantity = 40,
                    CategoryId = 9
                },
                new Product
                {
                    ProductId = 10,
                    Barcode = "893000000010",
                    ProductName = "Xúc xích đông lạnh 500g",
                    Price = 55000m,
                    StockQuantity = 30,
                    CategoryId = 10
                },
                new Product
                {
                    ProductId = 11,
                    Barcode = "893000000011",
                    ProductName = "Dầu gội Clear 630g",
                    Price = 125000m,
                    StockQuantity = 20,
                    CategoryId = 11
                },
                new Product
                {
                    ProductId = 12,
                    Barcode = "893000000012",
                    ProductName = "Hộp đựng thực phẩm 1L",
                    Price = 45000m,
                    StockQuantity = 25,
                    CategoryId = 12
                },
                new Product
                {
                    ProductId = 13,
                    Barcode = "893000000013",
                    ProductName = "Nước rửa chén Sunlight 750g",
                    Price = 32000m,
                    StockQuantity = 35,
                    CategoryId = 13
                },
                new Product
                {
                    ProductId = 14,
                    Barcode = "893000000014",
                    ProductName = "Khăn ướt em bé Bobby",
                    Price = 38000m,
                    StockQuantity = 30,
                    CategoryId = 14
                },
                new Product
                {
                    ProductId = 15,
                    Barcode = "893000000015",
                    ProductName = "Bút bi Thiên Long",
                    Price = 5000m,
                    StockQuantity = 100,
                    CategoryId = 15
                }
            );

            // =========================
            // 15 CUSTOMER
            // =========================
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn A",
                    PhoneNumber = "0901122334",
                    MembershipRank = "Vàng",
                    RewardPoints = 150,
                    Address = "12 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị B",
                    PhoneNumber = "0918877665",
                    MembershipRank = "Bạc",
                    RewardPoints = 50,
                    Address = "45 Võ Văn Tần, Quận 3, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn C",
                    PhoneNumber = "0983344556",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 10,
                    Address = "128 Xô Viết Nghệ Tĩnh, Bình Thạnh, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 4,
                    CustomerName = "Phạm Thị D",
                    PhoneNumber = "0904455667",
                    MembershipRank = "Vàng",
                    RewardPoints = 200,
                    Address = "76 Nguyễn Thị Thập, Quận 7, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 5,
                    CustomerName = "Hoàng Văn E",
                    PhoneNumber = "0935566778",
                    MembershipRank = "Bạc",
                    RewardPoints = 80,
                    Address = "215 Cộng Hòa, Tân Bình, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 6,
                    CustomerName = "Võ Thị F",
                    PhoneNumber = "0976677889",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 25,
                    Address = "93 Quang Trung, Gò Vấp, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 7,
                    CustomerName = "Đặng Văn G",
                    PhoneNumber = "0967788990",
                    MembershipRank = "Vàng",
                    RewardPoints = 300,
                    Address = "150 Võ Nguyên Giáp, TP. Thủ Đức"
                },
                new Customer
                {
                    CustomerId = 8,
                    CustomerName = "Bùi Thị H",
                    PhoneNumber = "0948899001",
                    MembershipRank = "Bạc",
                    RewardPoints = 65,
                    Address = "68 Sư Vạn Hạnh, Quận 10, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 9,
                    CustomerName = "Đỗ Văn I",
                    PhoneNumber = "0929900112",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 15,
                    Address = "320 Hà Huy Giáp, Quận 12, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 10,
                    CustomerName = "Ngô Thị K",
                    PhoneNumber = "0911011223",
                    MembershipRank = "Vàng",
                    RewardPoints = 180,
                    Address = "84 Phan Xích Long, Phú Nhuận, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 11,
                    CustomerName = "Đinh Văn L",
                    PhoneNumber = "0902233445",
                    MembershipRank = "Bạc",
                    RewardPoints = 70,
                    Address = "25 Lê Văn Sỹ, Quận 3, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 12,
                    CustomerName = "Mai Thị M",
                    PhoneNumber = "0913344556",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 30,
                    Address = "110 Nguyễn Trãi, Quận 5, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 13,
                    CustomerName = "Phan Văn N",
                    PhoneNumber = "0934455667",
                    MembershipRank = "Vàng",
                    RewardPoints = 250,
                    Address = "45 Lê Đức Thọ, Gò Vấp, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 14,
                    CustomerName = "Trương Thị O",
                    PhoneNumber = "0945566778",
                    MembershipRank = "Bạc",
                    RewardPoints = 90,
                    Address = "78 Âu Cơ, Tân Phú, TP. Hồ Chí Minh"
                },
                new Customer
                {
                    CustomerId = 15,
                    CustomerName = "Huỳnh Văn P",
                    PhoneNumber = "0956677889",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 40,
                    Address = "36 Phạm Văn Đồng, TP. Thủ Đức"
                }
            );
        }
    }
}