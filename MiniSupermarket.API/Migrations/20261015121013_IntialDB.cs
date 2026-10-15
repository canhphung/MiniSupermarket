using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class IntialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RewardPoints = table.Column<int>(type: "int", nullable: false),
                    MembershipRank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RewardPointsAwarded = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CashierId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CashReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChangeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Đồ uống", "Nước ngọt, nước suối, trà, cà phê, nước tăng lực" },
                    { 2, "Thực phẩm ăn liền", "Cơm, mì, sandwich, burger, bánh mì và đồ ăn nóng" },
                    { 3, "Bánh kẹo & Đồ ăn vặt", "Snack, bánh quy, kẹo và chocolate" },
                    { 4, "Sữa & sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai và đồ uống từ sữa" },
                    { 5, "Thực phẩm khô & đóng gói", "Mì gói, ngũ cốc, đồ hộp và thực phẩm đóng gói" },
                    { 6, "Rau củ", "Rau xanh, củ, quả và thực phẩm tươi" },
                    { 7, "Trái cây", "Trái cây trong nước và nhập khẩu" },
                    { 8, "Thịt & Hải sản", "Thịt heo, bò, gà, cá và hải sản" },
                    { 9, "Gia vị", "Nước mắm, nước tương, muối, đường và gia vị nấu ăn" },
                    { 10, "Đông lạnh", "Thực phẩm đông lạnh và chế biến sẵn" },
                    { 11, "Chăm sóc cá nhân", "Dầu gội, sữa tắm, kem đánh răng và sản phẩm cá nhân" },
                    { 12, "Đồ gia dụng", "Dụng cụ và vật dụng sử dụng trong gia đình" },
                    { 13, "Vệ sinh nhà cửa", "Nước lau sàn, nước rửa chén và chất tẩy rửa" },
                    { 14, "Mẹ & Bé", "Sản phẩm dành cho mẹ và trẻ nhỏ" },
                    { 15, "Văn phòng phẩm", "Bút, vở, giấy và dụng cụ học tập" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, "12 Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh", "Nguyễn Văn A", "Vàng", "0901122334", 150 },
                    { 2, "45 Võ Văn Tần, Quận 3, TP. Hồ Chí Minh", "Trần Thị B", "Bạc", "0918877665", 50 },
                    { 3, "128 Xô Viết Nghệ Tĩnh, Bình Thạnh, TP. Hồ Chí Minh", "Lê Văn C", "Chuẩn", "0983344556", 10 },
                    { 4, "76 Nguyễn Thị Thập, Quận 7, TP. Hồ Chí Minh", "Phạm Thị D", "Vàng", "0904455667", 200 },
                    { 5, "215 Cộng Hòa, Tân Bình, TP. Hồ Chí Minh", "Hoàng Văn E", "Bạc", "0935566778", 80 },
                    { 6, "93 Quang Trung, Gò Vấp, TP. Hồ Chí Minh", "Võ Thị F", "Chuẩn", "0976677889", 25 },
                    { 7, "150 Võ Nguyên Giáp, TP. Thủ Đức", "Đặng Văn G", "Vàng", "0967788990", 300 },
                    { 8, "68 Sư Vạn Hạnh, Quận 10, TP. Hồ Chí Minh", "Bùi Thị H", "Bạc", "0948899001", 65 },
                    { 9, "320 Hà Huy Giáp, Quận 12, TP. Hồ Chí Minh", "Đỗ Văn I", "Chuẩn", "0929900112", 15 },
                    { 10, "84 Phan Xích Long, Phú Nhuận, TP. Hồ Chí Minh", "Ngô Thị K", "Vàng", "0911011223", 180 },
                    { 11, "25 Lê Văn Sỹ, Quận 3, TP. Hồ Chí Minh", "Đinh Văn L", "Bạc", "0902233445", 70 },
                    { 12, "110 Nguyễn Trãi, Quận 5, TP. Hồ Chí Minh", "Mai Thị M", "Chuẩn", "0913344556", 30 },
                    { 13, "45 Lê Đức Thọ, Gò Vấp, TP. Hồ Chí Minh", "Phan Văn N", "Vàng", "0934455667", 250 },
                    { 14, "78 Âu Cơ, Tân Phú, TP. Hồ Chí Minh", "Trương Thị O", "Bạc", "0945566778", 90 },
                    { 15, "36 Phạm Văn Đồng, TP. Thủ Đức", "Huỳnh Văn P", "Chuẩn", "0956677889", 40 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "IsActive", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Nguyễn Quản Trị", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Admin", "admin01" },
                    { 2, "Trần Giám Đốc", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Admin", "admin02" },
                    { 3, "Lê Thu Ngân", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Cashier", "cashier01" },
                    { 4, "Phạm Bán Hàng", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Cashier", "cashier02" },
                    { 5, "Hoàng Thu Ngân", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Cashier", "cashier03" },
                    { 6, "Vũ Thị Quầy", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Cashier", "cashier04" },
                    { 7, "Đỗ Bán Lẻ", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Cashier", "cashier05" },
                    { 8, "Ngô Quản Kho", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Warehouse", "ware01" },
                    { 9, "Bùi Kiểm Kê", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Warehouse", "ware02" },
                    { 10, "Dương Thủ Kho", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Warehouse", "ware03" },
                    { 11, "Lý Nhập Hàng", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Warehouse", "ware04" },
                    { 12, "Đặng Hỗ Trợ", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Admin", "admin_backup" },
                    { 13, "Hồ Ca Chiều", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Cashier", "cashier06" },
                    { 14, "Trương Vận Chuyển", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Warehouse", "ware05" },
                    { 15, "Mai Giám Sát", true, "AQAAAAIAAYagAAAAEO5wcfkHQBsfJN/oCIQQ+D4YGuVsAvjV4cbdU6ARhty5S7eVD+oIAH8YWGOj7QOfuA==", "Admin", "supervisor" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "893000000001", 1, 12000m, "Coca-Cola lon 330ml", 50 },
                    { 2, "893000000002", 2, 35000m, "Cơm gà sốt tiêu đen", 20 },
                    { 3, "893000000003", 3, 18000m, "Bánh Oreo 133g", 35 },
                    { 4, "893000000004", 4, 9000m, "Sữa tươi Vinamilk 180ml", 50 },
                    { 5, "893000000005", 5, 4500m, "Mì Hảo Hảo tôm chua cay", 100 },
                    { 6, "893000000006", 6, 15000m, "Rau cải xanh 500g", 30 },
                    { 7, "893000000007", 7, 65000m, "Táo Fuji 1kg", 25 },
                    { 8, "893000000008", 8, 85000m, "Thịt ba rọi heo 500g", 20 },
                    { 9, "893000000009", 9, 28000m, "Nước mắm Nam Ngư 500ml", 40 },
                    { 10, "893000000010", 10, 55000m, "Xúc xích đông lạnh 500g", 30 },
                    { 11, "893000000011", 11, 125000m, "Dầu gội Clear 630g", 20 },
                    { 12, "893000000012", 12, 45000m, "Hộp đựng thực phẩm 1L", 25 },
                    { 13, "893000000013", 13, 32000m, "Nước rửa chén Sunlight 750g", 35 },
                    { 14, "893000000014", 14, 38000m, "Khăn ướt em bé Bobby", 30 },
                    { 15, "893000000015", 15, 5000m, "Bút bi Thiên Long", 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashierId",
                table: "Orders",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedAtUtc",
                table: "Orders",
                column: "CreatedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Barcode",
                table: "Products",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
