using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceBusExample.Infrastructure.Migrations
{
    public partial class iscadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIndex",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsIndex",
                table: "Articles");
        }
    }
}
