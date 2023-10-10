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
    Gender CHAR(1),
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
CREATE TABLE ImportProduct (
    ImportID int NOT NULL,
    ProductID int NOT NULL,
    SupplierID int NOT NULL,
    Quantity int NOT NULL,
    ImportDate date NOT NULL,
    PRIMARY KEY (ImportID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);


