using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraceCV.Migrations
{
    /// <inheritdoc />
    public partial class AdditionOfExtraColumnsOnExpert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Experts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "experience",
                table: "Experts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "lastedit",
                table: "Experts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Experts");

            migrationBuilder.DropColumn(
                name: "experience",
                table: "Experts");

            migrationBuilder.DropColumn(
                name: "lastedit",
                table: "Experts");
        }
    }
}
