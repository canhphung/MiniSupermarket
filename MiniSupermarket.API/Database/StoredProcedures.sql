CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByDateRange
    @FromDate datetime2 = NULL,
    @ToDate datetime2 = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- UTC, đầu ngày <= thời điểm < đầu ngày kế tiếp.
    SELECT u.Username AS CashierUsername, COUNT_BIG(*) AS TotalOrders,
           COALESCE(SUM(o.TotalAmount), 0) AS TotalRevenue,
           COALESCE(SUM(CAST(o.RewardPointsAwarded AS bigint)), 0) AS TotalPointsAwarded
    FROM dbo.Orders o JOIN dbo.Users u ON u.Id = o.CashierId
    WHERE (@FromDate IS NULL OR o.CreatedAtUtc >= @FromDate)
      AND (@ToDate IS NULL OR o.CreatedAtUtc < @ToDate)
    GROUP BY u.Id, u.Username
    ORDER BY TotalRevenue DESC, u.Username;
END
GO
CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByCashier
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.sp_GetRevenueByDateRange;
END
GO
CREATE OR ALTER PROCEDURE dbo.sp_GetTop5SellingProducts
    @FromDate datetime2 = NULL,
    @ToDate datetime2 = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (5) i.ProductId, p.ProductName,
           SUM(CAST(i.Quantity AS bigint)) AS TotalQuantity,
           SUM(i.TotalPrice) AS TotalRevenue
    FROM dbo.OrderItems i JOIN dbo.Orders o ON o.OrderId = i.OrderId
    JOIN dbo.Products p ON p.ProductId = i.ProductId
    WHERE (@FromDate IS NULL OR o.CreatedAtUtc >= @FromDate)
      AND (@ToDate IS NULL OR o.CreatedAtUtc < @ToDate)
    GROUP BY i.ProductId, p.ProductName
    ORDER BY TotalQuantity DESC, i.ProductId;
END
GO
