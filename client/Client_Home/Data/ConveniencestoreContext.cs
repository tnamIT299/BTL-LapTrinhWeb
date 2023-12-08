using System;
using System.Collections.Generic;
using Client_Home.Areas.Admin.Models;
using Client_Home.Models;
using Microsoft.EntityFrameworkCore;

namespace Client_Home.Data;

public partial class ConveniencestoreContext : DbContext
{
    public ConveniencestoreContext()
    {
    }
    public ConveniencestoreContext(DbContextOptions<ConveniencestoreContext> options)
        : base(options)
    {
    }
    public virtual DbSet<AdminBestSellingProduct> AdminBestSellingProduct { get; set; }
    public virtual DbSet<AdminOnlineOfflinePurchaseCount> AdminOnlineOfflinePurchaseCount { get; set; }
    public virtual DbSet<AdminRevenueByMonth> AdminRevenueByMonth { get; set; }
    public virtual DbSet<AdminRichestCustomerView> AdminRichestCustomerView { get; set; }
    public virtual DbSet<AdminSingleIntForProcedure> AdminSingleIntForProcedure { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CommentReply> CommentReplies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductBatch> ProductBatches { get; set; }

    public virtual DbSet<ProductComment> ProductComments { get; set; }

    public virtual DbSet<ProductSubImage> ProductSubImages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<SellPriceHistory> SellPriceHistories { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<ShippingLocation> ShippingLocations { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=LAPTOP-U09IUJKK\\SQLEXPRESS01;database=CONVENIENCESTORE;Encrypt=False;Integrated Security=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__cartItem__415B03B86F8BCDF9");

            entity.ToTable("cartItems");

            entity.Property(e => e.CartId)
                .ValueGeneratedNever()
                .HasColumnName("cartId");
            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__cartItems__produ__1209AD79");

            entity.HasOne(d => d.User).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__cartItems__userI__11158940");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B10713FBC");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK__Categorie__Paren__412EB0B6");
        });

        modelBuilder.Entity<CommentReply>(entity =>
        {
            entity.HasKey(e => e.ReplyId).HasName("PK__commentR__36BBF68836C134FA");

            entity.ToTable("commentReplies");

            entity.Property(e => e.ReplyId)
                .ValueGeneratedNever()
                .HasColumnName("replyId");
            entity.Property(e => e.CommentId).HasColumnName("commentId");
            entity.Property(e => e.ReplyText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("replyText");
            entity.Property(e => e.Timestamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("timestamp");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentReplies)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK__commentRe__comme__1B9317B3");

            entity.HasOne(d => d.User).WithMany(p => p.CommentReplies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__commentRe__userI__1C873BEC");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__TempCusto__UserI__03BB8E22");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__discount__D2130A66F74978AA");

            entity.ToTable("discount");

            entity.Property(e => e.DiscountId).HasColumnName("discountId");
            entity.Property(e => e.DiscountAmount)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("discountAmount");
            entity.Property(e => e.DiscountPercent)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("discountPercent");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("endDate");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("startDate");

            entity.HasOne(d => d.Product).WithMany(p => p.Discounts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__discount__produc__0E391C95");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C134C9A11A764CDB");

            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.CitizenId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CitizenID");
            entity.Property(e => e.DateHired).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Employees__UserI__70A8B9AE");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD58A15F0AA");

            entity.ToTable(tb => tb.HasTrigger("CalculateRewardPoints"));

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.DeliveryCost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("deliveryCost");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeID");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.ShippingId).HasColumnName("ShippingID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Invoices__Custom__0880433F");

            entity.HasOne(d => d.Employee).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Invoices__Employ__5165187F");

            entity.HasOne(d => d.Payment).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Invoices__Paymen__52593CB8");

            entity.HasOne(d => d.Shipping).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ShippingId)
                .HasConstraintName("FK__Invoices__Shippi__534D60F1");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => e.InvoiceDetailId).HasName("PK__InvoiceD__1F1578F1BB5D5EF5");

            entity.ToTable(tb => tb.HasTrigger("UpdateTotalAmount"));

