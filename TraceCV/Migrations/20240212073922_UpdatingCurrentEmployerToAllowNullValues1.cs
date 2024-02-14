using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraceCV.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingCurrentEmployerToAllowNullValues1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CurrentEmployer",
                table: "Experts",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Experts",
                keyColumn: "CurrentEmployer",
                keyValue: null,
                column: "CurrentEmployer",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentEmployer",
                table: "Experts",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
