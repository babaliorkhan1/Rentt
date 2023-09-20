using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBackend.Migrations
{
    public partial class addedfavourite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavouriteId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavouriteId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_FavouriteId",
                table: "Cars",
                column: "FavouriteId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FavouriteId",
                table: "AspNetUsers",
                column: "FavouriteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Favourites_FavouriteId",
                table: "AspNetUsers",
                column: "FavouriteId",
                principalTable: "Favourites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Favourites_FavouriteId",
                table: "Cars",
                column: "FavouriteId",
                principalTable: "Favourites",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Favourites_FavouriteId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Favourites_FavouriteId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Cars_FavouriteId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FavouriteId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavouriteId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FavouriteId",
                table: "AspNetUsers");
        }
    }
}
