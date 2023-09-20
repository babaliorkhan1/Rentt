using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBackend.Migrations
{
    public partial class addedlike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dislikecount",
                table: "Comments",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dislikecount",
                table: "Comments");
        }
    }
}
