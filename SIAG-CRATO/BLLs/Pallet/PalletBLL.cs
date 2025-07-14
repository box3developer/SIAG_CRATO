using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Caixa;
using SIAG_CRATO.BLLs.Log;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.AreaArmazenagem;
using SIAG_CRATO.DTOs.Pallet;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Pallet;

public class PalletBLL
{
    public static async Task<int> InsertAsync(PalletDTO pallet)
    {
        var parametros = new Dictionary<string, object>
        {
            { "@Codigo", pallet.IdPallet },
            { "@Status", pallet.FgStatus },
            { "@QtdUtilizacao", pallet.QtUtilizacao },
            { "@AreaArmazenagem", pallet.IdAreaArmazenagem == null? DBNull.Value: pallet.IdAreaArmazenagem},
            { "@Agrupador", pallet.IdAgrupador == Guid.Empty ? pallet.IdAgrupador : DBNull.Value },
            { "@DataUltimaMovimentacao", pallet.DtUltimaMovimentacao != null ? pallet.DtUltimaMovimentacao : DBNull.Value },
            { "@Identificacao", pallet.CdIdentificacao != null ? pallet.CdIdentificacao : DBNull.Value},
        };

        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(PalletQuery.SELECT, new DynamicParameters(parametros));

        return id;
    }

    public static async Task<List<PalletDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var pallets = await conexao.QueryAsync<PalletModel>(PalletQuery.SELECT);

