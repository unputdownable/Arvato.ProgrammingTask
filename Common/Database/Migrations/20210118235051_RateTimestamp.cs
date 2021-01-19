using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.Database.Migrations
{
    public partial class RateTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Timestamp",
                table: "Rates",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Rates");
        }
    }
}
