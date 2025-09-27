using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT Id
              FROM AspNetRoles
              WHERE Id = '75ad4d1b-c6dd-448d-a168-f22554da659d')
    INSERT INTO AspNetRoles(Id, Name, NormalizedName)
    VALUES ('75ad4d1b-c6dd-448d-a168-f22554da659d', 'admin', 'ADMIN');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE
FROM AspNetRoles
WHERE Id = '75ad4d1b-c6dd-448d-a168-f22554da659d';");
        }
    }
}
