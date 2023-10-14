using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client_Home.Migrations.Conveniencestore
{
    /// <inheritdoc />
    public partial class addActiveAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ParentCategoryID = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__19093A2B10713FBC", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK__Categorie__Paren__412EB0B6",
                        column: x => x.ParentCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    birthday = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RewardPoints = table.Column<int>(type: "int", nullable: true),
                    Rank = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    gender = table.Column<bool>(type: "bit", nullable: true),
                    userID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__B611CB9D98268D25", x => x.customerID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Position = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DateHired = table.Column<DateTime>(type: "date", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    CitizenID = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__C134C9A11A764CDB", x => x.employeeID);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__D3B9D30C1E2B64A9", x => x.OrderDetailID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    totalAmount = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__C3905BAFD0BC4AE7", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Details = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__9B556A589797EF5E", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    ROLEID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ROLENAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ROLES__006568E9599A1705", x => x.ROLEID);
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    ShippingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Provider = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    EstimatedDeliveryTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shipping__5FACD460BC0762F5", x => x.ShippingID);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ContactName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Supplier__4BE66694069AD674", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingLocations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    Street = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ward = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    district = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    city = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    isDefault = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shipping__E7FEA4771FD4FB63", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK__ShippingL__Custo__3E52440B",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "customerID");
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    MonthYear = table.Column<DateTime>(type: "date", nullable: true),
                    HoursWorked = table.Column<int>(type: "int", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Bonus = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Deductions = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Salaries__3214EC27DC986AA3", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Salaries__Employ__3B75D760",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "employeeID");
                });

            migrationBuilder.CreateTable(
                name: "ACCOUNTS",
                columns: table => new
                {
                    ACCOUNTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PHONE = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SALT = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    FULLNAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ROLEID = table.Column<int>(type: "int", nullable: true),
                    LASTLOGIN = table.Column<DateTime>(type: "datetime", nullable: true),
                    CREATEDATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ACCOUNTS__1F64FDFCD3B51490", x => x.ACCOUNTID);
                    table.ForeignKey(
                        name: "FK__ACCOUNTS__ROLEID__74AE54BC",
                        column: x => x.ROLEID,
                        principalTable: "ROLES",
                        principalColumn: "ROLEID");
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    PaymentID = table.Column<int>(type: "int", nullable: true),
                    ShippingID = table.Column<int>(type: "int", nullable: true),
                    createdDate = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Invoices__D796AAD58A15F0AA", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK__Invoices__Custom__5070F446",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "customerID");
                    table.ForeignKey(
                        name: "FK__Invoices__Employ__5165187F",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "employeeID");
                    table.ForeignKey(
                        name: "FK__Invoices__Paymen__52593CB8",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "PaymentID");
                    table.ForeignKey(
                        name: "FK__Invoices__Shippi__534D60F1",
                        column: x => x.ShippingID,
                        principalTable: "Shipping",
                        principalColumn: "ShippingID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    sellPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    totalQuantity = table.Column<int>(type: "int", nullable: false),
                    categoryID = table.Column<int>(type: "int", nullable: true),
                    imageUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    videoUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    discount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    discountPrice = table.Column<decimal>(type: "decimal(26,8)", nullable: true, computedColumnSql: "([sellPrice]-([sellPrice]*[discount])/(100))", stored: false),
                    bestsellerFlag = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    homeFlag = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    active = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    dateAdded = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Products__B40CC6ED2EBDBC5C", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK__Products__Suppli__49C3F6B7",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID");
                    table.ForeignKey(
                        name: "FK__Products__catego__48CFD27E",
                        column: x => x.categoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new
                {
                    InvoiceDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__InvoiceD__1F1578F1BB5D5EF5", x => x.InvoiceDetailID);
                    table.ForeignKey(
                        name: "FK__InvoiceDe__Invoi__5629CD9C",
                        column: x => x.InvoiceID,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceID");
                    table.ForeignKey(
                        name: "FK__InvoiceDe__Produ__571DF1D5",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "ProductBatches",
                columns: table => new
                {
                    BatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    BatchNumber = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ManufactureDate = table.Column<DateTime>(type: "date", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "date", nullable: true),
                    importPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    barcode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductB__5D55CE38B0793EA1", x => x.BatchID);
                    table.ForeignKey(
                        name: "FK__ProductBa__Produ__59FA5E80",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACCOUNTS_ROLEID",
                table: "ACCOUNTS",
                column: "ROLEID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryID",
                table: "Categories",
                column: "ParentCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceID",
                table: "InvoiceDetails",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_ProductID",
                table: "InvoiceDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerID",
                table: "Invoices",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_EmployeeID",
                table: "Invoices",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PaymentID",
                table: "Invoices",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ShippingID",
                table: "Invoices",
                column: "ShippingID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBatches_ProductID",
                table: "ProductBatches",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryID",
                table: "Products",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierID",
                table: "Products",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeID",
                table: "Salaries",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingLocations_CustomerID",
                table: "ShippingLocations",
                column: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACCOUNTS");

            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductBatches");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "ShippingLocations");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
