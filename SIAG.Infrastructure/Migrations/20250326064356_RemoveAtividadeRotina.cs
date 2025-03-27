using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAtividadeRotina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_atividadetarefa_atividaderotina_id_atividaderotina",
                table: "atividadetarefa");

            migrationBuilder.DropTable(
                name: "atividaderotina");

            migrationBuilder.DropIndex(
                name: "IX_atividadetarefa_id_atividaderotina",
                table: "atividadetarefa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "atividaderotina",
                columns: table => new
                {
                    id_atividaderotina = table.Column<int>(type: "int", nullable: false),
                    fg_tipo = table.Column<int>(type: "int", nullable: false),
                    nm_atividaderotina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_procedure = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividaderotina", x => x.id_atividaderotina);
                });

            migrationBuilder.CreateIndex(
                name: "IX_atividadetarefa_id_atividaderotina",
                table: "atividadetarefa",
                column: "id_atividaderotina");

            migrationBuilder.AddForeignKey(
                name: "FK_atividadetarefa_atividaderotina_id_atividaderotina",
                table: "atividadetarefa",
                column: "id_atividaderotina",
                principalTable: "atividaderotina",
                principalColumn: "id_atividaderotina",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
