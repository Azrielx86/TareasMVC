using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class UserTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserCreationId",
                table: "Tasks",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserCreationId",
                table: "Tasks",
                column: "UserCreationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_UserCreationId",
                table: "Tasks",
                column: "UserCreationId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_UserCreationId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserCreationId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserCreationId",
                table: "Tasks");
        }
    }
}
