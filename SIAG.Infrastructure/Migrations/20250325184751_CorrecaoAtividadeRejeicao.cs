using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoAtividadeRejeicao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nm_atividade_rejeicao",
                table: "atividaderejeicao",
                newName: "nm_atividaderejeicao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nm_atividaderejeicao",
                table: "atividaderejeicao",
                newName: "nm_atividade_rejeicao");
        }
    }
}
