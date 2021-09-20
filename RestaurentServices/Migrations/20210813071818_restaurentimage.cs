using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurentServices.Migrations
{
    public partial class restaurentimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RestaurentIamage",
                table: "Restaurents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurentIamage",
                table: "Restaurents");
        }
    }
}
