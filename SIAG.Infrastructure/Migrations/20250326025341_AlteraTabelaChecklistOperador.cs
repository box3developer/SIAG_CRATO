using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlteraTabelaChecklistOperador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "equipamentochecklistoperador");

            migrationBuilder.CreateTable(
                name: "equipamentochecklistoperador2",
                columns: table => new
                {
                    id_equipamentochecklistoperador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_equipamento = table.Column<int>(type: "int", nullable: false),
                    id_operador = table.Column<long>(type: "bigint", nullable: false),
                    id_equipamentochecklist = table.Column<int>(type: "int", nullable: false),
                    fg_resposta = table.Column<bool>(type: "bit", nullable: false),
                    dt_checklist = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentochecklistoperador2", x => x.id_equipamentochecklistoperador);
                    table.ForeignKey(
                        name: "FK_equipamentochecklistoperador2_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_equipamentochecklistoperador2_equipamentochecklist_id_equipamentochecklist",
                        column: x => x.id_equipamentochecklist,
                        principalTable: "equipamentochecklist",
                        principalColumn: "id_equipamentochecklist",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_equipamentochecklistoperador2_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_equipamentochecklistoperador2_id_equipamento",
                table: "equipamentochecklistoperador2",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_equipamentochecklistoperador2_id_equipamentochecklist",
                table: "equipamentochecklistoperador2",
                column: "id_equipamentochecklist");

            migrationBuilder.CreateIndex(
                name: "IX_equipamentochecklistoperador2_id_operador",
                table: "equipamentochecklistoperador2",
                column: "id_operador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "equipamentochecklistoperador2");

            migrationBuilder.CreateTable(
                name: "equipamentochecklistoperador",
                columns: table => new
                {
                    id_equipamentochecklistoperador = table.Column<int>(type: "int", nullable: false),
                    id_equipamento = table.Column<int>(type: "int", nullable: false),
                    id_equipamentochecklist = table.Column<int>(type: "int", nullable: false),
                    id_operador = table.Column<long>(type: "bigint", nullable: false),
                    dt_checklist = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fg_resposta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentochecklistoperador", x => x.id_equipamentochecklistoperador);
                    table.ForeignKey(
                        name: "FK_equipamentochecklistoperador_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_equipamentochecklistoperador_equipamentochecklist_id_equipamentochecklist",
                        column: x => x.id_equipamentochecklist,
                        principalTable: "equipamentochecklist",
                        principalColumn: "id_equipamentochecklist",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_equipamentochecklistoperador_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_equipamentochecklistoperador_id_equipamento",
                table: "equipamentochecklistoperador",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_equipamentochecklistoperador_id_equipamentochecklist",
                table: "equipamentochecklistoperador",
                column: "id_equipamentochecklist");

            migrationBuilder.CreateIndex(
                name: "IX_equipamentochecklistoperador_id_operador",
                table: "equipamentochecklistoperador",
                column: "id_operador");
        }
    }
}
