using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatePropertyUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Users",
                newName: "SecretKey");

            migrationBuilder.AddColumn<bool>(
                name: "IsOtp",
                table: "Users",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOtp",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SecretKey",
                table: "Users",
                newName: "CustomerId");
        }
    }
}
