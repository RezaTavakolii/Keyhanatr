using Microsoft.EntityFrameworkCore.Migrations;

namespace Keyhanatr.Data.Migrations
{
    public partial class TitrSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titr",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titr",
                table: "Sliders");
        }
    }
}
