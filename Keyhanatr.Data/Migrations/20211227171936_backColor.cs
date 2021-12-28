using Microsoft.EntityFrameworkCore.Migrations;

namespace Keyhanatr.Data.Migrations
{
    public partial class backColor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackColor",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackColor",
                table: "Sliders");
        }
    }
}
