using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Database.Migrations
{
    public partial class RateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Rates",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Rates",
                newName: "Timestamp");
        }
    }
}
