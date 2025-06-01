using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoricoPallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "historicopallet",
                columns: table => new
                {
                    id_historicopallet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_responsabilidade = table.Column<int>(type: "int", nullable: true),
                    id_atividade = table.Column<int>(type: "int", nullable: true),
                    id_chamada = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_areaarmazenagem = table.Column<long>(type: "bigint", nullable: true),
                    id_evento = table.Column<int>(type: "int", nullable: true),
                    id_pallet = table.Column<int>(type: "int", nullable: true),
                    id_operador = table.Column<long>(type: "bigint", nullable: true),
                    nm_historico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historicopallet", x => x.id_historicopallet);
                    table.ForeignKey(
                        name: "FK_historicopallet_areaarmazenagem_id_areaarmazenagem",
                        column: x => x.id_areaarmazenagem,
                        principalTable: "areaarmazenagem",
                        principalColumn: "id_areaarmazenagem");
                    table.ForeignKey(
                        name: "FK_historicopallet_atividade_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividade",
                        principalColumn: "id_atividade");
                    table.ForeignKey(
                        name: "FK_historicopallet_chamada_id_chamada",
                        column: x => x.id_chamada,
                        principalTable: "chamada",
                        principalColumn: "id_chamada");
                    table.ForeignKey(
                        name: "FK_historicopallet_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador");
                    table.ForeignKey(
                        name: "FK_historicopallet_pallet_id_pallet",
                        column: x => x.id_pallet,
                        principalTable: "pallet",
                        principalColumn: "id_pallet");
                });

            migrationBuilder.CreateIndex(
                name: "IX_historicopallet_id_areaarmazenagem",
                table: "historicopallet",
                column: "id_areaarmazenagem");

            migrationBuilder.CreateIndex(
                name: "IX_historicopallet_id_atividade",
                table: "historicopallet",
                column: "id_atividade");

            migrationBuilder.CreateIndex(
                name: "IX_historicopallet_id_chamada",
                table: "historicopallet",
                column: "id_chamada");

            migrationBuilder.CreateIndex(
                name: "IX_historicopallet_id_operador",
                table: "historicopallet",
                column: "id_operador");

            migrationBuilder.CreateIndex(
                name: "IX_historicopallet_id_pallet",
                table: "historicopallet",
                column: "id_pallet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historicopallet");
        }
    }
}
