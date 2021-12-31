using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Keyhanatr.Data.Migrations
{
    public partial class mig_Test2s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Test",
            //    table: "Test");

            //migrationBuilder.RenameTable(
            //    name: "Test",
            //    newName: "Tests");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Tests",
            //    table: "Tests",
            //    column: "TestId");

            //migrationBuilder.CreateTable(
            //    name: "Test2s",
            //    columns: table => new
            //    {
            //        TestId = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Date = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Test2s", x => x.TestId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test2s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tests",
                table: "Tests");

            migrationBuilder.RenameTable(
                name: "Tests",
                newName: "Test");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                column: "TestId");
        }
    }
}
