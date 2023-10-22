USE [CONVENIENCESTORE];

CREATE PROCEDURE dbo.AddInvoices
AS
BEGIN
    DECLARE @Counter INT
    SET @Counter = 1

    WHILE @Counter <= 10000
    BEGIN
        INSERT INTO  [dbo].[Invoices](
            [CustomerID],
            [EmployeeID],
            [PaymentID],
            [ShippingID],
            [createdDate],
            [TotalAmount],
            [Status]
        )
        VALUES (
            FLOOR(RAND()*(100-1+1)+1), -- Random CustomerID từ 1 đến 100
            FLOOR(RAND()*(100-1+1)+1), -- Random EmployeeID từ 1 đến 100 (Bạn cần điều chỉnh nếu có quy tắc khác)
            FLOOR(RAND()*(3-1+1)+1),   -- Random PaymentID từ 1 đến 3
            FLOOR(RAND()*(10-1+1)+1),  -- Random ShippingID từ 1 đến 10
            DATEADD(DAY, -CONVERT(INT, RAND()*365), GETDATE()), -- Random createdDate từ 1/1/2010 đến ngày hôm nay
           FLOOR(RAND()*(10000000-10000+1)+1), -- Random totalAmount  từ 10 củ đến 10k
            CASE WHEN RAND() > 0.5 THEN N'Đã giao' ELSE N'Chưa giao' END -- Random Status
        )

        SET @Counter = @Counter + 1
    END
END
EXEC dbo.AddInvoices


-- add invoicedetail
CREATE PROCEDURE GenerateRandomInvoices
AS
BEGIN
    DECLARE @Counter INT
    SET @Counter = 1

    WHILE @Counter <= 1000
    BEGIN
        INSERT INTO [CONVENIENCESTORE].[dbo].[InvoiceDetails] ([InvoiceID], [ProductID], [Quantity], [Price])
        VALUES (
            FLOOR(RAND() * 5000) + 1, -- InvoiceID từ 1 đến 10000
            FLOOR(RAND() * 109) + 1,    -- ProductID từ 1 đến 110
            FLOOR(RAND() * 50) + 1,     -- Quantity từ 1 đến 50
            FLOOR(RAND() * 99999000) + 1000 -- Price từ 1000 đến 100000000
        )
        SET @Counter = @Counter + 1
    END
END

EXEC GenerateRandomInvoices
-- lấy 10 lastest invoices
CREATE PROCEDURE GetLatestInvoices
AS
BEGIN
    SELECT TOP 10 *
    FROM [CONVENIENCESTORE].[dbo].[Invoices]
    ORDER BY [createdDate] DESC;
END;
EXEC GetLatestInvoices;
-- lấy 10 phú ông phù bà
CREATE PROCEDURE GetTopCustomers
AS
BEGIN
    SELECT TOP 10 
        c.customerID, 
        c.FirstName, 
        c.LastName, 
        SUM(i.TotalAmount) as TotalPaid
    FROM 
        Customers c
    JOIN 
        Invoices i ON c.customerID = i.CustomerID
    WHERE 
        MONTH(i.createdDate) = MONTH(GETDATE()) AND YEAR(i.createdDate) = YEAR(GETDATE())
    GROUP BY 
        c.customerID, c.FirstName, c.LastName
    ORDER BY 
        TotalPaid DESC;
END;
EXEC GetTopCustomers;
-- Lấy 05 sản phẩm bán chạy nhất tháng
	-- old version
CREATE PROCEDURE GetTop5Products AS
BEGIN
    SELECT TOP 5
        P.ProductID, 
        P.name, 
		P.sellPrice,
        SUM(ID.Quantity) as TotalQuantitySold
    FROM Products p
    INNER JOIN ProductBatches pb ON p.ProductID = pb.ProductID
    INNER JOIN InvoiceDetails id ON pb.BatchID = id.productBatchID
    INNER JOIN Invoices i ON id.InvoiceID = i.InvoiceID
    WHERE YEAR(i.createdDate) = YEAR(GETDATE())
    AND MONTH(i.createdDate) = MONTH(GETDATE())
    GROUP BY p.ProductID, p.name, P.sellPrice
    ORDER BY TotalQuantitySold DESC;
END;
EXEC GetTop5Products;
-- Lấy doanh thu theo tháng
CREATE PROCEDURE GetRevenueByMonth
AS
BEGIN
    SELECT 
        SUM(TotalAmount) AS DoanhThu
    FROM 
        Invoices
    WHERE 
        YEAR(createdDate) = YEAR(GETDATE())
    GROUP BY 
        MONTH(createdDate)
    ORDER BY 
        MONTH(createdDate);
