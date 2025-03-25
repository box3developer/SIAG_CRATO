using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoDesempenhoOnline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "qt_realizada {get",
                table: "desempenhoonline",
                newName: "qt_realizada");

            migrationBuilder.RenameColumn(
                name: "qt_prevista {get",
                table: "desempenhoonline",
                newName: "qt_prevista");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "qt_realizada",
                table: "desempenhoonline",
                newName: "qt_realizada {get");

            migrationBuilder.RenameColumn(
                name: "qt_prevista",
                table: "desempenhoonline",
                newName: "qt_prevista {get");
        }
    }
}
