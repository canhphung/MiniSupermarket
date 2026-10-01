using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    // DbContext đại diện cho phiên làm việc với cơ sở dữ liệu SQL Server
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) : base(options) { }

        // Khai báo các bảng dữ liệu ánh xạ từ Model
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        // Cấu hình dữ liệu mồi ban đầu (Data Seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nạp sẵn 5 danh mục ban đầu vào SQL Server ngay khi tạo bảng
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Đồ uống",
                    Description = "Nước ngọt, nước suối, trà, cà phê, nước tăng lực"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Thực phẩm ăn liền",
                    Description = "Cơm, mì, sandwich, burger, bánh mì và đồ ăn nóng"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Bánh kẹo & Đồ ăn vặt",
                    Description = "Snack, khoai tây chiên, bánh quy, kẹo và chocolate"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Sữa & sản phẩm từ sữa",
                    Description = "Sữa tươi, sữa chua, phô mai và đồ uống từ sữa"
                },
                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Thực phẩm khô & đóng gói",
                    Description = "Mì gói, ngũ cốc, đồ hộp và thực phẩm đóng gói"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                // Category 1: Đồ uống
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
                    ProductName = "Nước suối Aquafina 500ml",
                    Price = 7000m,
                    StockQuantity = 60,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 3,
                    Barcode = "893000000003",
                    ProductName = "Trà xanh Không Độ 455ml",
                    Price = 11000m,
                    StockQuantity = 45,
                    CategoryId = 1
                },

                // Category 2: Thực phẩm ăn liền
                new Product
                {
                    ProductId = 4,
                    Barcode = "893000000004",
                    ProductName = "Cơm gà sốt tiêu đen",
                    Price = 35000m,
                    StockQuantity = 20,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 5,
                    Barcode = "893000000005",
                    ProductName = "Sandwich trứng jambon",
                    Price = 25000m,
                    StockQuantity = 25,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 6,
                    Barcode = "893000000006",
                    ProductName = "Bánh mì xúc xích",
                    Price = 22000m,
                    StockQuantity = 30,
                    CategoryId = 2
                },

                // Category 3: Bánh kẹo & Đồ ăn vặt
                new Product
                {
                    ProductId = 7,
                    Barcode = "893000000007",
                    ProductName = "Snack khoai tây Oishi",
                    Price = 10000m,
                    StockQuantity = 40,
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 8,
                    Barcode = "893000000008",
                    ProductName = "Bánh Oreo 133g",
                    Price = 18000m,
                    StockQuantity = 35,
                    CategoryId = 3
                },
                new Product
                {
                    ProductId = 9,
                    Barcode = "893000000009",
                    ProductName = "Chocolate KitKat",
                    Price = 15000m,
                    StockQuantity = 30,
                    CategoryId = 3
                },

                // Category 4: Sữa & sản phẩm từ sữa
                new Product
                {
                    ProductId = 10,
                    Barcode = "893000000010",
                    ProductName = "Sữa tươi Vinamilk 180ml",
                    Price = 9000m,
                    StockQuantity = 50,
                    CategoryId = 4
                },
                new Product
                {
                    ProductId = 11,
                    Barcode = "893000000011",
                    ProductName = "Sữa chua Vinamilk",
                    Price = 7000m,
                    StockQuantity = 40,
                    CategoryId = 4
                },
                new Product
                {
                    ProductId = 12,
                    Barcode = "893000000012",
                    ProductName = "Phô mai Con Bò Cười",
                    Price = 32000m,
                    StockQuantity = 25,
                    CategoryId = 4
                },

                // Category 5: Thực phẩm khô & đóng gói
                new Product
                {
                    ProductId = 13,
                    Barcode = "893000000013",
                    ProductName = "Mì Hảo Hảo tôm chua cay",
                    Price = 4500m,
                    StockQuantity = 100,
                    CategoryId = 5
                },
                new Product
                {
                    ProductId = 14,
                    Barcode = "893000000014",
                    ProductName = "Cháo ăn liền thịt bằm",
                    Price = 8000m,
                    StockQuantity = 60,
                    CategoryId = 5
                },
                new Product
                {
                    ProductId = 15,
                    Barcode = "893000000015",
                    ProductName = "Cá hộp sốt cà",
                    Price = 22000m,
                    StockQuantity = 35,
                    CategoryId = 5
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn A",
                    PhoneNumber = "0901122334",
                    MembershipRank = "Vàng",
                    RewardPoints = 150
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị B",
                    PhoneNumber = "0918877665",
                    MembershipRank = "Bạc",
                    RewardPoints = 50
                },
                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn C",
                    PhoneNumber = "0983344556",
                    MembershipRank = "Chuẩn",
                    RewardPoints = 10
                }
            );
        }
    }
}
