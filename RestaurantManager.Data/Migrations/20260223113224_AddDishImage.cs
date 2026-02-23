using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDishImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Dishes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Dishes");
        }
    }
}
