using Microsoft.EntityFrameworkCore.Migrations;

namespace Keyhanatr.Data.Migrations
{
    public partial class mig_Nav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroups_ProductNavGroup_ProductNavGroupId",
                table: "ProductGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductNavGroup",
                table: "ProductNavGroup");

            migrationBuilder.RenameTable(
                name: "ProductNavGroup",
                newName: "ProductNavGroups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductNavGroups",
                table: "ProductNavGroups",
                column: "NavGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroups_ProductNavGroups_ProductNavGroupId",
                table: "ProductGroups",
                column: "ProductNavGroupId",
                principalTable: "ProductNavGroups",
                principalColumn: "NavGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroups_ProductNavGroups_ProductNavGroupId",
                table: "ProductGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductNavGroups",
                table: "ProductNavGroups");

            migrationBuilder.RenameTable(
                name: "ProductNavGroups",
                newName: "ProductNavGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductNavGroup",
                table: "ProductNavGroup",
                column: "NavGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroups_ProductNavGroup_ProductNavGroupId",
                table: "ProductGroups",
                column: "ProductNavGroupId",
                principalTable: "ProductNavGroup",
                principalColumn: "NavGroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
