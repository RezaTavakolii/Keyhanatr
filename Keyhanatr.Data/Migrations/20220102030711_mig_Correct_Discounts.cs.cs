using Microsoft.EntityFrameworkCore.Migrations;

namespace Keyhanatr.Data.Migrations
{
    public partial class mig_Correct_Discountscs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discount_DiscountId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "Discounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "Discount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discount_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
