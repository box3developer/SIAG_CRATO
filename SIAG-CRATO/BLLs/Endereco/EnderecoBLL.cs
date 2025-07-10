using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Endereco;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Endereco
{
    public class EnderecoBLL
    {
        public static async Task<List<EnderecoDTO>> GetListAsync()
        {
            using var conexao = new SqlConnection(Global.Conexao);
            var enderecos = await conexao.QueryAsync<EnderecoModel>(EnderecoQuery.SELECT);

            return enderecos.Select(ConvertToDTO).ToList();
        }

        public static async Task<EnderecoDTO?> GetByIdAsync(int id)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_endereco = @id";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryFirstOrDefaultAsync<EnderecoModel>(sql, new { id });

            if (endereco == null)
            {
                return null;
            }

            return ConvertToDTO(endereco);
        }

        public static async Task<List<EnderecoDTO>> GetBySetorStatus(int id_setortrabalho, int fg_status)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_setortrabalho = @id_setortrabalho and endereco.fg_status = @fg_status";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryAsync<EnderecoModel>(sql, new { id_setortrabalho, fg_status });

            return endereco.Select(ConvertToDTO).ToList();
        }

        public static async Task<List<EnderecoDTO>> GetBySetor(int id_setortrabalho)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_setortrabalho = @id_setortrabalho";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryAsync<EnderecoModel>(sql, new { id_setortrabalho });

            return endereco.Select(ConvertToDTO).ToList();
        }

        public async Task<IEnumerable<DestinoPalletDTO>> ObterDestinoPalletAsync(int idPallet)
        {
            const byte STATUS_CAIXA_ARMAZENADA = 4;
            const byte TIPO_ENDERECO_PORTAPALLET = 1;
            const byte STATUS_AREA_ATIVA = 2;
            const byte CANAL_MI = 1;
            const byte CANAL_ME = 2;

            using var conexao = new SqlConnection(Global.Conexao);
            await conexao.OpenAsync();

            // 1) Buscar pedido, programa e agrupador a partir da primeira caixa armazenada do pallet
            var caixaInfo = await conexao.QuerySingleOrDefaultAsync<CaixaInfoDto>(@"
                    SELECT TOP 1
                        dc.id_pedido           AS IdPedido,
                        dc.id_programa         AS IdPrograma,
                        dc.id_agrupador        AS IdAgrupador
                    FROM caixa dc WITH(NOLOCK)
                    WHERE dc.id_pallet = @IdPallet
                      AND dc.fg_status = @StatusCaixa;
                    ", new { IdPallet = idPallet, StatusCaixa = STATUS_CAIXA_ARMAZENADA });

            long? idPedido = caixaInfo?.IdPedido;
            long? idPrograma = caixaInfo?.IdPrograma;
            Guid? idAgrupador = caixaInfo?.IdAgrupador;

            // 2) Buscar dados do pedido
            PedidoInfoDto? pedidoInfo = null;
            if (idPedido.HasValue)
            {
                pedidoInfo = await conexao.QuerySingleOrDefaultAsync<PedidoInfoDto>(@"
                    SELECT
                        p.id_transportadora                          AS IdTransportadora,
                        p.tp_carga                                   AS TipoCargaPedido,
                        CASE
                          WHEN p.cd_ordemexportacao IS NOT NULL THEN @CanalME
                          WHEN p.cd_ordemexportacaodefinitiva IS NOT NULL THEN @CanalME
                          ELSE @CanalMI
                        END                                          AS CanalPedido,
                        p.cd_lote                                    AS CdLote,
                        ISNULL(p.cd_ordemexportacao, p.cd_ordemexportacaodefinitiva) AS CdOrdemExportacao,
                        ISNULL(p.cd_veiculoexportacao, p.cd_veiculoexportacaodefinitiva) AS CdVeiculoExportacao
                    FROM pedido p WITH(NOLOCK)
                    WHERE p.id_pedido = @IdPedido;
                    ", new
                {
                    IdPedido = idPedido,
                    CanalMI = CANAL_MI,
                    CanalME = CANAL_ME
                });
            }

            // 3) Se cdLote == 1 ou nulo, buscar do agrupador
            if (pedidoInfo != null
                && (!pedidoInfo.CdLote.HasValue || pedidoInfo.CdLote.Value == 1)
                && idAgrupador.HasValue)
            {
                pedidoInfo.CdLote = await conexao.QuerySingleOrDefaultAsync<long?>(@"
                    SELECT TOP 1 a.codigo3
                    FROM agrupadorativo a WITH(NOLOCK)
                    WHERE a.id_agrupador = @IdAgrupador
                      AND a.tp_agrupamento IN (1,4,5);
                    ", new { IdAgrupador = idAgrupador });
            }

            // 4) Buscar tipo de programa
            int tipoPrograma = 0;
            if (idPrograma.HasValue)
            {
                tipoPrograma = await conexao.QuerySingleOrDefaultAsync<int>(@"
                    SELECT pr.fg_tipo
                    FROM programa pr WITH(NOLOCK)
                    WHERE pr.id_programa = @IdPrograma;
                    ", new { IdPrograma = idPrograma.Value });
            }

            // 5) Montar lista inicial de endereços porta-pallet ativos
            var enderecosPP = (await conexao.QueryAsync<int>(@"
                    SELECT e.id_endereco
                    FROM endereco e WITH(NOLOCK)
                    WHERE e.id_tipoendereco = @TipoEnderecoPortaPallet
                      AND e.fg_status = @StatusAtiva;
                    ", new { TipoEnderecoPortaPallet = TIPO_ENDERECO_PORTAPALLET, StatusAtiva = STATUS_AREA_ATIVA }))
                .ToList();

            // 6) Filtrar por endereços atribuídos ao pedido, se houver pedido
            if (pedidoInfo != null)
            {
                var atribuídos = (await conexao.QueryAsync<int>(@"
                    SELECT ea.id_endereco
                    FROM enderecoarmazenagem ea WITH(NOLOCK)
                    WHERE (@CanalPedido = @CanalMI AND ea.cd_lote = @CdLote)
                       OR (@CanalPedido = @CanalME AND ea.cd_ordemexportacao = @CdOrdemExportacao
                                                 AND ea.cd_veiculoexportacao = @CdVeiculoExportacao);
                    ", new
                {
                    CanalPedido = pedidoInfo.CanalPedido,
                    CanalMI = CANAL_MI,
                    CanalME = CANAL_ME,
                    CdLote = pedidoInfo.CdLote,
                    CdOrdemExportacao = pedidoInfo.CdOrdemExportacao,
                    CdVeiculoExportacao = pedidoInfo.CdVeiculoExportacao
                })).ToHashSet();

                enderecosPP = enderecosPP
                    .Where(e => atribuídos.Contains(e))
                    .ToList();
            }

            // 7) Percorrer endereços e buscar preenchimentos
            var resultado = new List<DestinoPalletDTO>();
            foreach (var idEndereco in enderecosPP)
            {
                var preenchimentos = (await conexao.QueryAsync<PreenchimentoDto>(@"
                    SELECT
                        p.id_preenchimento          AS IdPreenchimento,
                        p.tp_carga                  AS TipoCargaPreenchimento,
                        p.id_transportadora         AS IdTransportadora,
                        p.cd_canal                  AS CanalPreenchimento,
                        p.fg_estoqueestrategico     AS EstoqueEstrategico,
                        p.fg_foradelinha            AS ForaDeLinha
                    FROM preenchimento p WITH(NOLOCK)
                    WHERE p.id_endereco = @IdEndereco
                    ORDER BY p.nr_sequencia;
                    ", new { IdEndereco = idEndereco })).ToList();

                if (preenchimentos.Count == 0)
                {
                    // sem qualquer preenchimento → pode alocar com preenchimento nulo
                    resultado.Add(new DestinoPalletDTO { IdEndereco = idEndereco, IdPreenchimento = null });
                }
                else
                {
                    // tenta achar primeiro preenchimento que satisfaça as regras
                    foreach (var p in preenchimentos)
                    {
                        if ((!string.IsNullOrEmpty(p.TipoCargaPreenchimento) && pedidoInfo?.TipoCargaPedido == p.TipoCargaPreenchimento)
                            || (p.IdTransportadora.HasValue && p.IdTransportadora == pedidoInfo?.IdTransportadora)
                            || (p.CanalPreenchimento != 0 && p.CanalPreenchimento == pedidoInfo?.CanalPedido)
                            || (p.EstoqueEstrategico && tipoPrograma == 2)
                            || (p.ForaDeLinha && tipoPrograma == 3))
                        {
                            resultado.Add(new DestinoPalletDTO
                            {
                                IdEndereco = idEndereco,
                                IdPreenchimento = p.IdPreenchimento
                            });
                            break;
                        }
                    }
                }
            }

            return resultado;
        }

        private static EnderecoDTO ConvertToDTO(EnderecoModel endereco)
        {
            return new()
            {
                IdEndereco = endereco.IdEndereco,
                IdRegiaoTrabalho = endereco.IdRegiaoTrabalho,
                IdSetorTrabalho = endereco.IdSetorTrabalho,
                IdTipoEndereco = endereco.IdTipoEndereco,
                NmEndereco = endereco.NmEndereco,
                QtEstoqueMinimo = endereco.QtEstoqueMinimo,
                QtEstoqueMaximo = endereco.QtEstoqueMaximo,
                FgStatus = endereco.FgStatus,
                TpPreenchimento = endereco.TpPreenchimento,
            };
        }



        private class CaixaInfoDto
        {
            public long? IdPedido { get; set; }
            public long? IdPrograma { get; set; }
            public Guid? IdAgrupador { get; set; }
        }

        private class PedidoInfoDto
        {
            public long? IdTransportadora { get; set; }
            public string TipoCargaPedido { get; set; } = "";
            public byte CanalPedido { get; set; }
            public long? CdLote { get; set; }
            public long? CdOrdemExportacao { get; set; }
            public int? CdVeiculoExportacao { get; set; }
        }

        private class PreenchimentoDto
        {
            public int IdPreenchimento { get; set; }
            public string TipoCargaPreenchimento { get; set; } = "";
            public long? IdTransportadora { get; set; }
            public byte CanalPreenchimento { get; set; }
            public bool EstoqueEstrategico { get; set; }
            public bool ForaDeLinha { get; set; }
        }

    }
}
