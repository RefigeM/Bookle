using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookle.DAL.Migrations
{
    /// <inheritdoc />
    public partial class isReaded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReaded",
                table: "ReadLists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReaded",
                table: "ReadLists");
        }
    }
}
