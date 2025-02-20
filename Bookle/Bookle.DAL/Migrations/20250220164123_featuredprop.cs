using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookle.DAL.Migrations
{
    /// <inheritdoc />
    public partial class featuredprop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Authors");
        }
    }
}
