using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacaturesApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacatures",
                columns: table => new
                {
                    VacatureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlSlug = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FunctionTitle = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Availability = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhatToExpect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsibilities = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Offer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryRange = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Industry = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ListPriority = table.Column<int>(type: "int", nullable: true),
                    Hidden = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacatures", x => x.VacatureId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacatures");
        }
    }
}
