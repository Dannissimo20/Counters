using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CountersLibrary.Migrations
{
    public partial class NewEntitiesUserandTarif : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Record",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Cost",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tarif",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Power = table.Column<double>(type: "double precision", nullable: false),
                    ColdWater = table.Column<double>(type: "double precision", nullable: false),
                    Disposal = table.Column<double>(type: "double precision", nullable: false),
                    HotWater = table.Column<double>(type: "double precision", nullable: false),
                    Gas = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarif", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tarif_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Record_UserID",
                table: "Record",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_UserID",
                table: "Cost",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tarif_UserID",
                table: "Tarif",
                column: "UserID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cost_User_UserID",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_User_UserID",
                table: "Record");

            migrationBuilder.DropTable(
                name: "Tarif");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Record_UserID",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Cost_UserID",
                table: "Cost");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Record");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Cost");
        }
    }
}
