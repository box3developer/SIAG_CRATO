using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgrupadorAtivo",
                columns: table => new
                {
                    AgrupadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TpAgrupamento = table.Column<int>(type: "int", nullable: false),
                    Codigo1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CdSequencia = table.Column<int>(type: "int", nullable: false),
                    DtAgrupador = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AreaArmazenagemId = table.Column<int>(type: "int", nullable: false),
                    FgStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgrupadorAtivo", x => x.AgrupadorId);
                });

            migrationBuilder.CreateTable(
                name: "Deposito",
                columns: table => new
                {
                    DepositoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NmDeposito = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposito", x => x.DepositoId);
                });

            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    EquipamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipamentoModeloId = table.Column<int>(type: "int", nullable: false),
                    SetorTrabalhoId = table.Column<int>(type: "int", nullable: false),
                    OperadorId = table.Column<int>(type: "int", nullable: false),
                    NmEquipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NmIdentificador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FgStatus = table.Column<int>(type: "int", nullable: false),
                    DtInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtManutencao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NmObservacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CdUltimaLeitura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtUltimaLeitura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    NmIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FgStatusTrocaCaracol = table.Column<int>(type: "int", nullable: false),
                    NmAbreviadoEquipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CdLeituraPendente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NmUsuarioLiberacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.EquipamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportadoraId = table.Column<int>(type: "int", nullable: false),
                    CdPedido = table.Column<int>(type: "int", nullable: false),
                    CdLote = table.Column<int>(type: "int", nullable: false),
                    CdBox = table.Column<int>(type: "int", nullable: false),
                    CdCliente = table.Column<int>(type: "int", nullable: false),
                    CdEstabelecimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotaFiscalId = table.Column<int>(type: "int", nullable: false),
                    CdCanal = table.Column<int>(type: "int", nullable: false),
                    CdOrdemExportacao = table.Column<int>(type: "int", nullable: false),
                    CdVeiculoExportacao = table.Column<int>(type: "int", nullable: false),
                    TpCarga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TpCargaAglutinado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CdRota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtCaixa = table.Column<int>(type: "int", nullable: false),
                    QtCubagemCaixa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtAcessorio = table.Column<int>(type: "int", nullable: false),
                    QtCubagemAcessorio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtDisplay = table.Column<int>(type: "int", nullable: false),
                    QtCubagemDisplay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtExpositores = table.Column<int>(type: "int", nullable: false),
                    QtCubagemExpositores = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FgStatus = table.Column<int>(type: "int", nullable: false),
                    DtImportacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CdOrdemExportacaoDefinitiva = table.Column<int>(type: "int", nullable: false),
                    CdVeiculoExportacaoDefinitiva = table.Column<int>(type: "int", nullable: false),
                    DtPrevisaoExportacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtImplantacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtPredata = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FgSku = table.Column<int>(type: "int", nullable: false),
                    CdSequenciaExpedicao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "Programa",
                columns: table => new
                {
                    ProgramaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CdPrograma = table.Column<int>(type: "int", nullable: false),
                    CdDocumento = table.Column<int>(type: "int", nullable: false),
                    CdFabrica = table.Column<int>(type: "int", nullable: false),
                    CdEstabelecimento = table.Column<int>(type: "int", nullable: false),
                    CdEquipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DtLiberacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FgTipo = table.Column<int>(type: "int", nullable: false),
                    CdDeposito = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtAlturaCaixa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtLarguraCaixa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QtComprimentoCaixa = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programa", x => x.ProgramaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoArea",
                columns: table => new
                {
                    TipoAreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NmTipoArea = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoArea", x => x.TipoAreaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoEndereco",
                columns: table => new
                {
                    TipoEnderecoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NmTipoEndereco = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEndereco", x => x.TipoEnderecoId);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    TurnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CdTurno = table.Column<int>(type: "int", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaAnterior = table.Column<bool>(type: "bit", nullable: false),
                    DiaSucessor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.TurnoId);
                });

            migrationBuilder.CreateTable(
                name: "Caixa",
                columns: table => new
                {
                    CaixaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AgrupadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PalletId = table.Column<int>(type: "int", nullable: false),
                    ProgramaId = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    CdProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CdCor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CdGrudeTamanho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NrCaixa = table.Column<int>(type: "int", nullable: false),
                    NrPares = table.Column<int>(type: "int", nullable: false),
                    FgRfid = table.Column<bool>(type: "bit", nullable: false),
                    FgStatus = table.Column<int>(type: "int", nullable: false),
                    DtEmbalagem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtSorter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtEstufamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtExpedicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QtPeso = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.CaixaId);
                    table.ForeignKey(
                        name: "FK_Caixa_AgrupadorAtivo_AgrupadorId",
                        column: x => x.AgrupadorId,
                        principalTable: "AgrupadorAtivo",
                        principalColumn: "AgrupadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegiaoTrabalho",
                columns: table => new
                {
                    RegiaoTrabalhoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositoId = table.Column<int>(type: "int", nullable: false),
                    NmRegiaoTrabalho = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegiaoTrabalho", x => x.RegiaoTrabalhoId);
                    table.ForeignKey(
                        name: "FK_RegiaoTrabalho_Deposito_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Deposito",
                        principalColumn: "DepositoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SetorTrabalho",
                columns: table => new
                {
                    SetorTrabalhoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositoId = table.Column<int>(type: "int", nullable: false),
                    NmSetorTrabalho = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetorTrabalho", x => x.SetorTrabalhoId);
                    table.ForeignKey(
                        name: "FK_SetorTrabalho_Deposito_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Deposito",
                        principalColumn: "DepositoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegiaoTrabalhoId = table.Column<int>(type: "int", nullable: false),
                    SetorTrabalhoId = table.Column<int>(type: "int", nullable: false),
                    TipoEnderecoId = table.Column<int>(type: "int", nullable: false),
                    NmEndereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtEstoqueMinimo = table.Column<int>(type: "int", nullable: false),
                    QtEstoqueMaximo = table.Column<int>(type: "int", nullable: false),
                    FgStatus = table.Column<int>(type: "int", nullable: false),
                    TpPreenchimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.EnderecoId);
                    table.ForeignKey(
                        name: "FK_Endereco_RegiaoTrabalho_RegiaoTrabalhoId",
                        column: x => x.RegiaoTrabalhoId,
                        principalTable: "RegiaoTrabalho",
                        principalColumn: "RegiaoTrabalhoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Endereco_SetorTrabalho_SetorTrabalhoId",
                        column: x => x.SetorTrabalhoId,
                        principalTable: "SetorTrabalho",
                        principalColumn: "SetorTrabalhoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Endereco_TipoEndereco_TipoEnderecoId",
                        column: x => x.TipoEnderecoId,
                        principalTable: "TipoEndereco",
                        principalColumn: "TipoEnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreaArmazenagem",
                columns: table => new
                {
                    AreaArmazenagemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAreaId = table.Column<int>(type: "int", nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    AgrupadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NrPosicaoX = table.Column<int>(type: "int", nullable: false),
                    NrPosicaoY = table.Column<int>(type: "int", nullable: false),
                    NrLado = table.Column<int>(type: "int", nullable: false),
                    FgStatus = table.Column<int>(type: "int", nullable: false),
                    CdIdentificacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgrupadorReservadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaArmazenagem", x => x.AreaArmazenagemId);
                    table.ForeignKey(
                        name: "FK_AreaArmazenagem_AgrupadorAtivo_AgrupadorId",
                        column: x => x.AgrupadorId,
                        principalTable: "AgrupadorAtivo",
                        principalColumn: "AgrupadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreaArmazenagem_AgrupadorAtivo_AgrupadorReservadoId",
                        column: x => x.AgrupadorReservadoId,
                        principalTable: "AgrupadorAtivo",
                        principalColumn: "AgrupadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreaArmazenagem_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AreaArmazenagem_TipoArea_TipoAreaId",
                        column: x => x.TipoAreaId,
                        principalTable: "TipoArea",
                        principalColumn: "TipoAreaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pallet",
                columns: table => new
                {
                    PalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaArmazenagemId = table.Column<int>(type: "int", nullable: false),
                    AgrupadorAtivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FgStatus = table.Column<int>(type: "int", nullable: false),
                    QtUtilizacao = table.Column<int>(type: "int", nullable: false),
                    DtUltimaMovimentacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CdIdentificacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pallet", x => x.PalletId);
                    table.ForeignKey(
                        name: "FK_Pallet_AgrupadorAtivo_AgrupadorAtivoId",
                        column: x => x.AgrupadorAtivoId,
                        principalTable: "AgrupadorAtivo",
                        principalColumn: "AgrupadorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pallet_AreaArmazenagem_AreaArmazenagemId",
                        column: x => x.AreaArmazenagemId,
                        principalTable: "AreaArmazenagem",
                        principalColumn: "AreaArmazenagemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaArmazenagem_AgrupadorId",
                table: "AreaArmazenagem",
                column: "AgrupadorId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaArmazenagem_AgrupadorReservadoId",
                table: "AreaArmazenagem",
                column: "AgrupadorReservadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaArmazenagem_EnderecoId",
                table: "AreaArmazenagem",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaArmazenagem_TipoAreaId",
                table: "AreaArmazenagem",
                column: "TipoAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_AgrupadorId",
                table: "Caixa",
                column: "AgrupadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_RegiaoTrabalhoId",
                table: "Endereco",
                column: "RegiaoTrabalhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_SetorTrabalhoId",
                table: "Endereco",
                column: "SetorTrabalhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_TipoEnderecoId",
                table: "Endereco",
                column: "TipoEnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pallet_AgrupadorAtivoId",
                table: "Pallet",
                column: "AgrupadorAtivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pallet_AreaArmazenagemId",
                table: "Pallet",
                column: "AreaArmazenagemId");

            migrationBuilder.CreateIndex(
                name: "IX_RegiaoTrabalho_DepositoId",
                table: "RegiaoTrabalho",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_SetorTrabalho_DepositoId",
                table: "SetorTrabalho",
                column: "DepositoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caixa");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "Pallet");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Programa");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "AreaArmazenagem");

            migrationBuilder.DropTable(
                name: "AgrupadorAtivo");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "TipoArea");

            migrationBuilder.DropTable(
                name: "RegiaoTrabalho");

            migrationBuilder.DropTable(
                name: "SetorTrabalho");

            migrationBuilder.DropTable(
                name: "TipoEndereco");

            migrationBuilder.DropTable(
                name: "Deposito");
        }
    }
}
