using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Remove2ChamadaTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atividadetarefa");

            migrationBuilder.CreateTable(
                name: "chamadatarefa",
                columns: table => new
                {
                    id_chamada = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dt_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_fim = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_chamadatarefa_chamada_id_chamada",
                        column: x => x.id_chamada,
                        principalTable: "chamada",
                        principalColumn: "id_chamada",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chamadatarefa_id_chamada",
                table: "chamadatarefa",
                column: "id_chamada");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chamadatarefa");

            migrationBuilder.CreateTable(
                name: "atividadetarefa",
                columns: table => new
                {
                    id_tarefa = table.Column<int>(type: "int", nullable: false),
                    id_atividaderotina = table.Column<int>(type: "int", nullable: false),
                    cd_sequencia = table.Column<int>(type: "int", nullable: false),
                    fg_recurso = table.Column<int>(type: "int", nullable: true),
                    id_atividade = table.Column<int>(type: "int", nullable: false),
                    nm_mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_tarefa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qt_potenciaaumentada = table.Column<int>(type: "int", nullable: false),
                    qt_potencianormal = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_atividadetarefa_id_atividaderotina",
                table: "atividadetarefa",
                column: "id_atividaderotina");
        }
    }
}
