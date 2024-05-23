using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCenter.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class updateInInventoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WareHouseManagers_Inventories_InventoryId",
                table: "WareHouseManagers");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "WareHouseManagers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WareHouseManagers_Inventories_InventoryId",
                table: "WareHouseManagers",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WareHouseManagers_Inventories_InventoryId",
                table: "WareHouseManagers");

            migrationBuilder.AlterColumn<int>(
                name: "InventoryId",
                table: "WareHouseManagers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WareHouseManagers_Inventories_InventoryId",
                table: "WareHouseManagers",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
