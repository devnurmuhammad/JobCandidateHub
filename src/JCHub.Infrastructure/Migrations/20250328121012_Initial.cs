using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JCHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enum_state",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enum_state", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "doc_candidate",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    email = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    preferred_call_time_from = table.Column<TimeSpan>(type: "interval", nullable: true),
                    preferred_call_time_to = table.Column<TimeSpan>(type: "interval", nullable: true),
                    linkedin_profile = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    github_profile = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    comment = table.Column<string>(type: "text", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_user_id = table.Column<int>(type: "integer", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modified_user_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doc_candidate", x => x.id);
                    table.ForeignKey(
                        name: "FK_doc_candidate_enum_state_state_id",
                        column: x => x.state_id,
                        principalTable: "enum_state",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "enum_state",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Passive" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_doc_candidate_email",
                table: "doc_candidate",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_doc_candidate_state_id",
                table: "doc_candidate",
                column: "state_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "doc_candidate");

            migrationBuilder.DropTable(
                name: "enum_state");
        }
    }
}
