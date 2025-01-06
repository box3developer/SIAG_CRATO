using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agrupadorativo",
                columns: table => new
                {
                    id_agrupador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tp_agrupamento = table.Column<int>(type: "int", nullable: false),
                    codigo1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codigo2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    codigo3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cd_sequencia = table.Column<int>(type: "int", nullable: false),
                    dt_agrupador = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_area_armazenagem = table.Column<int>(type: "int", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agrupadorativo", x => x.id_agrupador);
                });

            migrationBuilder.CreateTable(
                name: "deposito",
                columns: table => new
                {
                    id_deposito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_deposito = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deposito", x => x.id_deposito);
                });

            migrationBuilder.CreateTable(
                name: "equipamento",
                columns: table => new
                {
                    id_equipamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_equipamento_modelo = table.Column<int>(type: "int", nullable: false),
                    id_setor_trabalho = table.Column<int>(type: "int", nullable: false),
                    id_operador = table.Column<int>(type: "int", nullable: false),
                    nm_equipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_identificador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    dt_inclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_manutencao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nm_observacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cd_ultima_leitura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dt_ultima_leitura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endereco_id = table.Column<int>(type: "int", nullable: false),
                    nm_ip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fg_status_troca_caracol = table.Column<int>(type: "int", nullable: false),
                    nm_abreviado_equipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cd_leitura_pendente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_usuario_liberacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamento", x => x.id_equipamento);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    id_pedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_transportadora = table.Column<int>(type: "int", nullable: false),
                    cd_pedido = table.Column<int>(type: "int", nullable: false),
                    cd_lote = table.Column<int>(type: "int", nullable: false),
                    cd_box = table.Column<int>(type: "int", nullable: false),
                    cd_cliente = table.Column<int>(type: "int", nullable: false),
                    cd_estabelecimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_nota_fiscal = table.Column<int>(type: "int", nullable: false),
                    cd_canal = table.Column<int>(type: "int", nullable: false),
                    cd_ordem_exportacao = table.Column<int>(type: "int", nullable: false),
                    cd_veiculo_exportacao = table.Column<int>(type: "int", nullable: false),
                    tp_carga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tp_carga_aglutinado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cd_rota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qt_caixa = table.Column<int>(type: "int", nullable: false),
                    qt_cubagem_caixa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    qt_acessorio = table.Column<int>(type: "int", nullable: false),
                    qt_cubagem_acessorio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    qt_display = table.Column<int>(type: "int", nullable: false),
                    qt_cubagem_display = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    qt_expositores = table.Column<int>(type: "int", nullable: false),
                    qt_cubagem_expositores = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    dt_importacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cd_ordem_exportacao_definitiva = table.Column<int>(type: "int", nullable: false),
                    cd_veiculo_exportacao_definitiva = table.Column<int>(type: "int", nullable: false),
                    dt_previsao_exportacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_implantacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_predata = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fg_sku = table.Column<int>(type: "int", nullable: false),
                    cd_sequencia_expedicao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.id_pedido);
                });

            migrationBuilder.CreateTable(
                name: "programa",
                columns: table => new
                {
                    id_programa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cd_programa = table.Column<int>(type: "int", nullable: false),
                    cd_documento = table.Column<int>(type: "int", nullable: false),
                    cd_fabrica = table.Column<int>(type: "int", nullable: false),
                    cd_estabelecimento = table.Column<int>(type: "int", nullable: false),
                    cd_equipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dt_liberacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fg_tipo = table.Column<int>(type: "int", nullable: false),
                    cd_deposito = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qt_altura_caixa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    qt_largura_caixa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    qt_comprimento_caixa = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programa", x => x.id_programa);
                });

            migrationBuilder.CreateTable(
                name: "tipoarea",
                columns: table => new
                {
                    id_tipo_area = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_tipo_area = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoarea", x => x.id_tipo_area);
                });

            migrationBuilder.CreateTable(
                name: "tipoendereco",
                columns: table => new
                {
                    id_tipo_endereco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_tipo_endereco = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoendereco", x => x.id_tipo_endereco);
                });

            migrationBuilder.CreateTable(
                name: "turno",
                columns: table => new
                {
                    id_turno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cd_turno = table.Column<int>(type: "int", nullable: false),
                    dt_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_fim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dia_anterior = table.Column<bool>(type: "bit", nullable: false),
                    dia_sucessor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turno", x => x.id_turno);
                });

            migrationBuilder.CreateTable(
                name: "caixa",
                columns: table => new
                {
                    id_caixa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    agrupador_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_pallet = table.Column<int>(type: "int", nullable: false),
                    id_programa = table.Column<int>(type: "int", nullable: false),
                    id_pedido = table.Column<int>(type: "int", nullable: false),
                    cd_produto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cd_cor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cd_grude_tamanho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nr_caixa = table.Column<int>(type: "int", nullable: false),
                    nr_pares = table.Column<int>(type: "int", nullable: false),
                    id_fg_rf = table.Column<bool>(type: "bit", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    dt_embalagem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_sorter = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_estufamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_expedicao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    qt_peso = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caixa", x => x.id_caixa);
                    table.ForeignKey(
                        name: "FK_caixa_agrupadorativo_agrupador_id",
                        column: x => x.agrupador_id,
                        principalTable: "agrupadorativo",
                        principalColumn: "id_agrupador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "regiaotrabalho",
                columns: table => new
                {
                    id_regiao_trabalho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_deposito = table.Column<int>(type: "int", nullable: false),
                    nm_regiao_trabalho = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regiaotrabalho", x => x.id_regiao_trabalho);
                    table.ForeignKey(
                        name: "FK_regiaotrabalho_deposito_id_deposito",
                        column: x => x.id_deposito,
                        principalTable: "deposito",
                        principalColumn: "id_deposito",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "setortrabalho",
                columns: table => new
                {
                    id_setor_trabalho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_deposito = table.Column<int>(type: "int", nullable: false),
                    nm_setor_trabalho = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setortrabalho", x => x.id_setor_trabalho);
                    table.ForeignKey(
                        name: "FK_setortrabalho_deposito_id_deposito",
                        column: x => x.id_deposito,
                        principalTable: "deposito",
                        principalColumn: "id_deposito",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    id_endereco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_regiao_trabalho = table.Column<int>(type: "int", nullable: false),
                    id_setor_trabalho = table.Column<int>(type: "int", nullable: false),
                    id_tipo_endereco = table.Column<int>(type: "int", nullable: false),
                    nm_endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qt_estoque_minimo = table.Column<int>(type: "int", nullable: false),
                    qt_estoque_maximo = table.Column<int>(type: "int", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    tp_preenchimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endereco", x => x.id_endereco);
                    table.ForeignKey(
                        name: "FK_endereco_regiaotrabalho_id_regiao_trabalho",
                        column: x => x.id_regiao_trabalho,
                        principalTable: "regiaotrabalho",
                        principalColumn: "id_regiao_trabalho",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_endereco_setortrabalho_id_setor_trabalho",
                        column: x => x.id_setor_trabalho,
                        principalTable: "setortrabalho",
                        principalColumn: "id_setor_trabalho",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_endereco_tipoendereco_id_tipo_endereco",
                        column: x => x.id_tipo_endereco,
                        principalTable: "tipoendereco",
                        principalColumn: "id_tipo_endereco",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "areaarmazenagem",
                columns: table => new
                {
                    idarea_armazenagem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_tipo_area = table.Column<int>(type: "int", nullable: false),
                    id_endereco = table.Column<int>(type: "int", nullable: false),
                    agrupador_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nr_posicao_x = table.Column<int>(type: "int", nullable: false),
                    nr_posicao_y = table.Column<int>(type: "int", nullable: false),
                    nr_lado = table.Column<int>(type: "int", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    cd_identificacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_agrupador_reservado = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areaarmazenagem", x => x.idarea_armazenagem);
                    table.ForeignKey(
                        name: "FK_areaarmazenagem_agrupadorativo_agrupador_id",
                        column: x => x.agrupador_id,
                        principalTable: "agrupadorativo",
                        principalColumn: "id_agrupador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_areaarmazenagem_agrupadorativo_id_agrupador_reservado",
                        column: x => x.id_agrupador_reservado,
                        principalTable: "agrupadorativo",
                        principalColumn: "id_agrupador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_areaarmazenagem_endereco_id_endereco",
                        column: x => x.id_endereco,
                        principalTable: "endereco",
                        principalColumn: "id_endereco",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_areaarmazenagem_tipoarea_id_tipo_area",
                        column: x => x.id_tipo_area,
                        principalTable: "tipoarea",
                        principalColumn: "id_tipo_area",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pallet",
                columns: table => new
                {
                    id_pallet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_area_armazenagem = table.Column<int>(type: "int", nullable: false),
                    agrupador_ativo_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    qt_utilizacao = table.Column<int>(type: "int", nullable: false),
                    dt_ultima_movimentacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cd_identificacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pallet", x => x.id_pallet);
                    table.ForeignKey(
                        name: "FK_Pallet_agrupadorativo_agrupador_ativo_id",
                        column: x => x.agrupador_ativo_id,
                        principalTable: "agrupadorativo",
                        principalColumn: "id_agrupador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pallet_areaarmazenagem_id_area_armazenagem",
                        column: x => x.id_area_armazenagem,
                        principalTable: "areaarmazenagem",
                        principalColumn: "idarea_armazenagem",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_areaarmazenagem_agrupador_id",
                table: "areaarmazenagem",
                column: "agrupador_id");

            migrationBuilder.CreateIndex(
                name: "IX_areaarmazenagem_id_agrupador_reservado",
                table: "areaarmazenagem",
                column: "id_agrupador_reservado");

            migrationBuilder.CreateIndex(
                name: "IX_areaarmazenagem_id_endereco",
                table: "areaarmazenagem",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_areaarmazenagem_id_tipo_area",
                table: "areaarmazenagem",
                column: "id_tipo_area");

            migrationBuilder.CreateIndex(
                name: "IX_caixa_agrupador_id",
                table: "caixa",
                column: "agrupador_id");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_id_regiao_trabalho",
                table: "endereco",
                column: "id_regiao_trabalho");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_id_setor_trabalho",
                table: "endereco",
                column: "id_setor_trabalho");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_id_tipo_endereco",
                table: "endereco",
                column: "id_tipo_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Pallet_agrupador_ativo_id",
                table: "Pallet",
                column: "agrupador_ativo_id");

            migrationBuilder.CreateIndex(
                name: "IX_Pallet_id_area_armazenagem",
                table: "Pallet",
                column: "id_area_armazenagem");

            migrationBuilder.CreateIndex(
                name: "IX_regiaotrabalho_id_deposito",
                table: "regiaotrabalho",
                column: "id_deposito");

            migrationBuilder.CreateIndex(
                name: "IX_setortrabalho_id_deposito",
                table: "setortrabalho",
                column: "id_deposito");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "caixa");

            migrationBuilder.DropTable(
                name: "equipamento");

            migrationBuilder.DropTable(
                name: "Pallet");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "programa");

            migrationBuilder.DropTable(
                name: "turno");

            migrationBuilder.DropTable(
                name: "areaarmazenagem");

            migrationBuilder.DropTable(
                name: "agrupadorativo");

            migrationBuilder.DropTable(
                name: "endereco");

            migrationBuilder.DropTable(
                name: "tipoarea");

            migrationBuilder.DropTable(
                name: "regiaotrabalho");

            migrationBuilder.DropTable(
                name: "setortrabalho");

            migrationBuilder.DropTable(
                name: "tipoendereco");

            migrationBuilder.DropTable(
                name: "deposito");
        }
    }
}
