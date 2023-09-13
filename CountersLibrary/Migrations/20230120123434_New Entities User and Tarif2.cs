using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CountersLibrary.Migrations
{
    public partial class NewEntitiesUserandTarif2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cost_User_UserID",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_User_UserID",
                table: "Record");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Record",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Cost",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_User_UserID",
                table: "Cost",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_User_UserID",
                table: "Record",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cost_User_UserID",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_User_UserID",
                table: "Record");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Record",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Cost",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_User_UserID",
                table: "Cost",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_User_UserID",
                table: "Record",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID");
        }
    }
}
