-- trigger
 -- tinh luong khi nhap cac cot can thiet
CREATE TRIGGER trg_UpdateTotal ON [CONVENIENCESTORE].[dbo].[Salaries]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON; -- tắt số cộ trả về để cải thiện hiệu xuất trong trigger

    UPDATE s
    SET s.Total = i.HoursWorked * i.HourlyRate - i.Deductions + i.Bonus
    FROM [CONVENIENCESTORE].[dbo].[Salaries] s
    INNER JOIN inserted i ON s.ID = i.ID;
END;
-- cập nhật số lượng tổng cua sản phẩm khi thêm 1 lô sản phẩm
CREATE TRIGGER updateTotalQuantity
ON ProductBatches
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE p
    SET totalQuantity = totalQuantity + i.Quantity
    FROM Products p
    INNER JOIN inserted i ON p.ProductID = i.ProductID;
END;
 -- cập nhật totalAmount trong bảng invoices khi thêm 1 invoiceDetails
CREATE TRIGGER UpdateTotalAmount
ON InvoiceDetails
AFTER INSERT
AS
BEGIN
    UPDATE Invoices
    SET TotalAmount = TotalAmount + (i.Quantity * i.Price)
    FROM Invoices
    INNER JOIN inserted i ON Invoices.InvoiceID = i.InvoiceID;
END;
 -- cập nhật điểm thưởng cho khách hàng sau khi thêm 1 hóa đơn
 CREATE TRIGGER CalculateRewardPoints
ON Invoices
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE Customers
    SET RewardPoints = i.TotalAmount * 0.01
    FROM Customers c
    JOIN inserted i ON c.customerID = i.CustomerID;
END;