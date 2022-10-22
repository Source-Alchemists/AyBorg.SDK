using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atomy.Database.Migrations.PostgreSql.Migrations.Project
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AtomyProjects",
                columns: table => new
                {
                    DbId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtomyProjects", x => x.DbId);
                });

            migrationBuilder.CreateTable(
                name: "PluginMetaInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssemblyName = table.Column<string>(type: "text", nullable: false),
                    AssemblyVersion = table.Column<string>(type: "text", nullable: false),
                    TypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginMetaInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtomyLinks",
                columns: table => new
                {
                    DbId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectRecordId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtomyLinks", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_AtomyLinks_AtomyProjects_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "AtomyProjects",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtomyProjectMetas",
                columns: table => new
                {
                    DbId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    ServiceUniqueName = table.Column<string>(type: "text", nullable: false),
                    ProjectRecordId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtomyProjectMetas", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_AtomyProjectMetas_AtomyProjects_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "AtomyProjects",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtomySteps",
                columns: table => new
                {
                    DbId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MetaInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    ProjectRecordId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtomySteps", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_AtomySteps_AtomyProjects_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "AtomyProjects",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtomySteps_PluginMetaInfo_MetaInfoId",
                        column: x => x.MetaInfoId,
                        principalTable: "PluginMetaInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtomyPorts",
                columns: table => new
                {
                    DbId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<int>(type: "integer", nullable: false),
                    StepRecordId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtomyPorts", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_AtomyPorts_AtomySteps_StepRecordId",
                        column: x => x.StepRecordId,
                        principalTable: "AtomySteps",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtomyLinks_ProjectRecordId",
                table: "AtomyLinks",
                column: "ProjectRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AtomyPorts_StepRecordId",
                table: "AtomyPorts",
                column: "StepRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AtomyProjectMetas_ProjectRecordId",
                table: "AtomyProjectMetas",
                column: "ProjectRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AtomySteps_MetaInfoId",
                table: "AtomySteps",
                column: "MetaInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AtomySteps_ProjectRecordId",
                table: "AtomySteps",
                column: "ProjectRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtomyLinks");

            migrationBuilder.DropTable(
                name: "AtomyPorts");

            migrationBuilder.DropTable(
                name: "AtomyProjectMetas");

            migrationBuilder.DropTable(
                name: "AtomySteps");

            migrationBuilder.DropTable(
                name: "AtomyProjects");

            migrationBuilder.DropTable(
                name: "PluginMetaInfo");
        }
    }
}