        return pallets.Select(ConvertToDTO).ToList();
    }

    public static async Task<PalletDTO?> GetByIdAsync(int idPallet)
    {
        string sql = $"{PalletQuery.SELECT} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var pallet = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sql, new { idPallet });

        if (pallet == null)
        {
            return null;
        }

        return ConvertToDTO(pallet);
    }

    public static async Task<PalletDTO?> GetByIdentificadorAsync(string identificador, int id = 0)
    {
        string sql = $"{PalletQuery.SELECT} WHERE cd_identificacao = @identificador";

        if (id == 0)
        {
            sql = $"{sql} AND id_pallet <> @id";
        }

        using var conexao = new SqlConnection(Global.Conexao);
        var pallet = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sql, new { identificador, id });

        if (pallet == null)
        {
            return null;
        }

        return ConvertToDTO(pallet);
    }

    public static async Task<PalletDTO?> GetByAreaArmazenagemAsync(long idAreaArmazenagem)
    {
        string sql = $"{PalletQuery.SELECT} WHERE id_areaarmazenagem = @idAreaArmazenagem";

        using var conexao = new SqlConnection(Global.Conexao);
        var pallet = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sql, new { idAreaArmazenagem });

        if (pallet == null)
        {
            return null;
        }

        return ConvertToDTO(pallet);
    }

    public static async Task<bool> SetPalletCheio(int idPallet, Guid? id_requisicao)
    {
        try
        {
            var logInitial = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = "",
                IdCaixa = "",
                Data = DateTime.Now,
                Mensagem = $"Trocando status do pallet {idPallet} para cheio",
                Metodo = "SetPalletCheito",
                IdOperador = "",
                Tipo = "info",
            };

            await LogBLL.CreateLogCaracol(logInitial);

            var result = await SeStatusAsync(idPallet, StatusPallet.Cheio);

            return result > 0;


        }
        catch (Exception ex)
        {
            var logError = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = "",
                IdCaixa = "",
                Data = DateTime.Now,
                Mensagem = $"Erro ao trocar status do pallet {idPallet} para cheio",
                Metodo = "SetPalletCheio",
                IdOperador = "",
                Tipo = "erro",
            };

            await LogBLL.CreateLogCaracol(logError);

            throw new Exception(ex.Message);
        }

    }

    public static async Task<int> GetQuantidadeCaixasAsync(int idPallet)
    {
        string query = $"{PalletQuery.SELECT_COUNT_CAIXAS} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var pallets = await conexao.QueryFirstOrDefaultAsync<int>(query, new { idPallet });

        return 0;
    }

    public static async Task<int> SeStatusAsync(int id, StatusPallet status)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var pallet = await conexao.ExecuteAsync(PalletQuery.UPDADE_STATUS, new { status = (int)status, id });

        return pallet;
    }

    public static async Task<bool> SetStatusAndAgrupadorById(int idPallet, StatusPallet status, Guid idAgrupador)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var qtd = await conexao.ExecuteAsync(PalletQuery.UPDATE_AGRUPADOR_STATUS_BY_ID, new { idPallet, status = (int)status, idAgrupador });

        return qtd > 0;
    }

    public static async Task<bool> VincularAgrupadorAreaReservadaAsync(string? identificadorCaracol, AreaArmazenagemDTO areaAtual)
    {
        var sqlReserva = $@"{PalletQuery.SELECT_RESERVA} 
                            WHERE 
                                CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol 
                                AND areaarmazenagem.id_agrupador IS NULL 
                                AND areaarmazenagem.id_agrupador_reservado = @idAgrupadorReservado 
                                AND pallet.fg_status = 1 
                                AND (areaarmazenagem.fg_status = 2 or areaarmazenagem.fg_status = 1) 
                            ORDER BY nr_posicaoy";

        using var conexao = new SqlConnection(Global.Conexao);
        var palletNovo = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlReserva, new { identificadorCaracol, idAgrupadorReservado = areaAtual.IdAgrupador });

        if (palletNovo == null)
        {
            return false;
        }

        await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = DBNull.Value,
            status = 1,
            idAreaArmazenagem = areaAtual.IdAreaArmazenagem
        });

        if (palletNovo.IdAgrupador == Guid.Empty)
        {
            throw new Exception("Pallet novo ser agrupador.");
        }

        var sqlAgrupadorDestino = @"UPDATE agrupadorativo 
                                    SET id_areaarmazenagem = null, fg_status = 2 
                                    WHERE id_agrupador = @idAgrupador";

        await conexao.ExecuteAsync(sqlAgrupadorDestino, new { idAgrupador = palletNovo.IdAgrupador });

        await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = areaAtual.IdAgrupador,
            status = 2,
            idAreaArmazenagem = palletNovo.IdAreaArmazenagem,
        });

        var sqlAgrupadorOrigem = @"UPDATE agrupadorativo 
                                   SET id_areaarmazenagem = @idAreaArmazenagem
                                   WHERE id_agrupador = @idAgrupador";

        await conexao.ExecuteAsync(sqlAgrupadorOrigem, new { idAreaArmazenagem = palletNovo.IdAreaArmazenagem, idAgrupador = areaAtual.IdAgrupador });

        return true;
    }

    public static async Task<bool> VincularNovoPalletPorPrioridadeAsync(string? identificadorCaracol, AreaArmazenagemDTO areaAtual, int nivelAgrupador)
    {
        bool livreSemPrioridade = false;
        var sqlReserva = $@"{PalletQuery.SELECT_RESERVA} 
                            WHERE 
                                CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol 
                                AND areaarmazenagem.id_agrupador IS NULL 
                                AND areaarmazenagem.id_agrupador_reservado = @idAgrupadorReservado 
                                AND pallet.fg_status = 1 
                                AND (areaarmazenagem.fg_status = 2 or areaarmazenagem.fg_status = 1) 
                            ORDER BY nr_posicaoy";

        using var conexao = new SqlConnection(Global.Conexao);
        var palletNovo = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlReserva, new { identificadorCaracol, idAgrupadorReservado = areaAtual.IdAgrupador });

        if (palletNovo == null)
        {
            var sqlLivreSemPrioridade = $@"{PalletQuery.SELECT_RESERVA} 
                                           LEFT JOIN prioridadesareasarmazenagem as p on p.id_areaarmazenagem = areaarmazenagem.id_areaarmazenagem 
                                           WHERE 
                                               CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol 
	                                           AND p.nr_prioridade1 < 1 
                                               AND p.nr_prioridade2 < 1 
                                               AND areaarmazenagem.id_agrupador IS NULL 
                                               AND pallet.fg_status = 1 
                                               AND areaarmazenagem.fg_status = 1 
                                           ORDER BY nr_posicaoy";

            palletNovo = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlLivreSemPrioridade, new { identificadorCaracol });

            if (palletNovo == null)
            {
                var sqlLivreComPrioridade = $@"{PalletQuery.SELECT_RESERVA} 
                                               LEFT JOIN prioridadesareasarmazenagem as p on p.id_areaarmazenagem = areaarmazenagem.id_areaarmazenagem 
                                               WHERE 
                                                   CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol 
	                                               AND (p.nr_prioridade1 > 0 OR p.nr_prioridade2 > 0) 
                                                   AND areaarmazenagem.id_agrupador IS NULL 
                                                   AND pallet.fg_status = 1 
                                                   AND areaarmazenagem.fg_status = 1 
                                               ORDER BY nr_posicaoy";

                palletNovo = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlLivreSemPrioridade, new { identificadorCaracol, nivelAgrupadorPrioridade1 = nivelAgrupador });
            }
            else
            {
                livreSemPrioridade = true;
            }
        }

        if (palletNovo == null)
        {
            return false;
        }

        await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = DBNull.Value,
            status = 1,
            idAreaArmazenagem = areaAtual.IdAreaArmazenagem
        });

        if (!livreSemPrioridade)
        {
            var sqlAgrupadorDestino = @"UPDATE agrupadorativo 
                                        SET id_areaarmazenagem = null, fg_status = 2 
                                        WHERE id_agrupador = @idAgrupador";

            await conexao.ExecuteAsync(sqlAgrupadorDestino, new { idAgrupador = palletNovo.IdAgrupador });
        }

        await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = areaAtual.IdAgrupador,
            status = 2,
            idAreaArmazenagem = palletNovo.IdAreaArmazenagem,
        });

        var sqlAgrupadorOrigem = @"UPDATE agrupadorativo 
                                   SET id_areaarmazenagem = @idAreaArmazenagem
                                   WHERE id_agrupador = @idAgrupador";

        await conexao.ExecuteAsync(sqlAgrupadorOrigem, new { idAreaArmazenagem = palletNovo.IdAreaArmazenagem, idAgrupador = areaAtual.IdAgrupador });

        return true;
    }

    public static async Task<bool> VincularNovoPalletDisponivelAsync(string? identificadorCaracol, AreaArmazenagemDTO areaAtual)
    {
        var sqlReserva = $@"{PalletQuery.SELECT_RESERVA}
                            LEFT JOIN agrupadorativo ON agrupadorativo.id_areaarmazenagem = areaarmazenagem.id_areaarmazenagem AND agrupadorativo.fg_status = 3
                            WHERE 
                                CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
                                AND areaarmazenagem.id_agrupador IS NULL
                                AND agrupadorativo.id_agrupador IS NULL
                                AND pallet.fg_status = 1
                                AND areaarmazenagem.fg_status = 1
                            ORDER BY nr_posicaoy";

        using var conexao = new SqlConnection(Global.Conexao);
        var palletNovo = conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlReserva, new { identificadorCaracol });

        if (palletNovo == null)
        {
            return false;
        }

        var linhasArea = await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = areaAtual.IdAgrupador,
            status = 2,
            idAreaArmazenagem = areaAtual.IdAreaArmazenagem
        });

        var linhasAreaAntiga = await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = DBNull.Value,
            status = 1,
            idAreaArmazenagem = areaAtual.IdAreaArmazenagem
        });

        var sqlAgrupador = @"UPDATE agrupadorativo 
                             SET id_areaarmazenagem = @idAreaArmazenagem 
                             WHERE id_agrupador = @idAgrupador";

        var linhasAgrupador = await conexao.ExecuteAsync(sqlAgrupador, new { idAreaArmazenagem = areaAtual.IdAreaArmazenagem, idAgrupador = areaAtual.IdAgrupador });

        return linhasArea > 0 && linhasAreaAntiga > 0 && linhasAgrupador > 0;
    }

    public static async Task<bool> VincularNovoPalletReservadoAsync(string? identificadorCaracol, AreaArmazenagemDTO areaAtual)
    {
        var reservadaSemAgrupador = false;
        var sqlReseva = $@"{PalletQuery.SELECT_RESERVA}
                           WHERE 
                               CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
                               AND areaarmazenagem.id_agrupador IS NULL
                               AND pallet.fg_status = 1
                               AND areaarmazenagem.fg_status = 2
                           ORDER BY nr_posicaoy";

        using var conexao = new SqlConnection(Global.Conexao);
        var palletNovo = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlReseva, new { identificadorCaracol });

        if (palletNovo != null)
        {
            reservadaSemAgrupador = true;
        }
        else
        {
            var sqlReservadaMenosCaixasSemSorter = $@"{PalletQuery.SELECT_RESERVA}
                                                      LEFT JOIN agrupadorativo WITH(NOLOCK) ON agrupadorativo.id_agrupador = areaarmazenagem.id_agrupador
                                                      LEFT JOIN caixa WITH(NOLOCK) ON agrupadorativo.id_agrupador = caixa.id_agrupador
                                                      WHERE 
											             (caixa.fg_status < 4 OR caixa.fg_status = 8) 
                                                          AND caixa.dt_sorter IS NULL
											              AND CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
											              AND areaarmazenagem.id_endereco < 6
                                                          AND agrupadorativo.id_agrupador IS NOT NULL
                                                          AND pallet.fg_status = 1
                                                          AND areaarmazenagem.fg_status = 2
                                                      GROUP BY 
                                                          pallet.id_pallet, 
                                                          pallet.id_areaarmazenagem, 
                                                          agrupadorativo.id_agrupador, 
                                                          pallet.fg_status, 
                                                          areaarmazenagem.nr_posicaoy
                                                      ORDER BY COUNT (*), areaarmazenagem.nr_posicaoy";

            palletNovo = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlReservadaMenosCaixasSemSorter, new { identificadorCaracol });

            if (palletNovo == null)
            {
                var sqlReservadaMenosCaixasSorter = $@"{PalletQuery.SELECT_RESERVA}
                                                       LEFT JOIN agrupadorativo WITH(NOLOCK) ON agrupadorativo.id_agrupador = areaarmazenagem.id_agrupador
                                                       LEFT JOIN caixa WITH(NOLOCK) ON agrupadorativo.id_agrupador = caixa.id_agrupador
                                                       WHERE 
											              (caixa.fg_status < 4 OR caixa.fg_status = 8) 
                                                           AND caixa.dt_sorter IS NOT NULL
											               AND CAST(id_endereco AS varchar(10)) + RIGHT('00' + CAST(nr_posicaox AS varchar(10)), 2) = @identificadorCaracol
											               AND areaarmazenagem.id_endereco < 6
                                                           AND agrupadorativo.id_agrupador IS NOT NULL
                                                           AND pallet.fg_status = 1
                                                           AND areaarmazenagem.fg_status = 2
                                                       GROUP BY 
                                                           pallet.id_pallet, 
                                                           pallet.id_areaarmazenagem, 
                                                           agrupadorativo.id_agrupador, 
                                                           pallet.fg_status, 
                                                           areaarmazenagem.nr_posicaoy
                                                       ORDER BY COUNT (*), areaarmazenagem.nr_posicaoy";

                palletNovo = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlReservadaMenosCaixasSemSorter, new { identificadorCaracol });
            }
        }

        if (palletNovo == null)
        {
            return false;
        }

        await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = DBNull.Value,
            status = 1,
            idAreaArmazenagem = areaAtual.IdAreaArmazenagem
        });

        if (!reservadaSemAgrupador)
        {
            var sqlAgrupadorDestino = @"UPDATE agrupadorativo 
                                        SET id_areaarmazenagem = null, fg_status = 2 
                                        WHERE id_agrupador = @idAgrupador";

            await conexao.ExecuteAsync(sqlAgrupadorDestino, new { idAgrupador = palletNovo.IdAgrupador });
        }

        await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = areaAtual.IdAgrupador,
            status = 2,
            idAreaArmazenagem = palletNovo.IdAreaArmazenagem
        });

        var sqlAgrupadorOrigem = @"UPDATE agrupadorativo 
                                   SET id_areaarmazenagem = @idAreaArmazenagem 
                                   WHERE id_agrupador = @idAgrupador";

        await conexao.ExecuteAsync(sqlAgrupadorOrigem, new { idAreaArmazenagem = palletNovo.IdAreaArmazenagem, idAgrupador = areaAtual.IdAgrupador });

        return true;
    }

    //sp_siag_busca_qtde_pallets
    public static async Task<int> GetQtyPallets(int id_endereco, int id_pallet)
    {
        var pallet = await GetByIdAsync(id_pallet);
        var caixa = await CaixaBLL.GetByPalletAsync(id_pallet);
        var idAgrupador = pallet?.IdAgrupador ?? caixa.FirstOrDefault()?.IdAgrupador ??
            throw new Exception("Erro ao executar GetQtyPallets");


        var sql = $@"{PalletQuery.COUNT_PALLETS}";
        using var conexao = new SqlConnection(Global.Conexao);

        var quantity = await conexao.ExecuteScalarAsync<int>(sql, new { id_endereco, id_agrupador = idAgrupador });

        return quantity;

    }

    public static async Task<int> SetAreaArmazenagem(int id, long? idAreaArmazenagem = null)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var pallet = await conexao.ExecuteAsync(PalletQuery.DESALOCA_PALLET, new { idAreaArmazenagem, id });

        return pallet;
    }

    private static PalletDTO ConvertToDTO(PalletModel pallet)
    {
        return new()
        {
            IdPallet = pallet.IdPallet,
            CdIdentificacao = pallet.CdIdentificacao,
            QtUtilizacao = pallet.QtUtilizacao,
            IdAreaArmazenagem = pallet.IdAreaArmazenagem,
            IdAgrupador = pallet.IdAgrupador,
            FgStatus = pallet.FgStatus,
            DtUltimaMovimentacao = pallet.DtUltimaMovimentacao,
        };
    }
}