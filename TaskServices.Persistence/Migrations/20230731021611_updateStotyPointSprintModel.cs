using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateStotyPointSprintModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumOfIssue",
                table: "Sprints",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumOfStoryPoint",
                table: "Sprints",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfIssue",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "NumOfStoryPoint",
                table: "Sprints");
        }
    }
}
