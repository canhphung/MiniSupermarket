-- Chọn đúng database của ConnectionStrings:DefaultConnection trước khi chạy.
SELECT MigrationId FROM dbo.__EFMigrationsHistory ORDER BY MigrationId;
SELECT COL_LENGTH('dbo.Orders', 'RewardPointsAwarded') AS RewardColumnLength;
SELECT name FROM sys.procedures WHERE name IN
    ('sp_GetRevenueByCashier', 'sp_GetRevenueByDateRange', 'sp_GetTop5SellingProducts');
EXEC dbo.sp_GetRevenueByCashier;
-- Ví dụ ngày Việt Nam 15/10/2026: UTC từ 14/10 17:00 đến trước 15/10 17:00.
EXEC dbo.sp_GetRevenueByDateRange @FromDate = '2026-10-14T17:00:00', @ToDate = '2026-10-15T17:00:00';
EXEC dbo.sp_GetTop5SellingProducts;
SELECT TOP (20) OrderId, CreatedAtUtc, CashierId, CustomerId, TotalAmount, RewardPointsAwarded
FROM dbo.Orders ORDER BY OrderId DESC;
SELECT TOP (50) * FROM dbo.OrderItems ORDER BY OrderItemId DESC;
SELECT ProductId, ProductName, StockQuantity FROM dbo.Products ORDER BY ProductId;
SELECT CustomerId, CustomerName, RewardPoints FROM dbo.Customers ORDER BY CustomerId;
