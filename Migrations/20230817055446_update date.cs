using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogging_Project.Migrations
{
    /// <inheritdoc />
    public partial class updatedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Todo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadlineDate",
                table: "Todo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DeadlineTime",
                table: "Todo",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "DeadlineTime",
                table: "Todo");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Todo",
                type: "datetime2",
                nullable: true);
        }
    }
}
