using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaHistorico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "historico",
                columns: table => new
                {
                    dt_historico = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nm_usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_objeto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_registro = table.Column<int>(type: "int", nullable: true),
                    ds_operacao = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historico");
        }
    }
}
