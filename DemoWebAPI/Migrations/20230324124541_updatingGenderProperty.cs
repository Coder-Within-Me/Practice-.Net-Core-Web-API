using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoWebAPI.Migrations
{
    public partial class updatingGenderProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "testdata");

            migrationBuilder.AddColumn<int>(
                name: "GenderID",
                table: "testdata",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenderID",
                table: "testdata");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "testdata",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
