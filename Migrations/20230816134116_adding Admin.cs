using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging_Project.Migrations
{
    /// <inheritdoc />
    public partial class addingAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "Users",
                newName: "Admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Admin",
                table: "Users",
                newName: "IsAdmin");
        }
    }
}