            entity.Property(e => e.InvoiceDetailId).HasColumnName("InvoiceDetailID");
            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductBatchId).HasColumnName("productBatchID");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK__InvoiceDe__Invoi__5629CD9C");

            entity.HasOne(d => d.ProductBatch).WithMany(p => p.InvoiceDetails)
                .HasForeignKey(d => d.ProductBatchId)
                .HasConstraintName("FK__InvoiceDe__produ__662B2B3B");
        });

        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFD0BC4AE7");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("money")
                .HasColumnName("totalAmount");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Supplier__6CD828CA");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30C1E2B64A9");

            entity.Property(e => e.OrderDetailId)
                .ValueGeneratedNever()
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__671F4F74");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__681373AD");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A589797EF5E");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Details)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MethodName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED2EBDBC5C");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.BestsellerFlag)
                .HasDefaultValueSql("((0))")
                .HasColumnName("bestsellerFlag");
            entity.Property(e => e.CategoryId).HasColumnName("categoryID");
            entity.Property(e => e.DateAdded)
                .HasColumnType("date")
                .HasColumnName("dateAdded");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.DiscountPrice)
                .HasComputedColumnSql("([sellPrice]-([sellPrice]*[discount])/(100))", false)
                .HasColumnType("decimal(26, 8)")
                .HasColumnName("discountPrice");
            entity.Property(e => e.HomeFlag)
                .HasDefaultValueSql("((0))")
                .HasColumnName("homeFlag");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Qrcode)
                .HasMaxLength(50)
                .HasColumnName("QRCode");
            entity.Property(e => e.SellPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sellPrice");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.ThumbnailUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("thumbnailUrl");
            entity.Property(e => e.TotalQuantity).HasColumnName("totalQuantity");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("videoUrl");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__catego__48CFD27E");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Products__Suppli__49C3F6B7");

            entity.HasMany(d => d.Product1s).WithMany(p => p.Product2s)
                .UsingEntity<Dictionary<string, object>>(
                    "PackageProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("Product1Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__packagePr__produ__1F63A897"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product2Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__packagePr__produ__2057CCD0"),
                    j =>
                    {
                        j.HasKey("Product1Id", "Product2Id").HasName("PK__packageP__0D0BA5739EDAC62D");
                        j.ToTable("packageProduct");
                        j.IndexerProperty<int>("Product1Id").HasColumnName("product1ID");
                        j.IndexerProperty<int>("Product2Id").HasColumnName("product2ID");
                    });

            entity.HasMany(d => d.Product2s).WithMany(p => p.Product1s)
                .UsingEntity<Dictionary<string, object>>(
                    "PackageProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("Product2Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__packagePr__produ__2057CCD0"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product1Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__packagePr__produ__1F63A897"),
                    j =>
                    {
                        j.HasKey("Product1Id", "Product2Id").HasName("PK__packageP__0D0BA5739EDAC62D");
                        j.ToTable("packageProduct");
                        j.IndexerProperty<int>("Product1Id").HasColumnName("product1ID");
                        j.IndexerProperty<int>("Product2Id").HasColumnName("product2ID");
                    });
        });

        modelBuilder.Entity<ProductBatch>(entity =>
        {
            entity.HasKey(e => e.BatchId).HasName("PK__ProductB__5D55CE38B0793EA1");

            entity.ToTable(tb => tb.HasTrigger("updateTotalQuantity"));

            entity.Property(e => e.BatchId).HasColumnName("BatchID");
            entity.Property(e => e.Barcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("barcode");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.ImportPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("importPrice");
            entity.Property(e => e.ManufactureDate).HasColumnType("date");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductBatches)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductBa__Produ__59FA5E80");
        });

        modelBuilder.Entity<ProductComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__product___CDDE919D0E6B08DC");

            entity.ToTable("productComments");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("commentId");
            entity.Property(e => e.CommentText)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("commentText");
            entity.Property(e => e.CreatedDate)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("createdDate");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductComments)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__product_c__produ__14E61A24");

            entity.HasOne(d => d.User).WithMany(p => p.ProductComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__product_c__userI__15DA3E5D");
        });

        modelBuilder.Entity<ProductSubImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__productS__3214EC2730036022");

            entity.ToTable("productSubImage");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(255)
                .HasColumnName("imgUrl");
            entity.Property(e => e.ProductId).HasColumnName("productID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSubImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__productSu__produ__160F4887");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PK__ROLES__006568E9599A1705");

            entity.ToTable("ROLES");

            entity.Property(e => e.Roleid).HasColumnName("ROLEID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Rolename)
                .HasMaxLength(50)
                .HasColumnName("ROLENAME");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Salaries__3214EC27DC986AA3");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateTotal"));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bonus).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Deductions).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MonthYear).HasColumnType("date");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Salaries__Employ__3B75D760");
        });

        modelBuilder.Entity<SellPriceHistory>(entity =>
        {
            entity.HasKey(e => e.PriceId).HasName("PK__sellPric__366E4CC2B0D36D4A");

            entity.ToTable("sellPriceHistory");

            entity.Property(e => e.PriceId).HasColumnName("priceId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Sellprice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sellprice");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("updateDate");

            entity.HasOne(d => d.Product).WithMany(p => p.SellPriceHistories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__sellPrice__produ__0B5CAFEA");
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.ShippingId).HasName("PK__Shipping__5FACD460BC0762F5");

            entity.ToTable("Shipping");

            entity.Property(e => e.ShippingId).HasColumnName("ShippingID");
            entity.Property(e => e.CompleteTime)
                .HasColumnType("datetime")
                .HasColumnName("completeTime");
            entity.Property(e => e.ExpectedDate)
                .HasColumnType("date")
                .HasColumnName("expectedDate");
            entity.Property(e => e.LocationId).HasColumnName("locationID");
            entity.Property(e => e.ShipperName)
                .HasMaxLength(100)
                .HasColumnName("shipperName");
            entity.Property(e => e.ShippingCost)
                .HasColumnType("decimal(9, 0)")
                .HasColumnName("shippingCost");
            entity.Property(e => e.TimeSlot)
                .HasMaxLength(7)
                .HasColumnName("timeSlot");

            entity.HasOne(d => d.Location).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Shipping__locati__6DCC4D03");
        });

        modelBuilder.Entity<ShippingLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Shipping__E7FEA4771FD4FB63");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("district");
            entity.Property(e => e.IsDefault).HasColumnName("isDefault");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Ward)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ward");

            entity.HasOne(d => d.Customer).WithMany(p => p.ShippingLocations)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__ShippingL__Custo__078C1F06");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE66694069AD674");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__ACCOUNTS__1F64FDFCD3B51490");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Createdate)
                .HasColumnType("datetime")
                .HasColumnName("CREATEDATE");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fullname)
                .HasMaxLength(150)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Lastlogin)
                .HasColumnType("datetime")
                .HasColumnName("LASTLOGIN");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.Roleid).HasColumnName("ROLEID");
            entity.Property(e => e.Salt)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("SALT");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("FK__ACCOUNTS__ROLEID__74AE54BC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
