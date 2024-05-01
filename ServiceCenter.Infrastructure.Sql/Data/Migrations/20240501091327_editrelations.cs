using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCenter.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class editrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Appotiments_AppotimentId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServicePackages_ServicePackageId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "Appotiments");

            migrationBuilder.DropTable(
                name: "CustomerOffers");

            migrationBuilder.RenameColumn(
                name: "AppotimentId",
                table: "Schedules",
                newName: "AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_AppotimentId",
                table: "Schedules",
                newName: "IX_Schedules_AppointmentId");

            migrationBuilder.AlterColumn<int>(
                name: "ServicePackageId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Appointments_AppointmentId",
                table: "Schedules",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServicePackages_ServicePackageId",
                table: "Services",
                column: "ServicePackageId",
                principalTable: "ServicePackages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Appointments_AppointmentId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServicePackages_ServicePackageId",
                table: "Services");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "Schedules",
                newName: "AppotimentId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_AppointmentId",
                table: "Schedules",
                newName: "IX_Schedules_AppotimentId");

            migrationBuilder.AlterColumn<int>(
                name: "ServicePackageId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Appotiments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appotiments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appotiments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
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

            migrationBuilder.CreateIndex(
                name: "IX_Appotiments_CustomerId",
                table: "Appotiments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOffers_CustomerId",
                table: "CustomerOffers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOffers_OfferId",
                table: "CustomerOffers",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Appotiments_AppotimentId",
                table: "Schedules",
                column: "AppotimentId",
                principalTable: "Appotiments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServicePackages_ServicePackageId",
                table: "Services",
                column: "ServicePackageId",
                principalTable: "ServicePackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
