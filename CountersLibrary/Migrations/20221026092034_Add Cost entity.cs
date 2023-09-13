using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CountersLibrary.Migrations
{
    public partial class AddCostentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cost",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RRecordID = table.Column<int>(type: "integer", nullable: false),
                    PowerSpend = table.Column<int>(type: "integer", nullable: false),
                    ColdSpend = table.Column<int>(type: "integer", nullable: false),
                    HotSpend = table.Column<int>(type: "integer", nullable: false),
                    GasSpend = table.Column<int>(type: "integer", nullable: false),
                    PowerCost = table.Column<int>(type: "integer", nullable: false),
                    WaterCost = table.Column<int>(type: "integer", nullable: false),
                    GasCost = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cost", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cost_Record_RRecordID",
                        column: x => x.RRecordID,
                        principalTable: "Record",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cost_RRecordID",
                table: "Cost",
                column: "RRecordID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cost");
        }
    }
}
