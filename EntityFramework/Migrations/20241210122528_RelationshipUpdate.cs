using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LibraryMember_Email",
                table: "LibraryMember",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_ISBN",
                table: "Book",
                column: "ISBN",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LibraryMember_Email",
                table: "LibraryMember");

            migrationBuilder.DropIndex(
                name: "IX_Book_ISBN",
                table: "Book");
        }
    }
}
