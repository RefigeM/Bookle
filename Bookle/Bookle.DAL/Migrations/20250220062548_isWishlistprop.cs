using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookle.DAL.Migrations
{
    /// <inheritdoc />
    public partial class isWishlistprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInWishlist",
                table: "Wishlists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInWishlist",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInWishlist",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "IsInWishlist",
                table: "Books");
        }
    }
}
