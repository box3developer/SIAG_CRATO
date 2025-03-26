using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChamadaTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chamadatarefa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chamadatarefa",
                columns: table => new
                {
                    id_chamada = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_tarefa = table.Column<int>(type: "int", nullable: false),
                    dt_fim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_inicio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_chamadatarefa_atividadetarefa_id_tarefa",
                        column: x => x.id_tarefa,
                        principalTable: "atividadetarefa",
                        principalColumn: "id_tarefa",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_chamadatarefa_id_tarefa",
                table: "chamadatarefa",
                column: "id_tarefa");
        }
    }
}
