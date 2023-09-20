using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalBackend.Migrations
{
    public partial class addedfavsvs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteCars");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Favourites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_CarId",
                table: "Favourites",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Cars_CarId",
                table: "Favourites",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Cars_CarId",
                table: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Favourites_CarId",
                table: "Favourites");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Favourites");

            migrationBuilder.CreateTable(
                name: "FavouriteCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    FavouriteId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteCars_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteCars_Favourites_FavouriteId",
                        column: x => x.FavouriteId,
                        principalTable: "Favourites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteCars_CarId",
                table: "FavouriteCars",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteCars_FavouriteId",
                table: "FavouriteCars",
                column: "FavouriteId");
        }
    }
}
