using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetWebSite.Migrations
{
    /// <inheritdoc />
    public partial class AddFindRiscResultsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FindRiscResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalRiskPoints = table.Column<int>(type: "int", nullable: false),
                    DegreeOfRisk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenYearRiskRating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoutineScreening = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FindRiscResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    FindRiscResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswer_FindRiscResults_FindRiscResultId",
                        column: x => x.FindRiscResultId,
                        principalTable: "FindRiscResults",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BloodPressures",
                columns: table => new
                {
                    BloodPressureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Systolic = table.Column<int>(type: "int", nullable: false),
                    Diastolic = table.Column<int>(type: "int", nullable: false),
                    MeasurementTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressures", x => x.BloodPressureId);
                    table.ForeignKey(
                        name: "FK_BloodPressures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodSugars",
                columns: table => new
                {
                    BloodSugarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MeasurementValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasurementTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodSugars", x => x.BloodSugarId);
                    table.ForeignKey(
                        name: "FK_BloodSugars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BodyMassIndexes",
                columns: table => new
                {
                    BodyMassIndexId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    HeightCm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BMICalculated = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasurementTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMassIndexes", x => x.BodyMassIndexId);
                    table.ForeignKey(
                        name: "FK_BodyMassIndexes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiabetesControls",
                columns: table => new
                {
                    DiabetesControlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Hba1c = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasurementTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiabetesControls", x => x.DiabetesControlId);
                    table.ForeignKey(
                        name: "FK_DiabetesControls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressures_UserId",
                table: "BloodPressures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodSugars_UserId",
                table: "BloodSugars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyMassIndexes_UserId",
                table: "BodyMassIndexes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiabetesControls_UserId",
                table: "DiabetesControls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_FindRiscResultId",
                table: "UserAnswer",
                column: "FindRiscResultId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodPressures");

            migrationBuilder.DropTable(
                name: "BloodSugars");

            migrationBuilder.DropTable(
                name: "BodyMassIndexes");

            migrationBuilder.DropTable(
                name: "DiabetesControls");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FindRiscResults");
        }
    }
}
