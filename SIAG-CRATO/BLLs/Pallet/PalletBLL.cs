﻿using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Pallet;

public class PalletBLL
{
    public async Task<int> InsertAsync(PalletModel pallet)
    {
        var parametros = new Dictionary<string, object>
        {
            { "@Codigo", pallet.Codigo },
            { "@Status", pallet.Status },
            { "@QtdUtilizacao", pallet.QtUtilizacao },
            { "@AreaArmazenagem", pallet.AreaArmazenagemId > 0 ? pallet.AreaArmazenagemId : DBNull.Value},
            { "@Agrupador", pallet.AgrupadorId > 0 ? pallet.AgrupadorId : DBNull.Value },
            { "@DataUltimaMovimentacao", pallet.DataUltimaMovimentacao != null ? pallet.DataUltimaMovimentacao : DBNull.Value },
            { "@Identificacao", pallet.Identificacao != null ? pallet.Identificacao : DBNull.Value},
        };

        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(PalletQuery.SELECT, new DynamicParameters(parametros));

        return id;
    }

    public async Task<List<PalletModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var pallets = await conexao.QueryAsync<PalletModel>(PalletQuery.SELECT);

        return pallets.ToList() ?? [];
    }

    public async Task<PalletModel?> GetPalletByIdAsync(int idPallet)
    {
        string sql = $"{PalletQuery.SELECT} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var pallets = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sql, new { idPallet });

        return pallets;
    }

    public async Task<PalletModel?> GetPalletByIdentificadorAsync(string identificador)
    {
        string sql = $"{PalletQuery.SELECT} WHERE cd_identificacao = @identificador";

        using var conexao = new SqlConnection(Global.Conexao);
        var pallets = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sql, new { identificador });

        return pallets;
    }

    public async Task<int> GetQuatidadeCaixasAsync(int idPallet)
    {
        string query = $"{PalletQuery.SELECT_COUNT_CAIXAS} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var pallets = await conexao.QueryFirstOrDefaultAsync<int>(query, new { idPallet });

        return 0;
    }

    public async Task<bool> VincularNovoPalletPorPrioridade(string? identificadorCaracol, AreaArmazenagemModel areaAtual, int nivelAgrupador)
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
        var palletNovo = await conexao.QueryFirstOrDefaultAsync<PalletModel>(sqlReserva, new { identificadorCaracol, idAgrupadorReservado = areaAtual.Agrupador.Codigo });

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
            idAreaArmazenagem = areaAtual.Codigo
        });

        if (!livreSemPrioridade)
        {
            var sqlAgrupadorDestino = @"UPDATE agrupadorativo 
                                        SET id_areaarmazenagem = null, fg_status = 2 
                                        WHERE id_agrupador = @idAgrupador";

            await conexao.ExecuteAsync(sqlAgrupadorDestino, new { idAgrupador = palletNovo.AgrupadorId });
        }

        await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = areaAtual.Agrupador.Codigo,
            status = 2,
            idAreaArmazenagem = palletNovo.AreaArmazenagemId,
        });

        var sqlAgrupadorOrigem = @"UPDATE agrupadorativo 
                                   SET id_areaarmazenagem = @idAreaArmazenagem
                                   WHERE id_agrupador = @idAgrupador";

        await conexao.ExecuteAsync(sqlAgrupadorOrigem, new { idAreaArmazenagem = palletNovo.AreaArmazenagemId, idAgrupador = areaAtual.Agrupador.Codigo });

        return true;
    }

    public async Task<bool> VincularNovoPalletDisponivel(string? identificadorCaracol, AreaArmazenagemModel areaAtual)
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
            idAgrupador = areaAtual.Agrupador.Codigo,
            status = 2,
            idAreaArmazenagem = areaAtual.Codigo
        });

        var linhasAreaAntiga = await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = DBNull.Value,
            status = 1,
            idAreaArmazenagem = areaAtual.Codigo
        });

        var sqlAgrupador = @"UPDATE agrupadorativo 
                             SET id_areaarmazenagem = @idAreaArmazenagem 
                             WHERE id_agrupador = @idAgrupador";

        var linhasAgrupador = await conexao.ExecuteAsync(sqlAgrupador, new { idAreaArmazenagem = areaAtual.Codigo, idAgrupador = areaAtual.Agrupador.Codigo });

        return linhasArea > 0 && linhasAreaAntiga > 0 && linhasAgrupador > 0;
    }

    public static async Task<bool> VincularNovoPalletReservado(string? identificadorCaracol, AreaArmazenagemModel areaAtual)
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
            idAreaArmazenagem = areaAtual.Codigo
        });

        if (!reservadaSemAgrupador)
        {
            var sqlAgrupadorDestino = @"UPDATE agrupadorativo 
                                        SET id_areaarmazenagem = null, fg_status = 2 
                                        WHERE id_agrupador = @idAgrupador";

            await conexao.ExecuteAsync(sqlAgrupadorDestino, new { idAgrupador = palletNovo.AgrupadorId });
        }

        await conexao.ExecuteAsync(PalletQuery.UPDATE_AREAARMAZENAGEM, new
        {
            idAgrupador = areaAtual.Agrupador.Codigo,
            status = 2,
            idAreaArmazenagem = palletNovo.AreaArmazenagemId
        });

        var sqlAgrupadorOrigem = @"UPDATE agrupadorativo 
                                   SET id_areaarmazenagem = @idAreaArmazenagem 
                                   WHERE id_agrupador = @idAgrupador";

        await conexao.ExecuteAsync(sqlAgrupadorOrigem, new { idAreaArmazenagem = palletNovo.AreaArmazenagemId, idAgrupador = areaAtual.Agrupador.Codigo });

        return true;
    }
}