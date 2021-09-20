using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurentServices.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RestaurentIamage",
                table: "Restaurents",
                newName: "RestaurentImage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RestaurentImage",
                table: "Restaurents",
                newName: "RestaurentIamage");
        }
    }
}
