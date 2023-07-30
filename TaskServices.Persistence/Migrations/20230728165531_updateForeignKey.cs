using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskServices.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Statuses_StatusId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_SubIssues_Statuses_StatusId",
                table: "SubIssues");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Statuses_StatusId",
                table: "Issues",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SubIssues_Statuses_StatusId",
                table: "SubIssues",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Statuses_StatusId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_SubIssues_Statuses_StatusId",
                table: "SubIssues");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Statuses_StatusId",
                table: "Issues",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubIssues_Statuses_StatusId",
                table: "SubIssues",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
