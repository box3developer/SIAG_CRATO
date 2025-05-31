using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDatasEquipamentoManutencao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dt_fim",
                table: "equipamentomanutencao");

            migrationBuilder.DropColumn(
                name: "dt_inicio",
                table: "equipamentomanutencao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dt_fim",
                table: "equipamentomanutencao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "dt_inicio",
                table: "equipamentomanutencao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
