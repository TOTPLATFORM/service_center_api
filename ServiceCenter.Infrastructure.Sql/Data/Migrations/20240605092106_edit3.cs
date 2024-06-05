using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceCenter.Infrastructure.Sql.Data.Migrations
{
    /// <inheritdoc />
    public partial class edit3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Manager_ManagerId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Campagins_Manager_ManagerId",
                table: "Campagins");

            migrationBuilder.DropForeignKey(
                name: "FK_Manager_Employees_Id",
                table: "Manager");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Manager_ManagerId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manager",
                table: "Manager");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07a57aee-6dea-4921-a40b-7a9ee68392d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "23301982-5163-4d3a-93f1-747e6fd54dd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4511207e-064d-423c-aa2e-219519972712");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45cee4d4-aed9-4ccf-9455-84bb66d14dd5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a33c7ef7-0576-46b6-89c7-51a829775818");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8c27f82-d371-4092-b91a-4c44af184195");

            migrationBuilder.RenameTable(
                name: "Manager",
                newName: "Managers");

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "HiringDate",
                table: "Managers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Responsibilities",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WorkingHours",
                table: "Managers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Managers",
                table: "Managers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "139c25e3-2faf-4190-a581-5e421fcb768e", null, "Admin", "ADMIN" },
                    { "4ac1e94f-72b4-4447-b8a4-34ff444587ae", null, "Manager", "MANAGER" },
                    { "7a3ca6cf-d7f6-4f3d-92b4-24b7539c5be5", null, "Sales", "SALES" },
                    { "95cb486b-64fd-4dc6-81ab-9b858b911b34", null, "WarehouseManager", "WAREHOUSEMANAGER" },
                    { "a1a3d4e9-4e70-4f7f-8670-7422b6513f3a", null, "Employee", "EMPLOYEE" },
                    { "c0e8540b-36da-4f3d-9cb7-f0452d938553", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Managers_ManagerId",
                table: "Branches",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Campagins_Managers_ManagerId",
                table: "Campagins",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Employees_Id",
                table: "Managers",
                column: "Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Managers_ManagerId",
                table: "Reports",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Managers_ManagerId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Campagins_Managers_ManagerId",
                table: "Campagins");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Employees_Id",
                table: "Managers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Managers_ManagerId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Managers",
                table: "Managers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "139c25e3-2faf-4190-a581-5e421fcb768e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ac1e94f-72b4-4447-b8a4-34ff444587ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a3ca6cf-d7f6-4f3d-92b4-24b7539c5be5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95cb486b-64fd-4dc6-81ab-9b858b911b34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1a3d4e9-4e70-4f7f-8670-7422b6513f3a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0e8540b-36da-4f3d-9cb7-f0452d938553");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "HiringDate",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Responsibilities",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                table: "Managers");

            migrationBuilder.RenameTable(
                name: "Managers",
                newName: "Manager");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manager",
                table: "Manager",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07a57aee-6dea-4921-a40b-7a9ee68392d2", null, "Admin", "ADMIN" },
                    { "23301982-5163-4d3a-93f1-747e6fd54dd8", null, "Manager", "MANAGER" },
                    { "4511207e-064d-423c-aa2e-219519972712", null, "Employee", "EMPLOYEE" },
                    { "45cee4d4-aed9-4ccf-9455-84bb66d14dd5", null, "WarehouseManager", "WAREHOUSEMANAGER" },
                    { "a33c7ef7-0576-46b6-89c7-51a829775818", null, "Customer", "CUSTOMER" },
                    { "b8c27f82-d371-4092-b91a-4c44af184195", null, "Sales", "SALES" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Manager_ManagerId",
                table: "Branches",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Campagins_Manager_ManagerId",
                table: "Campagins",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Manager_Employees_Id",
                table: "Manager",
                column: "Id",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Manager_ManagerId",
                table: "Reports",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id");
        }
    }
}
