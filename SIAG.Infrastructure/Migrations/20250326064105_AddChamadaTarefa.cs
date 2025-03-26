using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChamadaTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_tarefa",
                table: "chamadatarefa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "atividadetarefa",
                columns: table => new
                {
                    id_tarefa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_tarefa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_atividade = table.Column<int>(type: "int", nullable: false),
                    cd_sequencia = table.Column<int>(type: "int", nullable: false),
                    fg_recurso = table.Column<int>(type: "int", nullable: true),
                    id_atividaderotina = table.Column<int>(type: "int", nullable: false),
                    qt_potencianormal = table.Column<int>(type: "int", nullable: false),
                    qt_potenciaaumentada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividadetarefa", x => x.id_tarefa);
                    table.ForeignKey(
                        name: "FK_atividadetarefa_atividaderotina_id_atividaderotina",
                        column: x => x.id_atividaderotina,
                        principalTable: "atividaderotina",
                        principalColumn: "id_atividaderotina",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chamadatarefa_id_tarefa",
                table: "chamadatarefa",
                column: "id_tarefa");

            migrationBuilder.CreateIndex(
                name: "IX_atividadetarefa_id_atividaderotina",
                table: "atividadetarefa",
                column: "id_atividaderotina");

            migrationBuilder.AddForeignKey(
                name: "FK_chamadatarefa_atividadetarefa_id_tarefa",
                table: "chamadatarefa",
                column: "id_tarefa",
                principalTable: "atividadetarefa",
                principalColumn: "id_tarefa",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chamadatarefa_atividadetarefa_id_tarefa",
                table: "chamadatarefa");

            migrationBuilder.DropTable(
                name: "atividadetarefa");

            migrationBuilder.DropIndex(
                name: "IX_chamadatarefa_id_tarefa",
                table: "chamadatarefa");

            migrationBuilder.DropColumn(
                name: "id_tarefa",
                table: "chamadatarefa");
        }
    }
}
