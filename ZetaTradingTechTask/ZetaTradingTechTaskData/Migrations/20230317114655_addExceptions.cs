using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZetaTradingTechTaskData.Migrations
{
    /// <inheritdoc />
    public partial class addExceptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exceptions",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Parameters = table.Column<string>(type: "varchar(200)", nullable: false),
                    StackTrace = table.Column<string>(type: "varchar(300)", nullable: false),
                    Message = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exceptions", x => x.EventId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exceptions");
        }
    }
}
