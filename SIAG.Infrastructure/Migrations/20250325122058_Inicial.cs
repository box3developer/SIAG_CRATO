using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIAG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agrupadorativo",
                columns: table => new
                {
                    id_agrupador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tp_agrupamento = table.Column<int>(type: "int", nullable: true),
                    codigo1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codigo2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    codigo3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cd_sequencia = table.Column<int>(type: "int", nullable: true),
                    dt_agrupador = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_areaarmazenagem = table.Column<int>(type: "int", nullable: true),
                    fg_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agrupadorativo", x => x.id_agrupador);
                });

            migrationBuilder.CreateTable(
                name: "atividade",
                columns: table => new
                {
                    id_atividade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_atividade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_equipamentomodelo = table.Column<int>(type: "int", nullable: false),
                    id_setortrabalho = table.Column<int>(type: "int", nullable: true),
                    fg_permite_rejeitar = table.Column<int>(type: "int", nullable: true),
                    id_atividadeanterior = table.Column<int>(type: "int", nullable: true),
                    fg_tipoatribuicaoautomatica = table.Column<int>(type: "int", nullable: true),
                    id_atividaderotinavalidacao = table.Column<int>(type: "int", nullable: true),
                    fg_evitaconflitoendereco = table.Column<int>(type: "int", nullable: true),
                    duracao = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividade", x => x.id_atividade);
                });

            migrationBuilder.CreateTable(
                name: "atividadeprioridade",
                columns: table => new
                {
                    id_atividadeprioridade = table.Column<int>(type: "int", nullable: false),
                    fg_tipo = table.Column<int>(type: "int", nullable: false),
                    nm_procedure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qt_pontuacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividadeprioridade", x => x.id_atividadeprioridade);
                });

            migrationBuilder.CreateTable(
                name: "atividaderejeicao",
                columns: table => new
                {
                    id_atividaderejeicao = table.Column<int>(type: "int", nullable: false),
                    nm_atividade_rejeicao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_email_alerta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividaderejeicao", x => x.id_atividaderejeicao);
                });

            migrationBuilder.CreateTable(
                name: "atividaderotina",
                columns: table => new
                {
                    id_atividaderotina = table.Column<int>(type: "int", nullable: false),
                    nm_atividaderotina = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_procedure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fg_tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividaderotina", x => x.id_atividaderotina);
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
                name: "desempenhoonline",
                columns: table => new
                {
                    id_desempenhoonline = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_operador = table.Column<int>(type: "int", nullable: false),
                    id_equipamentomodelo = table.Column<int>(type: "int", nullable: false),
                    id_setortrabalho = table.Column<int>(type: "int", nullable: false),
                    nr_tempologado = table.Column<int>(type: "int", nullable: false),
                    nr_tempoprevisto = table.Column<int>(type: "int", nullable: false),
                    nr_temporealizado = table.Column<int>(type: "int", nullable: false),
                    qt_previstaget = table.Column<int>(name: "qt_prevista {get", type: "int", nullable: false),
                    qt_realizadaget = table.Column<int>(name: "qt_realizada {get", type: "int", nullable: false),
                    dt_registro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desempenhoonline", x => x.id_desempenhoonline);
                });

            migrationBuilder.CreateTable(
                name: "equipamentoenderecoprioridade",
                columns: table => new
                {
                    id_equipamentoenderecoprioridade = table.Column<long>(type: "bigint", nullable: false),
                    id_equipamento_endereco = table.Column<long>(type: "bigint", nullable: false),
                    prioridade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentoenderecoprioridade", x => x.id_equipamentoenderecoprioridade);
                });

            migrationBuilder.CreateTable(
                name: "equipamentomodelo",
                columns: table => new
                {
                    id_equipamentomodelo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentomodelo", x => x.id_equipamentomodelo);
                });

            migrationBuilder.CreateTable(
                name: "log",
                columns: table => new
                {
                    id_requisicao = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    nm_identificador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_caixa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    metodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_operador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "logcaracol",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_requisicao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_identificador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_caixa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    mensagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    metodo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_operador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logcaracol", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "niveisagrupadores",
                columns: table => new
                {
                    id_niveisagrupadores = table.Column<long>(type: "bigint", nullable: false),
                    inicio = table.Column<long>(type: "bigint", nullable: false),
                    nivel = table.Column<int>(type: "int", nullable: false),
                    termino = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_niveisagrupadores", x => x.id_niveisagrupadores);
                });

            migrationBuilder.CreateTable(
                name: "operador",
                columns: table => new
                {
                    id_operador = table.Column<long>(type: "bigint", nullable: false),
                    nm_operador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nr_localidade = table.Column<int>(type: "int", nullable: true),
                    dt_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fg_funcao = table.Column<int>(type: "int", nullable: true),
                    id_responsavel = table.Column<long>(type: "bigint", nullable: true),
                    nm_login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_nfcoperador = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operador", x => x.id_operador);
                });

            migrationBuilder.CreateTable(
                name: "ordem",
                columns: table => new
                {
                    id_transportadora = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_veiculo = table.Column<int>(type: "int", nullable: false),
                    id_motorista = table.Column<int>(type: "int", nullable: false),
                    id_endereco = table.Column<int>(type: "int", nullable: false),
                    qt_cubagem = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dt_geracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_previsao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_entrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_fim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_saida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fg_controleendereco = table.Column<int>(type: "int", nullable: false),
                    fg_controlesms = table.Column<int>(type: "int", nullable: false),
                    id_motoristamanobrista = table.Column<int>(type: "int", nullable: false),
                    cd_centrocusto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nr_custovalor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    id_solicitante = table.Column<int>(type: "int", nullable: false),
                    id_motivo = table.Column<int>(type: "int", nullable: false),
                    priorizar = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordem", x => x.id_transportadora);
                });

            migrationBuilder.CreateTable(
                name: "ordemcarga",
                columns: table => new
                {
                    id_ordemcarga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ordem = table.Column<int>(type: "int", nullable: false),
                    id_pedido = table.Column<int>(type: "int", nullable: false),
                    id_programa = table.Column<int>(type: "int", nullable: false),
                    id_pallet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordemcarga", x => x.id_ordemcarga);
                });

            migrationBuilder.CreateTable(
                name: "ordemsequencia",
                columns: table => new
                {
                    id_ordemsequencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_ordem = table.Column<int>(type: "int", nullable: false),
                    id_transportadoratipocarga = table.Column<int>(type: "int", nullable: false),
                    nr_sequencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordemsequencia", x => x.id_ordemsequencia);
                });

            migrationBuilder.CreateTable(
                name: "parametro",
                columns: table => new
                {
                    id_parametro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fg_ativo = table.Column<int>(type: "int", nullable: true),
                    fg_tipoparametro = table.Column<int>(type: "int", nullable: true),
                    fg_visivel = table.Column<bool>(type: "bit", nullable: true),
                    nm_parametro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_unidademedida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_valor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametro", x => x.id_parametro);
                });

            migrationBuilder.CreateTable(
                name: "parametromensagemcaracol",
                columns: table => new
                {
                    id_parametromensagemcaracol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parametromensagemcaracol", x => x.id_parametromensagemcaracol);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    id_pedido = table.Column<int>(type: "int", nullable: false),
                    id_transportadora = table.Column<int>(type: "int", nullable: true),
                    cd_pedido = table.Column<int>(type: "int", nullable: true),
                    cd_lote = table.Column<int>(type: "int", nullable: true),
                    cd_box = table.Column<int>(type: "int", nullable: true),
                    cd_cliente = table.Column<int>(type: "int", nullable: true),
                    cd_estabelecimento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_notafiscal = table.Column<int>(type: "int", nullable: true),
                    cd_canal = table.Column<int>(type: "int", nullable: true),
                    cd_ordemexportacao = table.Column<int>(type: "int", nullable: true),
                    cd_veiculoexportacao = table.Column<int>(type: "int", nullable: true),
                    tp_carga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tp_cargaaglutinado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cd_rota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qt_caixa = table.Column<int>(type: "int", nullable: true),
                    qt_cubagemcaixa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    qt_acessorio = table.Column<int>(type: "int", nullable: true),
                    qt_cubagemacessorio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    qt_display = table.Column<int>(type: "int", nullable: true),
                    qt_cubagemdisplay = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    qt_expositores = table.Column<int>(type: "int", nullable: true),
                    qt_cubagemexpositores = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    fg_status = table.Column<int>(type: "int", nullable: true),
                    dt_importacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cd_ordemexportacaodefinitiva = table.Column<long>(type: "bigint", nullable: true),
                    cd_veiculoexportacaodefinitiva = table.Column<int>(type: "int", nullable: true),
                    dt_previsaoexportacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_implantacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_predata = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fg_sku = table.Column<int>(type: "int", nullable: true),
                    cd_sequenciaexpedicao = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.id_pedido);
                });

            migrationBuilder.CreateTable(
                name: "posicaocaracolrefugo",
                columns: table => new
                {
                    id_posicaocaracolrefugo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fabrica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    posicao = table.Column<int>(type: "int", nullable: true),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posicaocaracolrefugo", x => x.id_posicaocaracolrefugo);
                });

            migrationBuilder.CreateTable(
                name: "programa",
                columns: table => new
                {
                    id_programa = table.Column<int>(type: "int", nullable: false),
                    cd_programa = table.Column<int>(type: "int", nullable: true),
                    cd_documento = table.Column<int>(type: "int", nullable: true),
                    cd_fabrica = table.Column<int>(type: "int", nullable: false),
                    cd_estabelecimento = table.Column<int>(type: "int", nullable: false),
                    cd_equipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dt_liberacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fg_tipo = table.Column<int>(type: "int", nullable: false),
                    cd_deposito = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qt_alturacaixa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    qt_larguracaixa = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    qt_comprimentocaixa = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_programa", x => x.id_programa);
                });

            migrationBuilder.CreateTable(
                name: "status_leitor",
                columns: table => new
                {
                    id_status_leitor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    conectado = table.Column<int>(type: "int", nullable: true),
                    configurado = table.Column<int>(type: "int", nullable: true),
                    dt_status = table.Column<DateTime>(type: "datetime2", nullable: true),
                    equipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    executando = table.Column<int>(type: "int", nullable: true),
                    leitor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status_leitor", x => x.id_status_leitor);
                });

            migrationBuilder.CreateTable(
                name: "statusluzverde",
                columns: table => new
                {
                    caracol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    luz_verde = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "statusluzvermelha",
                columns: table => new
                {
                    caracol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    luz_vermelha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tempoatividade",
                columns: table => new
                {
                    id_tempoatividade = table.Column<int>(type: "int", nullable: false),
                    id_equipamentomodelo = table.Column<int>(type: "int", nullable: true),
                    id_setortrabalho = table.Column<int>(type: "int", nullable: true),
                    nr_tempooperacao = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_tempodeslocamento = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_tempocoluna = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_tempocorredor = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_tempoaltura = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao4 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao5 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao6 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao7 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao8 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao9 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao10 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao11 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao12 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao13 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao14 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao15 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao16 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao17 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao18 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao19 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao20 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao21 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_posicao22 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    nr_percentualineficiencia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    id_atividade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tempoatividade", x => x.id_tempoatividade);
                });

            migrationBuilder.CreateTable(
                name: "tipoarea",
                columns: table => new
                {
                    id_tipoarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_tipoarea = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoarea", x => x.id_tipoarea);
                });

            migrationBuilder.CreateTable(
                name: "tipoendereco",
                columns: table => new
                {
                    id_tipoendereco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nm_tipoendereco = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoendereco", x => x.id_tipoendereco);
                });

            migrationBuilder.CreateTable(
                name: "transportadoratipocarga",
                columns: table => new
                {
                    id_transportadoratipocarga = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_transportadora = table.Column<int>(type: "int", nullable: false),
                    tp_carga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_carga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_ordem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transportadoratipocarga", x => x.id_transportadoratipocarga);
                });

            migrationBuilder.CreateTable(
                name: "turno",
                columns: table => new
                {
                    id_turno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cd_turno = table.Column<int>(type: "int", nullable: false),
                    dt_inicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_fim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    diaanterior = table.Column<bool>(type: "bit", nullable: true),
                    diasucessor = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turno", x => x.id_turno);
                });

            migrationBuilder.CreateTable(
                name: "uf",
                columns: table => new
                {
                    nm_uf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_nomeuf = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "atividadetarefa",
                columns: table => new
                {
                    id_tarefa = table.Column<int>(type: "int", nullable: false),
                    nm_tarefa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nm_mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    id_atividade = table.Column<int>(type: "int", nullable: false),
                    cd_sequencia = table.Column<int>(type: "int", nullable: false),
                    fg_recurso = table.Column<int>(type: "int", nullable: true),
                    id_atividaderotina = table.Column<int>(type: "int", nullable: false),
                    qt_potencianormal = table.Column<int>(type: "int", nullable: false),
                    qt_potenciaaumentada = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "regiaotrabalho",
                columns: table => new
                {
                    id_regiaotrabalho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_deposito = table.Column<int>(type: "int", nullable: false),
                    nm_regiaotrabalho = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_regiaotrabalho", x => x.id_regiaotrabalho);
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
                    id_setortrabalho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_deposito = table.Column<int>(type: "int", nullable: false),
                    nm_setortrabalho = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setortrabalho", x => x.id_setortrabalho);
                    table.ForeignKey(
                        name: "FK_setortrabalho_deposito_id_deposito",
                        column: x => x.id_deposito,
                        principalTable: "deposito",
                        principalColumn: "id_deposito",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "equipamentochecklist",
                columns: table => new
                {
                    id_equipamentochecklist = table.Column<int>(type: "int", nullable: false),
                    id_equipamento = table.Column<int>(type: "int", nullable: false),
                    nm_descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fg_critico = table.Column<bool>(type: "bit", nullable: true),
                    fg_status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentochecklist", x => x.id_equipamentochecklist);
                    table.ForeignKey(
                        name: "FK_equipamentochecklist_equipamentomodelo_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamentomodelo",
                        principalColumn: "id_equipamentomodelo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    id_endereco = table.Column<int>(type: "int", nullable: false),
                    id_regiaotrabalho = table.Column<int>(type: "int", nullable: false),
                    id_setortrabalho = table.Column<int>(type: "int", nullable: false),
                    id_tipoendereco = table.Column<int>(type: "int", nullable: false),
                    nm_endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qt_estoqueminimo = table.Column<int>(type: "int", nullable: false),
                    qt_estoquemaximo = table.Column<int>(type: "int", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    tp_preenchimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endereco", x => x.id_endereco);
                    table.ForeignKey(
                        name: "FK_endereco_regiaotrabalho_id_regiaotrabalho",
                        column: x => x.id_regiaotrabalho,
                        principalTable: "regiaotrabalho",
                        principalColumn: "id_regiaotrabalho",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_endereco_setortrabalho_id_setortrabalho",
                        column: x => x.id_setortrabalho,
                        principalTable: "setortrabalho",
                        principalColumn: "id_setortrabalho",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_endereco_tipoendereco_id_tipoendereco",
                        column: x => x.id_tipoendereco,
                        principalTable: "tipoendereco",
                        principalColumn: "id_tipoendereco",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "areaarmazenagem",
                columns: table => new
                {
                    id_areaarmazenagem = table.Column<long>(type: "bigint", nullable: false),
                    id_tipoarea = table.Column<int>(type: "int", nullable: false),
                    id_endereco = table.Column<int>(type: "int", nullable: false),
                    id_agrupador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    nr_posicaox = table.Column<int>(type: "int", nullable: false),
                    nr_posicaoy = table.Column<int>(type: "int", nullable: false),
                    nr_lado = table.Column<int>(type: "int", nullable: true),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    cd_identificacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_agrupador_reservado = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_areaarmazenagem", x => x.id_areaarmazenagem);
                    table.ForeignKey(
                        name: "FK_areaarmazenagem_agrupadorativo_id_agrupador",
                        column: x => x.id_agrupador,
                        principalTable: "agrupadorativo",
                        principalColumn: "id_agrupador");
                    table.ForeignKey(
                        name: "FK_areaarmazenagem_agrupadorativo_id_agrupador_reservado",
                        column: x => x.id_agrupador_reservado,
                        principalTable: "agrupadorativo",
                        principalColumn: "id_agrupador");
                    table.ForeignKey(
                        name: "FK_areaarmazenagem_endereco_id_endereco",
                        column: x => x.id_endereco,
                        principalTable: "endereco",
                        principalColumn: "id_endereco",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_areaarmazenagem_tipoarea_id_tipoarea",
                        column: x => x.id_tipoarea,
                        principalTable: "tipoarea",
                        principalColumn: "id_tipoarea",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "equipamento",
                columns: table => new
                {
                    id_equipamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_equipamentomodelo = table.Column<int>(type: "int", nullable: true),
                    id_setortrabalho = table.Column<int>(type: "int", nullable: true),
                    id_operador = table.Column<long>(type: "bigint", nullable: true),
                    id_operador2 = table.Column<long>(type: "bigint", nullable: true),
                    id_endereco = table.Column<int>(type: "int", nullable: true),
                    nm_equipamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_identificador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fg_status = table.Column<int>(type: "int", nullable: true),
                    dt_inclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_manutencao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nm_observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cd_ultimaleitura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dt_ultimaleitura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    nm_ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fg_statustrocacaracol = table.Column<int>(type: "int", nullable: true),
                    nm_abreviado_equipamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cd_leitura_pendente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nm_usuario_liberacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamento", x => x.id_equipamento);
                    table.ForeignKey(
                        name: "FK_equipamento_endereco_id_endereco",
                        column: x => x.id_endereco,
                        principalTable: "endereco",
                        principalColumn: "id_endereco");
                    table.ForeignKey(
                        name: "FK_equipamento_equipamentomodelo_id_equipamentomodelo",
                        column: x => x.id_equipamentomodelo,
                        principalTable: "equipamentomodelo",
                        principalColumn: "id_equipamentomodelo");
                    table.ForeignKey(
                        name: "FK_equipamento_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador");
                    table.ForeignKey(
                        name: "FK_equipamento_operador_id_operador2",
                        column: x => x.id_operador2,
                        principalTable: "operador",
                        principalColumn: "id_operador");
                    table.ForeignKey(
                        name: "FK_equipamento_setortrabalho_id_setortrabalho",
                        column: x => x.id_setortrabalho,
                        principalTable: "setortrabalho",
                        principalColumn: "id_setortrabalho");
                });

            migrationBuilder.CreateTable(
                name: "pallet",
                columns: table => new
                {
                    id_pallet = table.Column<int>(type: "int", nullable: false),
                    id_areaarmazenagem = table.Column<long>(type: "bigint", nullable: true),
                    id_agrupador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fg_status = table.Column<int>(type: "int", nullable: true),
                    qt_utilizacao = table.Column<int>(type: "int", nullable: true),
                    dt_ultimamovimentacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cd_identificacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pallet", x => x.id_pallet);
                    table.ForeignKey(
                        name: "FK_pallet_agrupadorativo_id_agrupador",
                        column: x => x.id_agrupador,
                        principalTable: "agrupadorativo",
                        principalColumn: "id_agrupador");
                    table.ForeignKey(
                        name: "FK_pallet_areaarmazenagem_id_areaarmazenagem",
                        column: x => x.id_areaarmazenagem,
                        principalTable: "areaarmazenagem",
                        principalColumn: "id_areaarmazenagem");
                });

            migrationBuilder.CreateTable(
                name: "desempenho",
                columns: table => new
                {
                    id_desempenho = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dt_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fg_erroclassificacao = table.Column<int>(type: "int", nullable: true),
                    fg_humoreficiencia = table.Column<int>(type: "int", nullable: true),
                    id_areaarmazenagem = table.Column<long>(type: "bigint", nullable: true),
                    id_equipamento = table.Column<int>(type: "int", nullable: true),
                    id_equipamentomodelo = table.Column<int>(type: "int", nullable: true),
                    id_operador = table.Column<long>(type: "bigint", nullable: true),
                    id_referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_setortrabalho = table.Column<int>(type: "int", nullable: true),
                    nr_tempoestimado = table.Column<int>(type: "int", nullable: true),
                    nr_temporealizado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desempenho", x => x.id_desempenho);
                    table.ForeignKey(
                        name: "FK_desempenho_areaarmazenagem_id_areaarmazenagem",
                        column: x => x.id_areaarmazenagem,
                        principalTable: "areaarmazenagem",
                        principalColumn: "id_areaarmazenagem");
                    table.ForeignKey(
                        name: "FK_desempenho_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento");
                    table.ForeignKey(
                        name: "FK_desempenho_equipamentomodelo_id_equipamentomodelo",
                        column: x => x.id_equipamentomodelo,
                        principalTable: "equipamentomodelo",
                        principalColumn: "id_equipamentomodelo");
                    table.ForeignKey(
                        name: "FK_desempenho_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador");
                    table.ForeignKey(
                        name: "FK_desempenho_setortrabalho_id_setortrabalho",
                        column: x => x.id_setortrabalho,
                        principalTable: "setortrabalho",
                        principalColumn: "id_setortrabalho");
                });

            migrationBuilder.CreateTable(
                name: "desempenhocaixa",
                columns: table => new
                {
                    id_caixa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dt_leituracaixa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fg_erroclassificacao = table.Column<int>(type: "int", nullable: true),
                    id_areaarmazenagem = table.Column<long>(type: "bigint", nullable: true),
                    id_equipamento = table.Column<int>(type: "int", nullable: true),
                    id_operador = table.Column<long>(type: "bigint", nullable: true),
                    nr_tempoestimado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_desempenhocaixa_areaarmazenagem_id_areaarmazenagem",
                        column: x => x.id_areaarmazenagem,
                        principalTable: "areaarmazenagem",
                        principalColumn: "id_areaarmazenagem");
                    table.ForeignKey(
                        name: "FK_desempenhocaixa_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento");
                    table.ForeignKey(
                        name: "FK_desempenhocaixa_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador");
                });

            migrationBuilder.CreateTable(
                name: "equipamentochecklistoperador",
                columns: table => new
                {
                    id_equipamentochecklistoperador = table.Column<int>(type: "int", nullable: false),
                    id_equipamento = table.Column<int>(type: "int", nullable: false),
                    id_operador = table.Column<long>(type: "bigint", nullable: false),
                    id_equipamentochecklist = table.Column<int>(type: "int", nullable: false),
                    fg_resposta = table.Column<bool>(type: "bit", nullable: false),
                    dt_checklist = table.Column<DateTime>(type: "datetime2", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "equipamentoendereco",
                columns: table => new
                {
                    id_equipamentoendereco = table.Column<long>(type: "bigint", nullable: false),
                    id_equipamento = table.Column<int>(type: "int", nullable: false),
                    id_endereco = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentoendereco", x => x.id_equipamentoendereco);
                    table.ForeignKey(
                        name: "FK_equipamentoendereco_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "equipamentomanutencao",
                columns: table => new
                {
                    id_equipamentomanutencao = table.Column<int>(type: "int", nullable: false),
                    id_equipamento = table.Column<int>(type: "int", nullable: false),
                    fg_tipo_manutencao = table.Column<int>(type: "int", nullable: false),
                    dt_inicio = table.Column<int>(type: "int", nullable: false),
                    dt_fim = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipamentomanutencao", x => x.id_equipamentomanutencao);
                    table.ForeignKey(
                        name: "FK_equipamentomanutencao_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "lidervirtual",
                columns: table => new
                {
                    id_lidervirtual = table.Column<long>(type: "bigint", nullable: false),
                    dt_login = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_loginlimite = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_logoff = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_equipamentodestino = table.Column<int>(type: "int", nullable: true),
                    id_equipamentoorigem = table.Column<int>(type: "int", nullable: true),
                    id_operador = table.Column<long>(type: "bigint", nullable: true),
                    id_operadorlogin = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lidervirtual", x => x.id_lidervirtual);
                    table.ForeignKey(
                        name: "FK_lidervirtual_equipamento_id_equipamentodestino",
                        column: x => x.id_equipamentodestino,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento");
                    table.ForeignKey(
                        name: "FK_lidervirtual_equipamento_id_equipamentoorigem",
                        column: x => x.id_equipamentoorigem,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento");
                    table.ForeignKey(
                        name: "FK_lidervirtual_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador");
                    table.ForeignKey(
                        name: "FK_lidervirtual_operador_id_operadorlogin",
                        column: x => x.id_operadorlogin,
                        principalTable: "operador",
                        principalColumn: "id_operador");
                });

            migrationBuilder.CreateTable(
                name: "operadorhistorico",
                columns: table => new
                {
                    id_operador = table.Column<long>(type: "bigint", nullable: true),
                    cd_evento = table.Column<int>(type: "int", nullable: true),
                    dt_evento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    id_endereco = table.Column<int>(type: "int", nullable: true),
                    id_equipamento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_operadorhistorico_endereco_id_endereco",
                        column: x => x.id_endereco,
                        principalTable: "endereco",
                        principalColumn: "id_endereco");
                    table.ForeignKey(
                        name: "FK_operadorhistorico_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento");
                });

            migrationBuilder.CreateTable(
                name: "caixa",
                columns: table => new
                {
                    id_caixa = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    id_agrupador = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_pallet = table.Column<int>(type: "int", nullable: true),
                    id_programa = table.Column<int>(type: "int", nullable: true),
                    id_pedido = table.Column<int>(type: "int", nullable: true),
                    cd_produto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cd_cor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cd_gradetamanho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nr_caixa = table.Column<int>(type: "int", nullable: true),
                    nr_pares = table.Column<int>(type: "int", nullable: true),
                    fg_rfid = table.Column<bool>(type: "bit", nullable: true),
                    fg_status = table.Column<int>(type: "int", nullable: true),
                    dt_embalagem = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_sorter = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_estufamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dt_expedicao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    qt_peso = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caixa", x => x.id_caixa);
                    table.ForeignKey(
                        name: "FK_caixa_agrupadorativo_id_agrupador",
                        column: x => x.id_agrupador,
                        principalTable: "agrupadorativo",
                        principalColumn: "id_agrupador");
                    table.ForeignKey(
                        name: "FK_caixa_pallet_id_pallet",
                        column: x => x.id_pallet,
                        principalTable: "pallet",
                        principalColumn: "id_pallet");
                    table.ForeignKey(
                        name: "FK_caixa_pedido_id_pedido",
                        column: x => x.id_pedido,
                        principalTable: "pedido",
                        principalColumn: "id_pedido");
                    table.ForeignKey(
                        name: "FK_caixa_programa_id_programa",
                        column: x => x.id_programa,
                        principalTable: "programa",
                        principalColumn: "id_programa");
                });

            migrationBuilder.CreateTable(
                name: "chamada",
                columns: table => new
                {
                    id_chamada = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_palletorigem = table.Column<int>(type: "int", nullable: false),
                    id_areaarmazenagemorigem = table.Column<long>(type: "bigint", nullable: false),
                    id_palletdestino = table.Column<int>(type: "int", nullable: false),
                    id_areaarmazenagemdestino = table.Column<long>(type: "bigint", nullable: false),
                    id_palletleitura = table.Column<int>(type: "int", nullable: false),
                    id_areaarmazenagemleitura = table.Column<long>(type: "bigint", nullable: false),
                    id_operador = table.Column<long>(type: "bigint", nullable: false),
                    id_equipamento = table.Column<int>(type: "int", nullable: false),
                    id_atividaderejeicao = table.Column<int>(type: "int", nullable: false),
                    id_atividade = table.Column<int>(type: "int", nullable: false),
                    fg_status = table.Column<int>(type: "int", nullable: false),
                    dt_chamada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_atendida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_finalizada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_recebida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_rejeitada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_chamadaorigem = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_chamadasuspensa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    priorizar = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chamada", x => x.id_chamada);
                    table.ForeignKey(
                        name: "FK_chamada_areaarmazenagem_id_areaarmazenagemdestino",
                        column: x => x.id_areaarmazenagemdestino,
                        principalTable: "areaarmazenagem",
                        principalColumn: "id_areaarmazenagem",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_areaarmazenagem_id_areaarmazenagemleitura",
                        column: x => x.id_areaarmazenagemleitura,
                        principalTable: "areaarmazenagem",
                        principalColumn: "id_areaarmazenagem",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_areaarmazenagem_id_areaarmazenagemorigem",
                        column: x => x.id_areaarmazenagemorigem,
                        principalTable: "areaarmazenagem",
                        principalColumn: "id_areaarmazenagem",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_atividade_id_atividade",
                        column: x => x.id_atividade,
                        principalTable: "atividade",
                        principalColumn: "id_atividade",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_atividade_id_atividaderejeicao",
                        column: x => x.id_atividaderejeicao,
                        principalTable: "atividade",
                        principalColumn: "id_atividade",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_chamada_id_chamadaorigem",
                        column: x => x.id_chamadaorigem,
                        principalTable: "chamada",
                        principalColumn: "id_chamada",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_chamada_id_chamadasuspensa",
                        column: x => x.id_chamadasuspensa,
                        principalTable: "chamada",
                        principalColumn: "id_chamada",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_pallet_id_palletdestino",
                        column: x => x.id_palletdestino,
                        principalTable: "pallet",
                        principalColumn: "id_pallet",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_pallet_id_palletleitura",
                        column: x => x.id_palletleitura,
                        principalTable: "pallet",
                        principalColumn: "id_pallet",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_chamada_pallet_id_palletorigem",
                        column: x => x.id_palletorigem,
                        principalTable: "pallet",
                        principalColumn: "id_pallet",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "caixaleitura",
                columns: table => new
                {
                    id_caixaleitura = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dt_leitura = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fg_cancelado = table.Column<bool>(type: "bit", nullable: true),
                    fg_status = table.Column<int>(type: "int", nullable: true),
                    fg_tipo = table.Column<int>(type: "int", nullable: true),
                    id_areaarmazenagem = table.Column<long>(type: "bigint", nullable: true),
                    id_caixa = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    id_endereco = table.Column<int>(type: "int", nullable: true),
                    id_equipamento = table.Column<int>(type: "int", nullable: true),
                    id_operador = table.Column<long>(type: "bigint", nullable: true),
                    id_ordem = table.Column<long>(type: "bigint", nullable: true),
                    id_pallet = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caixaleitura", x => x.id_caixaleitura);
                    table.ForeignKey(
                        name: "FK_caixaleitura_areaarmazenagem_id_areaarmazenagem",
                        column: x => x.id_areaarmazenagem,
                        principalTable: "areaarmazenagem",
                        principalColumn: "id_areaarmazenagem");
                    table.ForeignKey(
                        name: "FK_caixaleitura_caixa_id_caixa",
                        column: x => x.id_caixa,
                        principalTable: "caixa",
                        principalColumn: "id_caixa");
                    table.ForeignKey(
                        name: "FK_caixaleitura_endereco_id_endereco",
                        column: x => x.id_endereco,
                        principalTable: "endereco",
                        principalColumn: "id_endereco");
                    table.ForeignKey(
                        name: "FK_caixaleitura_equipamento_id_equipamento",
                        column: x => x.id_equipamento,
                        principalTable: "equipamento",
                        principalColumn: "id_equipamento");
                    table.ForeignKey(
                        name: "FK_caixaleitura_operador_id_operador",
                        column: x => x.id_operador,
                        principalTable: "operador",
                        principalColumn: "id_operador");
                    table.ForeignKey(
                        name: "FK_caixaleitura_pallet_id_pallet",
                        column: x => x.id_pallet,
                        principalTable: "pallet",
                        principalColumn: "id_pallet");
                });

            migrationBuilder.CreateTable(
                name: "chamadatarefa",
                columns: table => new
                {
                    id_chamada = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_tarefa = table.Column<int>(type: "int", nullable: false),
                    dt_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_fim = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "IX_areaarmazenagem_id_agrupador",
                table: "areaarmazenagem",
                column: "id_agrupador");

            migrationBuilder.CreateIndex(
                name: "IX_areaarmazenagem_id_agrupador_reservado",
                table: "areaarmazenagem",
                column: "id_agrupador_reservado");

            migrationBuilder.CreateIndex(
                name: "IX_areaarmazenagem_id_endereco",
                table: "areaarmazenagem",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_areaarmazenagem_id_tipoarea",
                table: "areaarmazenagem",
                column: "id_tipoarea");

            migrationBuilder.CreateIndex(
                name: "IX_atividadetarefa_id_atividaderotina",
                table: "atividadetarefa",
                column: "id_atividaderotina");

            migrationBuilder.CreateIndex(
                name: "IX_caixa_id_agrupador",
                table: "caixa",
                column: "id_agrupador");

            migrationBuilder.CreateIndex(
                name: "IX_caixa_id_pallet",
                table: "caixa",
                column: "id_pallet");

            migrationBuilder.CreateIndex(
                name: "IX_caixa_id_pedido",
                table: "caixa",
                column: "id_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_caixa_id_programa",
                table: "caixa",
                column: "id_programa");

            migrationBuilder.CreateIndex(
                name: "IX_caixaleitura_id_areaarmazenagem",
                table: "caixaleitura",
                column: "id_areaarmazenagem");

            migrationBuilder.CreateIndex(
                name: "IX_caixaleitura_id_caixa",
                table: "caixaleitura",
                column: "id_caixa");

            migrationBuilder.CreateIndex(
                name: "IX_caixaleitura_id_endereco",
                table: "caixaleitura",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_caixaleitura_id_equipamento",
                table: "caixaleitura",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_caixaleitura_id_operador",
                table: "caixaleitura",
                column: "id_operador");

            migrationBuilder.CreateIndex(
                name: "IX_caixaleitura_id_pallet",
                table: "caixaleitura",
                column: "id_pallet");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_areaarmazenagemdestino",
                table: "chamada",
                column: "id_areaarmazenagemdestino");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_areaarmazenagemleitura",
                table: "chamada",
                column: "id_areaarmazenagemleitura");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_areaarmazenagemorigem",
                table: "chamada",
                column: "id_areaarmazenagemorigem");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_atividade",
                table: "chamada",
                column: "id_atividade");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_atividaderejeicao",
                table: "chamada",
                column: "id_atividaderejeicao");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_chamadaorigem",
                table: "chamada",
                column: "id_chamadaorigem");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_chamadasuspensa",
                table: "chamada",
                column: "id_chamadasuspensa");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_equipamento",
                table: "chamada",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_operador",
                table: "chamada",
                column: "id_operador");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_palletdestino",
                table: "chamada",
                column: "id_palletdestino");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_palletleitura",
                table: "chamada",
                column: "id_palletleitura");

            migrationBuilder.CreateIndex(
                name: "IX_chamada_id_palletorigem",
                table: "chamada",
                column: "id_palletorigem");

            migrationBuilder.CreateIndex(
                name: "IX_chamadatarefa_id_chamada",
                table: "chamadatarefa",
                column: "id_chamada");

            migrationBuilder.CreateIndex(
                name: "IX_chamadatarefa_id_tarefa",
                table: "chamadatarefa",
                column: "id_tarefa");

            migrationBuilder.CreateIndex(
                name: "IX_desempenho_id_areaarmazenagem",
                table: "desempenho",
                column: "id_areaarmazenagem");

            migrationBuilder.CreateIndex(
                name: "IX_desempenho_id_equipamento",
                table: "desempenho",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_desempenho_id_equipamentomodelo",
                table: "desempenho",
                column: "id_equipamentomodelo");

            migrationBuilder.CreateIndex(
                name: "IX_desempenho_id_operador",
                table: "desempenho",
                column: "id_operador");

            migrationBuilder.CreateIndex(
                name: "IX_desempenho_id_setortrabalho",
                table: "desempenho",
                column: "id_setortrabalho");

            migrationBuilder.CreateIndex(
                name: "IX_desempenhocaixa_id_areaarmazenagem",
                table: "desempenhocaixa",
                column: "id_areaarmazenagem");

            migrationBuilder.CreateIndex(
                name: "IX_desempenhocaixa_id_equipamento",
                table: "desempenhocaixa",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_desempenhocaixa_id_operador",
                table: "desempenhocaixa",
                column: "id_operador");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_id_regiaotrabalho",
                table: "endereco",
                column: "id_regiaotrabalho");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_id_setortrabalho",
                table: "endereco",
                column: "id_setortrabalho");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_id_tipoendereco",
                table: "endereco",
                column: "id_tipoendereco");

            migrationBuilder.CreateIndex(
                name: "IX_equipamento_id_endereco",
                table: "equipamento",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_equipamento_id_equipamentomodelo",
                table: "equipamento",
                column: "id_equipamentomodelo");

            migrationBuilder.CreateIndex(
                name: "IX_equipamento_id_operador",
                table: "equipamento",
                column: "id_operador");

            migrationBuilder.CreateIndex(
                name: "IX_equipamento_id_operador2",
                table: "equipamento",
                column: "id_operador2");

            migrationBuilder.CreateIndex(
                name: "IX_equipamento_id_setortrabalho",
                table: "equipamento",
                column: "id_setortrabalho");

            migrationBuilder.CreateIndex(
                name: "IX_equipamentochecklist_id_equipamento",
                table: "equipamentochecklist",
                column: "id_equipamento");

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

            migrationBuilder.CreateIndex(
                name: "IX_equipamentoendereco_id_equipamento",
                table: "equipamentoendereco",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_equipamentomanutencao_id_equipamento",
                table: "equipamentomanutencao",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_lidervirtual_id_equipamentodestino",
                table: "lidervirtual",
                column: "id_equipamentodestino");

            migrationBuilder.CreateIndex(
                name: "IX_lidervirtual_id_equipamentoorigem",
                table: "lidervirtual",
                column: "id_equipamentoorigem");

            migrationBuilder.CreateIndex(
                name: "IX_lidervirtual_id_operador",
                table: "lidervirtual",
                column: "id_operador");

            migrationBuilder.CreateIndex(
                name: "IX_lidervirtual_id_operadorlogin",
                table: "lidervirtual",
                column: "id_operadorlogin");

            migrationBuilder.CreateIndex(
                name: "IX_operadorhistorico_id_endereco",
                table: "operadorhistorico",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_operadorhistorico_id_equipamento",
                table: "operadorhistorico",
                column: "id_equipamento");

            migrationBuilder.CreateIndex(
                name: "IX_pallet_id_agrupador",
                table: "pallet",
                column: "id_agrupador");

            migrationBuilder.CreateIndex(
                name: "IX_pallet_id_areaarmazenagem",
                table: "pallet",
                column: "id_areaarmazenagem");

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
                name: "atividadeprioridade");

            migrationBuilder.DropTable(
                name: "atividaderejeicao");

            migrationBuilder.DropTable(
                name: "caixaleitura");

            migrationBuilder.DropTable(
                name: "chamadatarefa");

            migrationBuilder.DropTable(
                name: "desempenho");

            migrationBuilder.DropTable(
                name: "desempenhocaixa");

            migrationBuilder.DropTable(
                name: "desempenhoonline");

            migrationBuilder.DropTable(
                name: "equipamentochecklistoperador");

            migrationBuilder.DropTable(
                name: "equipamentoendereco");

            migrationBuilder.DropTable(
                name: "equipamentoenderecoprioridade");

            migrationBuilder.DropTable(
                name: "equipamentomanutencao");

            migrationBuilder.DropTable(
                name: "lidervirtual");

            migrationBuilder.DropTable(
                name: "log");

            migrationBuilder.DropTable(
                name: "logcaracol");

            migrationBuilder.DropTable(
                name: "niveisagrupadores");

            migrationBuilder.DropTable(
                name: "operadorhistorico");

            migrationBuilder.DropTable(
                name: "ordem");

            migrationBuilder.DropTable(
                name: "ordemcarga");

            migrationBuilder.DropTable(
                name: "ordemsequencia");

            migrationBuilder.DropTable(
                name: "parametro");

            migrationBuilder.DropTable(
                name: "parametromensagemcaracol");

            migrationBuilder.DropTable(
                name: "posicaocaracolrefugo");

            migrationBuilder.DropTable(
                name: "status_leitor");

            migrationBuilder.DropTable(
                name: "statusluzverde");

            migrationBuilder.DropTable(
                name: "statusluzvermelha");

            migrationBuilder.DropTable(
                name: "tempoatividade");

            migrationBuilder.DropTable(
                name: "transportadoratipocarga");

            migrationBuilder.DropTable(
                name: "turno");

            migrationBuilder.DropTable(
                name: "uf");

            migrationBuilder.DropTable(
                name: "caixa");

            migrationBuilder.DropTable(
                name: "atividadetarefa");

            migrationBuilder.DropTable(
                name: "chamada");

            migrationBuilder.DropTable(
                name: "equipamentochecklist");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "programa");

            migrationBuilder.DropTable(
                name: "atividaderotina");

            migrationBuilder.DropTable(
                name: "atividade");

            migrationBuilder.DropTable(
                name: "equipamento");

            migrationBuilder.DropTable(
                name: "pallet");

            migrationBuilder.DropTable(
                name: "equipamentomodelo");

            migrationBuilder.DropTable(
                name: "operador");

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
