using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addRoleToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CodeSmS", "CreatedAt", "Email", "ExpiredCodeSms", "Image", "IsDeleted", "IsOtp", "IsSms", "Name", "Password", "Phone", "RefreshToken", "RefreshTokenExpiryTime", "Role", "SecretKey" },
                values: new object[] { -1, null, new DateTimeOffset(new DateTime(2023, 8, 31, 18, 18, 26, 162, DateTimeKind.Unspecified).AddTicks(8266), new TimeSpan(0, 7, 0, 0, 0)), "admin@gmail.com", null, null, false, false, false, "Admin", "$2a$11$l/9Sw9nmo9iZkTY0AvlI8emVy53K2XmhYLTHHz13l.CffHPBFTXi2", null, null, null, "Admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
