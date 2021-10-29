using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceBusExample.Infrastructure.Migrations
{
    public partial class Probs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IsIndex",
                table: "Articles",
                type: "nvarchar(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsIndex",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldNullable: true);
        }
    }
}