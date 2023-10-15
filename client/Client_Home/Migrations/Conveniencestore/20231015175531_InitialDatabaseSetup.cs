using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Client_Home.Migrations.Conveniencestore
{
    /// <inheritdoc />
    public partial class InitialDatabaseSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryTime",
                table: "Shipping");

            migrationBuilder.DropColumn(
                name: "importPrice",
                table: "ProductBatches");

            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "Products",
                newName: "thumbnailUrl");

            migrationBuilder.AlterDatabase(
                collation: "Latin1_General_CI_AS");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateAdded",
                table: "Products",
                type: "date",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "rowversion",
                oldRowVersion: true);

            migrationBuilder.AddColumn<decimal>(
                name: "importPrice",
                table: "Products",
                type: "decimal(10,0)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdDate",
                table: "Invoices",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "rowversion",
                oldRowVersion: true);

            migrationBuilder.AddColumn<decimal>(
                name: "deliveryCost",
                table: "Invoices",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "birthday",
                table: "Customers",
                type: "date",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "active",
                table: "ACCOUNTS",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "productSubImage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imgUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    productID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__productS__3214EC2730036022", x => x.ID);
                    table.ForeignKey(
                        name: "FK__productSu__produ__160F4887",
                        column: x => x.productID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_productSubImage_productID",
                table: "productSubImage",
                column: "productID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productSubImage");

            migrationBuilder.DropColumn(
                name: "importPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "deliveryCost",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "thumbnailUrl",
                table: "Products",
                newName: "imageUrl");

            migrationBuilder.AlterDatabase(
                oldCollation: "Latin1_General_CI_AS");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Shipping",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDeliveryTime",
                table: "Shipping",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "dateAdded",
                table: "Products",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "importPrice",
                table: "ProductBatches",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<byte[]>(
                name: "createdDate",
                table: "Invoices",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birthday",
                table: "Customers",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "active",
                table: "ACCOUNTS",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))");
        }
    }
}
