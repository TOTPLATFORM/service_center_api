using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCenter.Infrastructure.Sql.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceCenterDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appotiment_Customer_CustomerId",
                table: "Appotiment");

            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Center_CenterId",
                table: "Branch");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_Customer_CustomerId",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_AspNetUsers_Id",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Branch_BranchId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Center_CenterId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_Id",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Customer_CustomerId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_AspNetUsers_Id",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_Department_DepartmentId",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Center_CenterId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Appotiment_AppotimentId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Employee_EmployeeId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Employee_EmployeeId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServiceCategory_ServiceCategoryId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_ServicePackage_ServicePackageId",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Manager_ManagerId",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicePackage",
                table: "ServicePackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCategory",
                table: "ServiceCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Service",
                table: "Service");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manager",
                table: "Manager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaint",
                table: "Complaint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Center",
                table: "Center");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branch",
                table: "Branch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appotiment",
                table: "Appotiment");

            migrationBuilder.RenameTable(
                name: "ServicePackage",
                newName: "ServicePackages");

            migrationBuilder.RenameTable(
                name: "ServiceCategory",
                newName: "ServiceCategories");

            migrationBuilder.RenameTable(
                name: "Service",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "Manager",
                newName: "Managers");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "Complaint",
                newName: "Complaints");

            migrationBuilder.RenameTable(
                name: "Center",
                newName: "Centers");

            migrationBuilder.RenameTable(
                name: "Branch",
                newName: "Branches");

            migrationBuilder.RenameTable(
                name: "Appotiment",
                newName: "Appotiments");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "ServiceCategories",
                newName: "ServiceCategoryName");

            migrationBuilder.RenameColumn(
                name: "ServiceDescription",
                table: "ServiceCategories",
                newName: "ServiceCategoryDescription");

            migrationBuilder.RenameIndex(
                name: "IX_Service_ServicePackageId",
                table: "Services",
                newName: "IX_Services_ServicePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_ServiceCategoryId",
                table: "Services",
                newName: "IX_Services_ServiceCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_EmployeeId",
                table: "Services",
                newName: "IX_Services_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_CenterId",
                table: "Rooms",
                newName: "IX_Rooms_CenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Manager_DepartmentId",
                table: "Managers",
                newName: "IX_Managers_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_CustomerId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employees",
                newName: "IX_Employees_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Department_CenterId",
                table: "Departments",
                newName: "IX_Departments_CenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_BranchId",
                table: "Customers",
                newName: "IX_Customers_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_CustomerId",
                table: "Complaints",
                newName: "IX_Complaints_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Branch_CenterId",
                table: "Branches",
                newName: "IX_Branches_CenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Appotiment_CustomerId",
                table: "Appotiments",
                newName: "IX_Appotiments_CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "AppotimentId",
                table: "Schedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicePackages",
                table: "ServicePackages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCategories",
                table: "ServiceCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaints",
                table: "Complaints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Centers",
                table: "Centers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appotiments",
                table: "Appotiments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CenterServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterServices_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePackageId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_ServicePackages_ServicePackageId",
                        column: x => x.ServicePackageId,
                        principalTable: "ServicePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerServices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryCapacity = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundedYear = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    RatingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ContractEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendors_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendors_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceNumber = table.Column<int>(type: "int", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCategories_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WareHouseManagers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PositionTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndtDate = table.Column<DateOnly>(type: "date", nullable: false),
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouseManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHouseManagers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WareHouseManagers_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerOffers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOffers_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RatingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingCustomers_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingServices_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    SalesId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Sales_SalesId",
                        column: x => x.SalesId,
                        principalTable: "Sales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemStock = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOrder_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterServices_CenterId",
                table: "CenterServices",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterServices_ServiceId",
                table: "CenterServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ServicePackageId",
                table: "Contracts",
                column: "ServicePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOffers_CustomerId",
                table: "CustomerOffers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOffers_OfferId",
                table: "CustomerOffers",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServices_CustomerId",
                table: "CustomerServices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerServices_ServiceId",
                table: "CustomerServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategories_InventoryId",
                table: "ItemCategories",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_ItemId",
                table: "ItemOrder",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_OrderId",
                table: "ItemOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandId",
                table: "Products",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SalesId",
                table: "Products",
                column: "SalesId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingCustomers_CustomerId",
                table: "RatingCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingCustomers_RatingId",
                table: "RatingCustomers",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingServices_RatingId",
                table: "RatingServices",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingServices_ServiceId",
                table: "RatingServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CenterId",
                table: "Vendors",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouseManagers_InventoryId",
                table: "WareHouseManagers",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appotiments_Customers_CustomerId",
                table: "Appotiments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Centers_CenterId",
                table: "Branches",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Customers_CustomerId",
                table: "Complaints",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_Id",
                table: "Customers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Branches_BranchId",
                table: "Customers",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Centers_CenterId",
                table: "Departments",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_Id",
                table: "Employees",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Customers_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_AspNetUsers_Id",
                table: "Managers",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Departments_DepartmentId",
                table: "Managers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Centers_CenterId",
                table: "Rooms",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Appotiments_AppotimentId",
                table: "Schedules",
                column: "AppotimentId",
                principalTable: "Appotiments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Employees_EmployeeId",
                table: "Schedules",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceCategories_ServiceCategoryId",
                table: "Services",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServicePackages_ServicePackageId",
                table: "Services",
                column: "ServicePackageId",
                principalTable: "ServicePackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Managers_ManagerId",
                table: "TimeSlots",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appotiments_Customers_CustomerId",
                table: "Appotiments");

            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Centers_CenterId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Customers_CustomerId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_Id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Branches_BranchId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Centers_CenterId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_Id",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Customers_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_AspNetUsers_Id",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Departments_DepartmentId",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Centers_CenterId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Appotiments_AppotimentId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Employees_EmployeeId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Employees_EmployeeId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceCategories_ServiceCategoryId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServicePackages_ServicePackageId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Managers_ManagerId",
                table: "TimeSlots");

            migrationBuilder.DropTable(
                name: "CenterServices");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "CustomerOffers");

            migrationBuilder.DropTable(
                name: "CustomerServices");

            migrationBuilder.DropTable(
                name: "ItemOrder");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RatingCustomers");

            migrationBuilder.DropTable(
                name: "RatingServices");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "WareHouseManagers");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductBrands");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicePackages",
                table: "ServicePackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCategories",
                table: "ServiceCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaints",
                table: "Complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Centers",
                table: "Centers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appotiments",
                table: "Appotiments");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Service");

            migrationBuilder.RenameTable(
                name: "ServicePackages",
                newName: "ServicePackage");

            migrationBuilder.RenameTable(
                name: "ServiceCategories",
                newName: "ServiceCategory");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "Manager");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "Feedback");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "Complaints",
                newName: "Complaint");

            migrationBuilder.RenameTable(
                name: "Centers",
                newName: "Center");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "Branch");

            migrationBuilder.RenameTable(
                name: "Appotiments",
                newName: "Appotiment");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ServicePackageId",
                table: "Service",
                newName: "IX_Service_ServicePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ServiceCategoryId",
                table: "Service",
                newName: "IX_Service_ServiceCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Services_EmployeeId",
                table: "Service",
                newName: "IX_Service_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "ServiceCategoryName",
                table: "ServiceCategory",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "ServiceCategoryDescription",
                table: "ServiceCategory",
                newName: "ServiceDescription");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_CenterId",
                table: "Room",
                newName: "IX_Room_CenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Managers_DepartmentId",
                table: "Manager",
                newName: "IX_Manager_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_CustomerId",
                table: "Feedback",
                newName: "IX_Feedback_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employee",
                newName: "IX_Employee_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_CenterId",
                table: "Department",
                newName: "IX_Department_CenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BranchId",
                table: "Customer",
                newName: "IX_Customer_BranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_CustomerId",
                table: "Complaint",
                newName: "IX_Complaint_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Branches_CenterId",
                table: "Branch",
                newName: "IX_Branch_CenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Appotiments_CustomerId",
                table: "Appotiment",
                newName: "IX_Appotiment_CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "AppotimentId",
                table: "Schedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Service",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Service",
                table: "Service",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicePackage",
                table: "ServicePackage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCategory",
                table: "ServiceCategory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manager",
                table: "Manager",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedback",
                table: "Feedback",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaint",
                table: "Complaint",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Center",
                table: "Center",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branch",
                table: "Branch",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appotiment",
                table: "Appotiment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appotiment_Customer_CustomerId",
                table: "Appotiment",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Center_CenterId",
                table: "Branch",
                column: "CenterId",
                principalTable: "Center",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_Customer_CustomerId",
                table: "Complaint",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_AspNetUsers_Id",
                table: "Customer",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Branch_BranchId",
                table: "Customer",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Center_CenterId",
                table: "Department",
                column: "CenterId",
                principalTable: "Center",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_Id",
                table: "Employee",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                table: "Employee",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Customer_CustomerId",
                table: "Feedback",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_AspNetUsers_Id",
                table: "Manager",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_Department_DepartmentId",
                table: "Manager",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Center_CenterId",
                table: "Room",
                column: "CenterId",
                principalTable: "Center",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Appotiment_AppotimentId",
                table: "Schedules",
                column: "AppotimentId",
                principalTable: "Appotiment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Employee_EmployeeId",
                table: "Schedules",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Employee_EmployeeId",
                table: "Service",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServiceCategory_ServiceCategoryId",
                table: "Service",
                column: "ServiceCategoryId",
                principalTable: "ServiceCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_ServicePackage_ServicePackageId",
                table: "Service",
                column: "ServicePackageId",
                principalTable: "ServicePackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Manager_ManagerId",
                table: "TimeSlots",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id");
        }
    }
}