END;
EXEC GetRevenueByMonth;
-- Tính lợi nhuận theo tháng
UPDATE productBatches
SET importPrice = 150000

CREATE PROCEDURE CalculateMonthlyProfit
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SUM(i.TotalAmount ) - SUM(id.Quantity * (pb.importPrice)) - SUM(s.Total) as LoiNhuan
    FROM
        Invoices i
    JOIN
        InvoiceDetails id ON i.InvoiceID = id.InvoiceID
    JOIN
		ProductBatches PB ON PB.BatchID = ID.productBatchID
    LEFT JOIN
        Salaries s ON i.EmployeeID = s.EmployeeID
    WHERE
        YEAR(i.createdDate) >=YEAR(GETDATE()) 
    GROUP BY 
        MONTH(createdDate)
    ORDER BY 
        MONTH(createdDate);
END;

EXEC CalculateMonthlyProfit;


ALTER TABLE [CONVENIENCESTORE].[dbo].[Invoices]
DROP COLUMN createdDate;

ALTER TABLE [CONVENIENCESTORE].[dbo].[Invoices]
ADD createdDate DATETIME;

UPDATE Products
SET importPrice = 100000
WHERE ProductID >=3 and ProductID<115;

UPDATE Invoices
SET TotalAmount = 9999999
WHERE InvoiceID >=1 and InvoiceID<9999;

UPDATE Invoices
SET [deliveryCost] = 0
WHERE InvoiceID % 2 !=0;

DELETE FROM Invoices
WHERE InvoiceID >10000;

-- stored procedure
CREATE PROCEDURE CalculateOnlineOfflinePurchases
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SUM(CASE WHEN [deliveryCost] = 0 THEN 1 ELSE 0 END) AS OnlinePurchases,
        COUNT(*) - SUM(CASE WHEN [deliveryCost] = 0 THEN 1 ELSE 0 END) AS OfflinePurchases
    FROM
        Invoices
    WHERE
        YEAR(createdDate) = YEAR(GETDATE()) 
    GROUP BY
        MONTH(createdDate)
    ORDER BY
        MONTH(createdDate);
END

EXEC CalculateOnlineOfflinePurchases
-- tìm tổng số đơn của tháng này
CREATE PROCEDURE GetInvoiceCountForCurrentMonth
AS
BEGIN
    SELECT COUNT(*) AS InvoiceCount
    FROM [CONVENIENCESTORE].[dbo].[Invoices]
    WHERE MONTH(createdDate) = MONTH(GETDATE()) AND YEAR(createdDate) = YEAR(GETDATE());
END;
EXEC GetInvoiceCountForCurrentMonth;
-- tìm tổng số đơn của tháng trước đo
CREATE PROCEDURE GetTotalOrdersLastMonth
AS
BEGIN
    DECLARE @CurrentMonth INT, @CurrentYear INT, @LastMonth INT, @LastYear INT;
    SET @CurrentMonth = MONTH(GETDATE());
    SET @CurrentYear = YEAR(GETDATE());

    IF @CurrentMonth = 1
    BEGIN
        SET @LastMonth = 12;
        SET @LastYear = @CurrentYear - 1;
    END
    ELSE
    BEGIN
        SET @LastMonth = @CurrentMonth - 1;
        SET @LastYear = @CurrentYear;
    END

    SELECT COUNT(*) AS TotalOrders
    FROM Invoices
    WHERE MONTH(createdDate) = @LastMonth AND YEAR(createdDate) = @LastYear;
END;


EXEC GetTotalOrdersLastMonth;
-- 
CREATE PROCEDURE AddProductBatches
    @n INT
AS
BEGIN
    DECLARE @i INT = 1
    
    WHILE @i <= @n
    BEGIN
        DECLARE @productID INT = ROUND(RAND() * (114 - 3) + 3, 0)
        DECLARE @manufactureDate DATE = DATEADD(day, -ROUND(RAND() * 100, 0), GETDATE())
        DECLARE @expiryDate DATE = DATEADD(day, ROUND(RAND() * 365, 0), @manufactureDate)
        DECLARE @quantity INT = ROUND(RAND() * (1000 - 10) + 10, 0)
        DECLARE @importPrice DECIMAL(18, 2) = ROUND(RAND() * (1000000 - 1000) + 1000, 2)

        INSERT INTO ProductBatches (productID, manufactureDate, expiryDate, quantity, importPrice)
        VALUES (@productID, @manufactureDate, @expiryDate, @quantity, @importPrice)

        SET @i = @i + 1
    END
END
EXEC AddProductBatches @n = 500;
