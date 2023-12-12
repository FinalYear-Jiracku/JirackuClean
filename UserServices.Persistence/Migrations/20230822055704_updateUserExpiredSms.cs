using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateUserExpiredSms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExpiredCodeSms",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredCodeSms",
                table: "Users");
        }
    }
}
