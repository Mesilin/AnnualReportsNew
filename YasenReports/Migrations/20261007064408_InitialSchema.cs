using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace YasenReports.Api.Migrations
{
    public partial class InitialSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "FederalDistricts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FederalDistricts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Config = table.Column<JsonElement>(type: "jsonb", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidationRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    FormCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    LeftExpression = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightExpression = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightFormCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Operator = table.Column<int>(type: "integer", nullable: false),
                    Tolerance = table.Column<decimal>(type: "numeric", nullable: false),
                    IsBlocking = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ForestFundAreaThousandHa = table.Column<decimal>(type: "numeric", nullable: true),
                    Sorting = table.Column<int>(type: "integer", nullable: true),
                    FederalDistrictId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_FederalDistricts_FederalDistrictId",
                        column: x => x.FederalDistrictId,
                        principalTable: "FederalDistricts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Airbases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airbases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airbases_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormCode = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    RowKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FormConfigId = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<JsonElement>(type: "jsonb", nullable: false),
                    ValidationJson = table.Column<JsonElement>(type: "jsonb", nullable: true),
                    CreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_FormConfigs_FormConfigId",
                        column: x => x.FormConfigId,
                        principalTable: "FormConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SourceSnapshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ColumnKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    CapturedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceSnapshots_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airbases_SubjectId",
                table: "Airbases",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FederalDistricts_Code",
                table: "FederalDistricts",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormConfigs_Code_Year",
                table: "FormConfigs",
                columns: new[] { "Code", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_FormCode_Year_SubjectId_RowKey",
                table: "Reports",
                columns: new[] { "FormCode", "Year", "SubjectId", "RowKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_FormConfigId",
                table: "Reports",
                column: "FormConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_SubjectId",
                table: "Reports",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceSnapshots_SubjectId",
                table: "SourceSnapshots",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SourceSnapshots_Year_SubjectId_ColumnKey",
                table: "SourceSnapshots",
                columns: new[] { "Year", "SubjectId", "ColumnKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Code",
                table: "Subjects",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_FederalDistrictId",
                table: "Subjects",
                column: "FederalDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidationRules_Year_FormCode",
                table: "ValidationRules",
                columns: new[] { "Year", "FormCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airbases");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "SourceSnapshots");

            migrationBuilder.DropTable(
                name: "ValidationRules");

            migrationBuilder.DropTable(
                name: "FormConfigs");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "FederalDistricts");
        }
    }
}
