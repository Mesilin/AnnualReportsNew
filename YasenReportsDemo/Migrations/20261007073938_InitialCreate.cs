using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace YasenReportsDemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    EffectiveFromYear = table.Column<int>(type: "integer", nullable: false),
                    ConfigJson = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Oktmo = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidationRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Scope = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Kind = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Expression = table.Column<string>(type: "text", nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Severity = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    EffectiveFromYear = table.Column<int>(type: "integer", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirDivisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirDivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirDivisions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forestries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    AreaThsHa = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forestries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forestries_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportInstances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    RegionId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ConfigVersionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinalizedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportInstances_FormConfigs_ConfigVersionId",
                        column: x => x.ConfigVersionId,
                        principalTable: "FormConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportInstances_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AirDivisionId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    WorkType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SubType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Hours = table.Column<double>(type: "double precision", nullable: false),
                    DaysWithFlights = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightHours_AirDivisions_AirDivisionId",
                        column: x => x.AirDivisionId,
                        principalTable: "AirDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ForestryId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AreaHa = table.Column<double>(type: "double precision", nullable: false),
                    DetectedBy = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ExtinguishedBy = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fires_Forestries_ForestryId",
                        column: x => x.ForestryId,
                        principalTable: "Forestries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportInstanceId = table.Column<int>(type: "integer", nullable: false),
                    RowKey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ValuesJson = table.Column<string>(type: "text", nullable: false),
                    ComputedSnapshotJson = table.Column<string>(type: "text", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportRows_ReportInstances_ReportInstanceId",
                        column: x => x.ReportInstanceId,
                        principalTable: "ReportInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirDivisions_RegionId",
                table: "AirDivisions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Fires_ForestryId",
                table: "Fires",
                column: "ForestryId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightHours_AirDivisionId",
                table: "FlightHours",
                column: "AirDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Forestries_RegionId",
                table: "Forestries",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormConfigs_FormKey_Version",
                table: "FormConfigs",
                columns: new[] { "FormKey", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportInstances_ConfigVersionId",
                table: "ReportInstances",
                column: "ConfigVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportInstances_FormKey_Year_RegionId",
                table: "ReportInstances",
                columns: new[] { "FormKey", "Year", "RegionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportInstances_RegionId",
                table: "ReportInstances",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_ReportInstanceId_RowKey",
                table: "ReportRows",
                columns: new[] { "ReportInstanceId", "RowKey" },
                unique: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fires");

            migrationBuilder.DropTable(
                name: "FlightHours");

            migrationBuilder.DropTable(
                name: "ReportRows");

            migrationBuilder.DropTable(
                name: "ValidationRules");

            migrationBuilder.DropTable(
                name: "Forestries");

            migrationBuilder.DropTable(
                name: "AirDivisions");

            migrationBuilder.DropTable(
                name: "ReportInstances");

            migrationBuilder.DropTable(
                name: "FormConfigs");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
