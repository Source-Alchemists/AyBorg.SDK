using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autodroid.Database.Migrations.PostgreSql.Migrations.Project
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutodroidProjects",
                columns: table => new
                {
                    DbId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutodroidProjects", x => x.DbId);
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
                name: "AutodroidLinks",
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
                    table.PrimaryKey("PK_AutodroidLinks", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_AutodroidLinks_AutodroidProjects_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "AutodroidProjects",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutodroidProjectMetas",
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
                    table.PrimaryKey("PK_AutodroidProjectMetas", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_AutodroidProjectMetas_AutodroidProjects_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "AutodroidProjects",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutodroidSteps",
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
                    table.PrimaryKey("PK_AutodroidSteps", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_AutodroidSteps_AutodroidProjects_ProjectRecordId",
                        column: x => x.ProjectRecordId,
                        principalTable: "AutodroidProjects",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutodroidSteps_PluginMetaInfo_MetaInfoId",
                        column: x => x.MetaInfoId,
                        principalTable: "PluginMetaInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutodroidPorts",
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
                    table.PrimaryKey("PK_AutodroidPorts", x => x.DbId);
                    table.ForeignKey(
                        name: "FK_AutodroidPorts_AutodroidSteps_StepRecordId",
                        column: x => x.StepRecordId,
                        principalTable: "AutodroidSteps",
                        principalColumn: "DbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutodroidLinks_ProjectRecordId",
                table: "AutodroidLinks",
                column: "ProjectRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AutodroidPorts_StepRecordId",
                table: "AutodroidPorts",
                column: "StepRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AutodroidProjectMetas_ProjectRecordId",
                table: "AutodroidProjectMetas",
                column: "ProjectRecordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AutodroidSteps_MetaInfoId",
                table: "AutodroidSteps",
                column: "MetaInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AutodroidSteps_ProjectRecordId",
                table: "AutodroidSteps",
                column: "ProjectRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutodroidLinks");

            migrationBuilder.DropTable(
                name: "AutodroidPorts");

            migrationBuilder.DropTable(
                name: "AutodroidProjectMetas");

            migrationBuilder.DropTable(
                name: "AutodroidSteps");

            migrationBuilder.DropTable(
                name: "AutodroidProjects");

            migrationBuilder.DropTable(
                name: "PluginMetaInfo");
        }
    }
}
