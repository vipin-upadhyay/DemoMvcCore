using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMvcCore.Migrations
{
    public partial class AddcolumntodbTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDataedited",
                table: "Registrations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDataedited",
                table: "Registrations");
        }
    }
}
