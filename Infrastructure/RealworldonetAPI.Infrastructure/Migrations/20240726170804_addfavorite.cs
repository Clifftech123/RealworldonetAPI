using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealworldonetAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addfavorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favorited",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorited",
                table: "Articles");
        }
    }
}
