using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdAstra.Migrations
{
    /// <inheritdoc />
    public partial class addedmissingrelationbetweenserandcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdAstraUserId",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_AdAstraUserId",
                table: "Categories",
                column: "AdAstraUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_AdAstraUserId",
                table: "Categories",
                column: "AdAstraUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_AdAstraUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_AdAstraUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "AdAstraUserId",
                table: "Categories");
        }
    }
}
