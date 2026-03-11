using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicFieldToRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "Restaurants",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Public",
                table: "Restaurants");
        }
    }
}
