using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "RewardPoints",
                value: 153);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "RewardPoints",
                value: 67);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "RewardPoints",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "RewardPoints",
                value: 204);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "RewardPoints",
                value: 86);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "RewardPoints",
                value: 39);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "RewardPoints",
                value: 317);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "RewardPoints",
                value: 93);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "RewardPoints",
                value: 21);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "RewardPoints",
                value: 191);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "RewardPoints",
                value: 108);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "RewardPoints",
                value: 38);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "RewardPoints",
                value: 258);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "RewardPoints",
                value: 102);

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashReceived", "CashierId", "ChangeAmount", "CreatedAtUtc", "CustomerId", "TotalAmount" },
                values: new object[] { -1015, 50000m, 7, 40500m, new DateTime(2026, 10, 13, 4, 0, 0, 0, DateTimeKind.Utc), 15, 9500m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashReceived", "CashierId", "ChangeAmount", "CreatedAtUtc", "CustomerId", "RewardPointsAwarded", "TotalAmount" },
                values: new object[,]
                {
                    { -1014, 150000m, 6, 27000m, new DateTime(2026, 10, 13, 3, 15, 0, 0, DateTimeKind.Utc), 14, 12, 123000m },
                    { -1013, 100000m, 5, 18000m, new DateTime(2026, 10, 13, 2, 30, 0, 0, DateTimeKind.Utc), 13, 8, 82000m },
                    { -1012, 100000m, 4, 20000m, new DateTime(2026, 10, 13, 1, 45, 0, 0, DateTimeKind.Utc), 12, 8, 80000m },
                    { -1011, 400000m, 3, 13000m, new DateTime(2026, 10, 13, 1, 0, 0, 0, DateTimeKind.Utc), 11, 38, 387000m },
                    { -1010, 150000m, 7, 35000m, new DateTime(2026, 10, 14, 4, 0, 0, 0, DateTimeKind.Utc), 10, 11, 115000m },
                    { -1009, 100000m, 6, 34000m, new DateTime(2026, 10, 14, 3, 15, 0, 0, DateTimeKind.Utc), 9, 6, 66000m },
                    { -1008, 300000m, 5, 13000m, new DateTime(2026, 10, 14, 2, 30, 0, 0, DateTimeKind.Utc), 8, 28, 287000m },
                    { -1007, 200000m, 4, 25000m, new DateTime(2026, 10, 14, 1, 45, 0, 0, DateTimeKind.Utc), 7, 17, 175000m },
                    { -1006, 150000m, 3, 10000m, new DateTime(2026, 10, 14, 1, 0, 0, 0, DateTimeKind.Utc), 6, 14, 140000m },
                    { -1005, 100000m, 7, 31500m, new DateTime(2026, 10, 15, 4, 0, 0, 0, DateTimeKind.Utc), 5, 6, 68500m },
                    { -1004, 50000m, 6, 4000m, new DateTime(2026, 10, 15, 3, 15, 0, 0, DateTimeKind.Utc), 4, 4, 46000m },
                    { -1003, 150000m, 5, 47000m, new DateTime(2026, 10, 15, 2, 30, 0, 0, DateTimeKind.Utc), 3, 10, 103000m },
                    { -1002, 200000m, 4, 30000m, new DateTime(2026, 10, 15, 1, 45, 0, 0, DateTimeKind.Utc), 2, 17, 170000m },
                    { -1001, 50000m, 3, 11000m, new DateTime(2026, 10, 15, 1, 0, 0, 0, DateTimeKind.Utc), 1, 3, 39000m }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "StockQuantity",
                value: 47);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "StockQuantity",
                value: 16);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "StockQuantity",
                value: 33);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "StockQuantity",
                value: 47);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "StockQuantity",
                value: 96);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "StockQuantity",
                value: 28);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "StockQuantity",
                value: 22);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "StockQuantity",
                value: 16);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "StockQuantity",
                value: 38);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "StockQuantity",
                value: 27);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                column: "StockQuantity",
                value: 16);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                column: "StockQuantity",
                value: 23);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                column: "StockQuantity",
                value: 32);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                column: "StockQuantity",
                value: 26);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                column: "StockQuantity",
                value: 98);

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "ProductId", "ProductName", "Quantity", "TotalPrice", "UnitPrice" },
                values: new object[,]
                {
                    { -2030, -1015, 5, "Mì Hảo Hảo tôm chua cay", 1, 4500m, 4500m },
                    { -2029, -1015, 15, "Bút bi Thiên Long", 1, 5000m, 5000m },
                    { -2028, -1014, 4, "Sữa tươi Vinamilk 180ml", 1, 9000m, 9000m },
                    { -2027, -1014, 14, "Khăn ướt em bé Bobby", 3, 114000m, 38000m },
                    { -2026, -1013, 3, "Bánh Oreo 133g", 1, 18000m, 18000m },
                    { -2025, -1013, 13, "Nước rửa chén Sunlight 750g", 2, 64000m, 32000m },
                    { -2024, -1012, 2, "Cơm gà sốt tiêu đen", 1, 35000m, 35000m },
                    { -2023, -1012, 12, "Hộp đựng thực phẩm 1L", 1, 45000m, 45000m },
                    { -2022, -1011, 1, "Coca-Cola lon 330ml", 1, 12000m, 12000m },
                    { -2021, -1011, 11, "Dầu gội Clear 630g", 3, 375000m, 125000m },
                    { -2020, -1010, 15, "Bút bi Thiên Long", 1, 5000m, 5000m },
                    { -2019, -1010, 10, "Xúc xích đông lạnh 500g", 2, 110000m, 55000m },
                    { -2018, -1009, 14, "Khăn ướt em bé Bobby", 1, 38000m, 38000m },
                    { -2017, -1009, 9, "Nước mắm Nam Ngư 500ml", 1, 28000m, 28000m },
                    { -2016, -1008, 13, "Nước rửa chén Sunlight 750g", 1, 32000m, 32000m },
                    { -2015, -1008, 8, "Thịt ba rọi heo 500g", 3, 255000m, 85000m },
                    { -2014, -1007, 12, "Hộp đựng thực phẩm 1L", 1, 45000m, 45000m },
                    { -2013, -1007, 7, "Táo Fuji 1kg", 2, 130000m, 65000m },
                    { -2012, -1006, 11, "Dầu gội Clear 630g", 1, 125000m, 125000m },
                    { -2011, -1006, 6, "Rau cải xanh 500g", 1, 15000m, 15000m },
                    { -2010, -1005, 10, "Xúc xích đông lạnh 500g", 1, 55000m, 55000m },
                    { -2009, -1005, 5, "Mì Hảo Hảo tôm chua cay", 3, 13500m, 4500m },
                    { -2008, -1004, 9, "Nước mắm Nam Ngư 500ml", 1, 28000m, 28000m },
                    { -2007, -1004, 4, "Sữa tươi Vinamilk 180ml", 2, 18000m, 9000m },
                    { -2006, -1003, 8, "Thịt ba rọi heo 500g", 1, 85000m, 85000m },
                    { -2005, -1003, 3, "Bánh Oreo 133g", 1, 18000m, 18000m },
                    { -2004, -1002, 7, "Táo Fuji 1kg", 1, 65000m, 65000m },
                    { -2003, -1002, 2, "Cơm gà sốt tiêu đen", 3, 105000m, 35000m },
                    { -2002, -1001, 6, "Rau cải xanh 500g", 1, 15000m, 15000m },
                    { -2001, -1001, 1, "Coca-Cola lon 330ml", 2, 24000m, 12000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2030);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2029);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2028);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2027);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2026);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2025);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2024);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2023);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2022);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2021);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2020);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2019);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2018);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2017);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2016);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2015);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2014);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2013);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2012);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2011);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2010);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2009);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2008);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2007);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2006);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2005);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2004);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2003);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2002);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: -2001);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1015);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1014);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1013);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1012);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1011);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1010);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1009);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1008);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1007);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1006);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1005);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1004);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1003);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1002);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: -1001);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "RewardPoints",
                value: 150);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "RewardPoints",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "RewardPoints",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "RewardPoints",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "RewardPoints",
                value: 80);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "RewardPoints",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "RewardPoints",
                value: 300);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "RewardPoints",
                value: 65);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "RewardPoints",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "RewardPoints",
                value: 180);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "RewardPoints",
                value: 70);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "RewardPoints",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "RewardPoints",
                value: 250);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "RewardPoints",
                value: 90);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "StockQuantity",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "StockQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "StockQuantity",
                value: 35);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "StockQuantity",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "StockQuantity",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "StockQuantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "StockQuantity",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "StockQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "StockQuantity",
                value: 40);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "StockQuantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                column: "StockQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                column: "StockQuantity",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                column: "StockQuantity",
                value: 35);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                column: "StockQuantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                column: "StockQuantity",
                value: 100);
        }
    }
}
