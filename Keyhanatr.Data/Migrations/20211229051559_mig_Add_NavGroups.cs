using Microsoft.EntityFrameworkCore.Migrations;

namespace Keyhanatr.Data.Migrations
{
    public partial class mig_Add_NavGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductNavGroupId",
                table: "ProductGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductNavGroup",
                columns: table => new
                {
                    NavGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NavTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductNavGroup", x => x.NavGroupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_ProductNavGroupId",
                table: "ProductGroups",
                column: "ProductNavGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroups_ProductNavGroup_ProductNavGroupId",
                table: "ProductGroups",
                column: "ProductNavGroupId",
                principalTable: "ProductNavGroup",
                principalColumn: "NavGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroups_ProductNavGroup_ProductNavGroupId",
                table: "ProductGroups");

            migrationBuilder.DropTable(
                name: "ProductNavGroup");

            migrationBuilder.DropIndex(
                name: "IX_ProductGroups_ProductNavGroupId",
                table: "ProductGroups");

            migrationBuilder.DropColumn(
                name: "ProductNavGroupId",
                table: "ProductGroups");
        }
    }
}
