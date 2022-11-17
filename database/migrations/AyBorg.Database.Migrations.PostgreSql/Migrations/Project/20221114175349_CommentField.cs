using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AyBorg.Database.Migrations.PostgreSql.Migrations.Project
{
    /// <inheritdoc />
    public partial class CommentField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "AyBorgProjectMetas",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "AyBorgProjectMetas");
        }
    }
}
