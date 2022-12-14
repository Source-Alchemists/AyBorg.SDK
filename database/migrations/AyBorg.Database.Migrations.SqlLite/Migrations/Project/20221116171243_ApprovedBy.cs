using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AyBorg.Database.Migrations.SqlLite.Migrations.Project
{
    /// <inheritdoc />
    public partial class ApprovedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "AyBorgProjectMetas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "AyBorgProjectMetas");
        }
    }
}
