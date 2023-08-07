using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateSprintModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStart",
                table: "Sprints",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStart",
                table: "Sprints");
        }
    }
}
