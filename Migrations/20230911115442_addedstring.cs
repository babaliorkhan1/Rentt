using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBackend.Migrations
{
    public partial class addedstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_AppUserId1",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_AppUserId1",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Favourites");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Favourites",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_AppUserId",
                table: "Favourites",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_AppUserId",
                table: "Favourites",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_AppUserId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_AppUserId",
                table: "Favourites");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Favourites",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Favourites",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_AppUserId1",
                table: "Favourites",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_AppUserId1",
                table: "Favourites",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
