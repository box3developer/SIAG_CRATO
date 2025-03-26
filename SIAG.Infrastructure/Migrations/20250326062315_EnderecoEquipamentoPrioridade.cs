using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnderecoEquipamentoPrioridade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_equipamento_endereco",
                table: "equipamentoenderecoprioridade",
                newName: "id_equipamentoendereco");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_equipamentoendereco",
                table: "equipamentoenderecoprioridade",
                newName: "id_equipamento_endereco");
        }
    }
}
