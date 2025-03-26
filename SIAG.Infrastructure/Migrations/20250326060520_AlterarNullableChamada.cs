using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterarNullableChamada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemdestino",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemleitura",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemorigem",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_atividade_id_atividade",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_atividade_id_atividaderejeicao",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_chamada_id_chamadaorigem",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_chamada_id_chamadasuspensa",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_equipamento_id_equipamento",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_operador_id_operador",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_pallet_id_palletdestino",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_pallet_id_palletleitura",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_pallet_id_palletorigem",
                table: "chamada");

            migrationBuilder.AlterColumn<bool>(
                name: "priorizar",
                table: "chamada",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "id_palletorigem",
                table: "chamada",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id_palletleitura",
                table: "chamada",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id_palletdestino",
                table: "chamada",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "id_operador",
                table: "chamada",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "id_equipamento",
                table: "chamada",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_chamadasuspensa",
                table: "chamada",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_chamadaorigem",
                table: "chamada",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "id_atividaderejeicao",
                table: "chamada",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "id_atividade",
                table: "chamada",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "id_areaarmazenagemorigem",
                table: "chamada",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "id_areaarmazenagemleitura",
                table: "chamada",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "id_areaarmazenagemdestino",
                table: "chamada",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_rejeitada",
                table: "chamada",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_recebida",
                table: "chamada",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_finalizada",
                table: "chamada",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_chamada",
                table: "chamada",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_atendida",
                table: "chamada",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemdestino",
                table: "chamada",
                column: "id_areaarmazenagemdestino",
                principalTable: "areaarmazenagem",
                principalColumn: "id_areaarmazenagem");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemleitura",
                table: "chamada",
                column: "id_areaarmazenagemleitura",
                principalTable: "areaarmazenagem",
                principalColumn: "id_areaarmazenagem");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemorigem",
                table: "chamada",
                column: "id_areaarmazenagemorigem",
                principalTable: "areaarmazenagem",
                principalColumn: "id_areaarmazenagem");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_atividade_id_atividade",
                table: "chamada",
                column: "id_atividade",
                principalTable: "atividade",
                principalColumn: "id_atividade");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_atividade_id_atividaderejeicao",
                table: "chamada",
                column: "id_atividaderejeicao",
                principalTable: "atividade",
                principalColumn: "id_atividade");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_chamada_id_chamadaorigem",
                table: "chamada",
                column: "id_chamadaorigem",
                principalTable: "chamada",
                principalColumn: "id_chamada");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_chamada_id_chamadasuspensa",
                table: "chamada",
                column: "id_chamadasuspensa",
                principalTable: "chamada",
                principalColumn: "id_chamada");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_equipamento_id_equipamento",
                table: "chamada",
                column: "id_equipamento",
                principalTable: "equipamento",
                principalColumn: "id_equipamento");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_operador_id_operador",
                table: "chamada",
                column: "id_operador",
                principalTable: "operador",
                principalColumn: "id_operador");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_pallet_id_palletdestino",
                table: "chamada",
                column: "id_palletdestino",
                principalTable: "pallet",
                principalColumn: "id_pallet");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_pallet_id_palletleitura",
                table: "chamada",
                column: "id_palletleitura",
                principalTable: "pallet",
                principalColumn: "id_pallet");

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_pallet_id_palletorigem",
                table: "chamada",
                column: "id_palletorigem",
                principalTable: "pallet",
                principalColumn: "id_pallet");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemdestino",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemleitura",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemorigem",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_atividade_id_atividade",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_atividade_id_atividaderejeicao",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_chamada_id_chamadaorigem",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_chamada_id_chamadasuspensa",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_equipamento_id_equipamento",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_operador_id_operador",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_pallet_id_palletdestino",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_pallet_id_palletleitura",
                table: "chamada");

            migrationBuilder.DropForeignKey(
                name: "FK_chamada_pallet_id_palletorigem",
                table: "chamada");

            migrationBuilder.AlterColumn<bool>(
                name: "priorizar",
                table: "chamada",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_palletorigem",
                table: "chamada",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_palletleitura",
                table: "chamada",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_palletdestino",
                table: "chamada",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "id_operador",
                table: "chamada",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_equipamento",
                table: "chamada",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_chamadasuspensa",
                table: "chamada",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "id_chamadaorigem",
                table: "chamada",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_atividaderejeicao",
                table: "chamada",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_atividade",
                table: "chamada",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "id_areaarmazenagemorigem",
                table: "chamada",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "id_areaarmazenagemleitura",
                table: "chamada",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "id_areaarmazenagemdestino",
                table: "chamada",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_rejeitada",
                table: "chamada",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_recebida",
                table: "chamada",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_finalizada",
                table: "chamada",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_chamada",
                table: "chamada",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dt_atendida",
                table: "chamada",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemdestino",
                table: "chamada",
                column: "id_areaarmazenagemdestino",
                principalTable: "areaarmazenagem",
                principalColumn: "id_areaarmazenagem",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemleitura",
                table: "chamada",
                column: "id_areaarmazenagemleitura",
                principalTable: "areaarmazenagem",
                principalColumn: "id_areaarmazenagem",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_areaarmazenagem_id_areaarmazenagemorigem",
                table: "chamada",
                column: "id_areaarmazenagemorigem",
                principalTable: "areaarmazenagem",
                principalColumn: "id_areaarmazenagem",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_atividade_id_atividade",
                table: "chamada",
                column: "id_atividade",
                principalTable: "atividade",
                principalColumn: "id_atividade",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_atividade_id_atividaderejeicao",
                table: "chamada",
                column: "id_atividaderejeicao",
                principalTable: "atividade",
                principalColumn: "id_atividade",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_chamada_id_chamadaorigem",
                table: "chamada",
                column: "id_chamadaorigem",
                principalTable: "chamada",
                principalColumn: "id_chamada",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_chamada_id_chamadasuspensa",
                table: "chamada",
                column: "id_chamadasuspensa",
                principalTable: "chamada",
                principalColumn: "id_chamada",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_equipamento_id_equipamento",
                table: "chamada",
                column: "id_equipamento",
                principalTable: "equipamento",
                principalColumn: "id_equipamento",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_operador_id_operador",
                table: "chamada",
                column: "id_operador",
                principalTable: "operador",
                principalColumn: "id_operador",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_pallet_id_palletdestino",
                table: "chamada",
                column: "id_palletdestino",
                principalTable: "pallet",
                principalColumn: "id_pallet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_pallet_id_palletleitura",
                table: "chamada",
                column: "id_palletleitura",
                principalTable: "pallet",
                principalColumn: "id_pallet",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chamada_pallet_id_palletorigem",
                table: "chamada",
                column: "id_palletorigem",
                principalTable: "pallet",
                principalColumn: "id_pallet",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
