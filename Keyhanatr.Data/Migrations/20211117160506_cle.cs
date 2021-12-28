using Microsoft.EntityFrameworkCore.Migrations;

namespace Keyhanatr.Data.Migrations
{
    public partial class cle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "AddressUsers");

            //migrationBuilder.DropTable(
            //    name: "UserInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "AddressUsers",
            //    columns: table => new
            //    {
            //        AddressID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CodePosti = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Family = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Ostan = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Shahr = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AddressUsers", x => x.AddressID);
            //        table.ForeignKey(
            //            name: "FK_AddressUsers_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "UserId",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserInfos",
            //    columns: table => new
            //    {
            //        UserInfoID = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Family = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PhoneDaftar = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserInfos", x => x.UserInfoID);
            //        table.ForeignKey(
            //            name: "FK_UserInfos_Users_UserId",
            //            column: x => x.UserId,
            //            principalTable: "Users",
            //            principalColumn: "UserId",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_AddressUsers_UserId",
            //    table: "AddressUsers",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserInfos_UserId",
            //    table: "UserInfos",
            //    column: "UserId");
        }
    }
}
