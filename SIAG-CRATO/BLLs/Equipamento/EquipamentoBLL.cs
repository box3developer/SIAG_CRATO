﻿using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Equipamento;

public class EquipamentoBLL
{
    public static async Task<List<EquipamentoModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(EquipamentoQuery.SELECT);

        return equipamentos.ToList();
    }

    public static async Task<EquipamentoModel?> GetByIdAsync(int id)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamento = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { id });

        return equipamento;
    }

    public static async Task<EquipamentoModel?> GetByOperadorAsync(string cracha)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_operador = @cracha";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { cracha });

        return equipamento;
    }

    public static async Task<List<EquipamentoModel>> GetByModeloAsync(int idModelo)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = @idModelo";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(sql, new { idModelo });

        return equipamentos.ToList();
    }
}