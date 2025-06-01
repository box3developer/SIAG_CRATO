using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoricoPalletRemoveEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_evento",
                table: "historicopallet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_evento",
                table: "historicopallet",
                type: "int",
                nullable: true);
        }
    }
}
