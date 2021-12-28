using Microsoft.EntityFrameworkCore.Migrations;

namespace Keyhanatr.Data.Migrations
{
    public partial class mig_Add_Exist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExistNumber",
                table: "Products",
                newName: "ExistId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductColors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExistId",
                table: "ProductColors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductExist",
                columns: table => new
                {
                    ExistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExistNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductExist", x => x.ExistId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ExistId",
                table: "Products",
                column: "ExistId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ExistId",
                table: "ProductColors",
                column: "ExistId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColors_ProductExist_ExistId",
                table: "ProductColors",
                column: "ExistId",
                principalTable: "ProductExist",
                principalColumn: "ExistId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductExist_ExistId",
                table: "Products",
                column: "ExistId",
                principalTable: "ProductExist",
                principalColumn: "ExistId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColors_ProductExist_ExistId",
                table: "ProductColors");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductExist_ExistId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductExist");

            migrationBuilder.DropIndex(
                name: "IX_Products_ExistId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductColors_ExistId",
                table: "ProductColors");

            migrationBuilder.DropColumn(
                name: "ExistId",
                table: "ProductColors");

            migrationBuilder.RenameColumn(
                name: "ExistId",
                table: "Products",
                newName: "ExistNumber");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductColors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
