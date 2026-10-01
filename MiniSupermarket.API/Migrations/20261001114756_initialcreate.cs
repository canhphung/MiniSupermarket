using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Đồ uống", "Nước ngọt, nước suối, trà, cà phê, nước tăng lực" },
                    { 2, "Thực phẩm ăn liền", "Cơm, mì, sandwich, burger, bánh mì và đồ ăn nóng" },
                    { 3, "Bánh kẹo & Đồ ăn vặt", "Snack, khoai tây chiên, bánh quy, kẹo và chocolate" },
                    { 4, "Sữa & sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai và đồ uống từ sữa" },
                    { 5, "Thực phẩm khô & đóng gói", "Mì gói, ngũ cốc, đồ hộp và thực phẩm đóng gói" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, null, "Nguyễn Văn A", "Vàng", "0901122334", 150 },
                    { 2, null, "Trần Thị B", "Bạc", "0918877665", 50 },
                    { 3, null, "Lê Văn C", "Chuẩn", "0983344556", 10 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "893000000001", 1, 12000m, "Coca-Cola lon 330ml", 50 },
                    { 2, "893000000002", 1, 7000m, "Nước suối Aquafina 500ml", 60 },
                    { 3, "893000000003", 1, 11000m, "Trà xanh Không Độ 455ml", 45 },
                    { 4, "893000000004", 2, 35000m, "Cơm gà sốt tiêu đen", 20 },
                    { 5, "893000000005", 2, 25000m, "Sandwich trứng jambon", 25 },
                    { 6, "893000000006", 2, 22000m, "Bánh mì xúc xích", 30 },
                    { 7, "893000000007", 3, 10000m, "Snack khoai tây Oishi", 40 },
                    { 8, "893000000008", 3, 18000m, "Bánh Oreo 133g", 35 },
                    { 9, "893000000009", 3, 15000m, "Chocolate KitKat", 30 },
                    { 10, "893000000010", 4, 9000m, "Sữa tươi Vinamilk 180ml", 50 },
                    { 11, "893000000011", 4, 7000m, "Sữa chua Vinamilk", 40 },
                    { 12, "893000000012", 4, 32000m, "Phô mai Con Bò Cười", 25 },
                    { 13, "893000000013", 5, 4500m, "Mì Hảo Hảo tôm chua cay", 100 },
                    { 14, "893000000014", 5, 8000m, "Cháo ăn liền thịt bằm", 60 },
                    { 15, "893000000015", 5, 22000m, "Cá hộp sốt cà", 35 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
