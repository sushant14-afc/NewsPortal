using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPortal.Migrations
{
    /// <inheritdoc />
    public partial class Added_Categoryfield_in_News : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "News",
                newName: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_CategoryId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "News",
                newName: "Category");
        }
    }
}
