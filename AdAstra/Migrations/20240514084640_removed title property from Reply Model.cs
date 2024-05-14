using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdAstra.Migrations
{
    /// <inheritdoc />
    public partial class removedtitlepropertyfromReplyModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Replies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
