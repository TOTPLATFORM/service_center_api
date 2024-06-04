using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceCenter.Infrastructure.Sql.Data.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemCategories_ItemCategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Services_ServiceId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Centers_CenterId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Inventories_InventoryId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vendors_VendorId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Centers_CenterId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemCategoryId",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "221d225d-b1db-4f8d-8b9e-9ccc65aef98a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f127a35-0482-4634-a259-c008c6caa052");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "995d26cb-4d3d-4610-9b68-205167832655");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99f7f577-e649-4ff9-99cb-32863596337a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d69686ea-c454-4b1d-8984-c21f662c508f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecb19858-df64-4c9d-8815-07b514c84c79");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorType",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "ItemCategoryId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "CenterId",
                table: "Vendors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VendorId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CenterId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "RatingDate",
                table: "Rating",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "OfferCategory",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "23f17829-f8a2-4e95-a888-7b1141b9026a", null, "Employee", "EMPLOYEE" },
                    { "31b95a82-ee49-44b3-b574-10dab181f1cf", null, "Sales", "SALES" },
                    { "4745518e-4ff1-44c8-aa48-8912c9ef27f2", null, "Manager", "MANAGER" },
                    { "59c3088e-99d6-4c3e-84c6-57bf9640c794", null, "Admin", "ADMIN" },
                    { "5c83f3cf-344b-4de8-be58-13f297d56821", null, "Customer", "CUSTOMER" },
                    { "e42576c7-5cc7-4ebf-8eed-6647fc23152f", null, "WarehouseManager", "WAREHOUSEMANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemCategories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "ItemCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Services_ServiceId",
                table: "Items",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Centers_CenterId",
                table: "Sales",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Inventories_InventoryId",
                table: "Transactions",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vendors_VendorId",
                table: "Transactions",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Centers_CenterId",
                table: "Vendors",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemCategories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Services_ServiceId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Centers_CenterId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Inventories_InventoryId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Vendors_VendorId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_Centers_CenterId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategoryId",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23f17829-f8a2-4e95-a888-7b1141b9026a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31b95a82-ee49-44b3-b574-10dab181f1cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4745518e-4ff1-44c8-aa48-8912c9ef27f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59c3088e-99d6-4c3e-84c6-57bf9640c794");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c83f3cf-344b-4de8-be58-13f297d56821");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e42576c7-5cc7-4ebf-8eed-6647fc23152f");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RatingDate",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "OfferCategory",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "CenterId",
                table: "Vendors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VendorType",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "VendorId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CenterId",
                table: "Sales",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "Items",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ItemCategoryId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "221d225d-b1db-4f8d-8b9e-9ccc65aef98a", null, "Employee", "EMPLOYEE" },
                    { "6f127a35-0482-4634-a259-c008c6caa052", null, "Manager", "MANAGER" },
                    { "995d26cb-4d3d-4610-9b68-205167832655", null, "Sales", "SALES" },
                    { "99f7f577-e649-4ff9-99cb-32863596337a", null, "Customer", "CUSTOMER" },
                    { "d69686ea-c454-4b1d-8984-c21f662c508f", null, "Admin", "ADMIN" },
                    { "ecb19858-df64-4c9d-8815-07b514c84c79", null, "WarehouseManager", "WAREHOUSEMANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemCategoryId",
                table: "Items",
                column: "ItemCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemCategories_ItemCategoryId",
                table: "Items",
                column: "ItemCategoryId",
                principalTable: "ItemCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Services_ServiceId",
                table: "Items",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Centers_CenterId",
                table: "Sales",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Inventories_InventoryId",
                table: "Transactions",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Vendors_VendorId",
                table: "Transactions",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_Centers_CenterId",
                table: "Vendors",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id");
        }
    }
}
