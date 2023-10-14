CREATE DATABASE CONVENIENCESTORE;
USE CONVENIENCESTORE;

CREATE TABLE Customers (
    customerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    Email VARCHAR(255),
    Phone VARCHAR(15),
    birthday VARCHAR(255),
    RewardPoints INT,
    Rank VARCHAR(50),
	gender bit,
	userID int
);
CREATE TABLE Employees (
    employeeID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    Position VARCHAR(100),
    DateHired DATE,
    BirthDate DATE,
    CitizenID VARCHAR(20),
    Gender bit,
    Email VARCHAR(255),
    PhoneNumber VARCHAR(15)
);
CREATE TABLE Salaries (
    ID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT,
    MonthYear DATE,
    HoursWorked INT,
    HourlyRate DECIMAL(10, 2),
    Bonus DECIMAL(10, 2),
    Deductions DECIMAL(10, 2),
    Total DECIMAL(10, 2),
	FOREIGN KEY (EMPLOYEEID) REFERENCES Employees(employeeID)
);
CREATE TABLE ShippingLocations (
    LocationID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    Street VARCHAR(255),
    ward VARCHAR(100),
    district VARCHAR(100),
    city VARCHAR(100),
	isDefault bit,
	FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);
CREATE TABLE	Categories (
    CategoryID INT PRIMARY KEY,
    ParentCategoryID INT,
    CategoryName VARCHAR(255) NOT NULL,
    Description TEXT,
    FOREIGN KEY (ParentCategoryID) REFERENCES Categories(CategoryID)
);
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName VARCHAR(255) NOT NULL,
    ContactName VARCHAR(255),
    Address VARCHAR(255),
    City VARCHAR(255),
    PostalCode VARCHAR(20),
    Country VARCHAR(100),
    Phone VARCHAR(20)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    description TEXT,
   
	sellPrice DECIMAL(10, 2),
    totalQuantity INT NOT NULL,
    categoryID INT,
    imageUrl VARCHAR(255),
    videoUrl VARCHAR(255),
    discount DECIMAL(10, 2),
    discountPrice AS (sellPrice - (sellPrice * discount / 100)),
    bestsellerFlag BIT DEFAULT 0,
    homeFlag BIT DEFAULT 0,
    active BIT DEFAULT 1,
    dateAdded TIMESTAMP ,
	SupplierID int,
	FOREIGN KEY (categoryID) REFERENCES Categories(CategoryID),
	 FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);

CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    MethodName VARCHAR(100),
    Details VARCHAR(255)
);

CREATE TABLE Shipping (
    ShippingID INT PRIMARY KEY IDENTITY(1,1),
    MethodName VARCHAR(100),
    Provider VARCHAR(100),
    Cost DECIMAL(10, 2),
    EstimatedDeliveryTime INT
);

CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    EmployeeID INT,
    PaymentID INT,
    ShippingID INT,
    createdDate TIMESTAMP ,
    TotalAmount DECIMAL(10, 2),
    Status VARCHAR(50),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
    FOREIGN KEY (ShippingID) REFERENCES Shipping(ShippingID)
);
CREATE TABLE InvoiceDetails (
    InvoiceDetailID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID INT,
    ProductID INT,
    Quantity INT,
    Price DECIMAL(10, 2),
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE ProductBatches (
    BatchID int PRIMARY KEY IDENTITY(1,1),
    ProductID int NOT NULL,
    BatchNumber varchar(255),
    ManufactureDate date,
    ExpiryDate date,
	importPrice DECIMAL(10, 2) NOT NULL,
    Quantity int,
	barcode varchar(50),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
CREATE TABLE Orders (
    OrderID int PRIMARY KEY,
    SupplierID int NOT NULL,
    OrderDate date NOT NULL,
	totalAmount money
);

CREATE TABLE OrderDetails (
    OrderDetailID int PRIMARY KEY,
    OrderID int NOT NULL,
    ProductID int NOT NULL,
    Quantity int NOT NULL,
    UnitPrice money
);
CREATE TABLE ROLES(
ROLEID INT PRIMARY KEY IDENTITY(1,1),
ROLENAME NVARCHAR(50),
DESCRIPTION NVARCHAR(255)
);
CREATE TABLE ACCOUNTS(
ACCOUNTID INT PRIMARY KEY IDENTITY(1,1),
PHONE VARCHAR(12),
EMAIL NVARCHAR(50),
PASSWORD NVARCHAR(50),
SALT NCHAR(10),
FULLNAME NVARCHAR(150),
ROLEID INT,
LASTLOGIN DATETIME,
CREATEDATE DATETIME,
FOREIGN KEY (ROLEID) REFERENCES ROLES(ROLEID)
);

USE [CONVENIENCESTORE]

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
            DATEADD(DAY, -CONVERT(INT, RAND()*3652), GETDATE()), -- Random createdDate từ 1/1/2010 đến ngày hôm nay
            0,  -- Chưa có TotalAmount, bạn có thể điều chỉnh nếu cần
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



ALTER TABLE [CONVENIENCESTORE].[dbo].[Invoices]
DROP COLUMN createdDate;
ALTER TABLE [CONVENIENCESTORE].[dbo].[Invoices]
ADD createdDate DATETIME;


