﻿using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.EquipamentoManutencao;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.EquipamentoManutencao;

public class EquipamentoManutencaoBLL
{
    public static async Task<List<EquipamentoManutencaoDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoManutencaoModel>(EquipamentoManutencaoQuery.SELECT);

        return equipamentos.Select(ConvertToDTO).ToList();
    }

    public static async Task<EquipamentoManutencaoDTO?> GetByIdAsync(int id)
    {
        var sql = $@"{EquipamentoManutencaoQuery.SELECT} WHERE id_equipamento_manutencao = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoManutencaoModel>(sql, new { id });

        if (equipamento == null)
        {
            return null;
        }

        return ConvertToDTO(equipamento);
    }

    public static async Task<bool> SetDtFimByEquipamento(int id_equipamento)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var result = await conexao.ExecuteAsync(EquipamentoManutencaoQuery.UPDATE_DTFIM_BY_EQUIPAMENTO, new { id_equipamento });

        return result > 0;
    }

    private static EquipamentoManutencaoDTO ConvertToDTO(EquipamentoManutencaoModel equipamento)
    {
        return new()
        {
            IdEquipamentoManutencao = equipamento.IdEquipamentoManutencao,
            IdEquipamento = equipamento.IdEquipamento,
            FgTipoManutencao = equipamento.FgTipoManutencao,
            DtInicio = equipamento.DtInicio,
            DtFim = equipamento.DtFim,
        };
    }
}
