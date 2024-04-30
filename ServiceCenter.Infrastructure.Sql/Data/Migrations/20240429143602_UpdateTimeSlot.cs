using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceCenter.Infrastructure.Sql.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTimeSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Manager_ManagerId",
                table: "TimeSlots");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "TimeSlots",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Manager_ManagerId",
                table: "TimeSlots",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Manager_ManagerId",
                table: "TimeSlots");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "TimeSlots",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Manager_ManagerId",
                table: "TimeSlots",
                column: "ManagerId",
                principalTable: "Manager",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
