﻿using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.BLLs.EquipamentoManutencao;
using SIAG_CRATO.BLLs.OperadorHistorico;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Operador;

public class OperadorBLL
{
    public static async Task<OperadorModel?> GetByCrachaAsync(string cracha)
    {
        var sql = $"{OperadorQuery.SELECT} WHERE id_operador = @cracha";

        using var conexao = new SqlConnection(Global.Conexao);
        var operador = await conexao.QueryFirstOrDefaultAsync<OperadorModel>(sql, new { cracha });

        return operador;
    }

    public static async Task<int> GetMetaAsync()
    {
        var sql = $"{OperadorQuery.SELECT_PARAMETRO} WHERE nm_parametro = 'Caixa hora operador sorter'";

        using var conexao = new SqlConnection(Global.Conexao);
        var meta = await conexao.QueryFirstOrDefaultAsync<int>(sql);

        return meta;
    }

    public static async Task<bool> Login(int id_operador,int id_equipamento)
    {

        await LogOff(id_operador, id_equipamento);

        await EquipamentoBLL.SetEquipamentoOperador(id_operador, id_equipamento);
        await EquipamentoManutencaoBLL.SetDtFimByEquipamento(id_equipamento);
        await OperadorHistoricoBLL.SetHistorico(id_operador, id_equipamento);
        //....sp_siag_alocacaoautomaticabilaterais
        return true;
    }

    public static async Task<bool> LogOff(int id_operador, int id_equipamento)
    {
        if(id_operador == 0 || id_equipamento == 0)
        {
            return false;
        }
        var getEquip = await EquipamentoBLL.GetByOperadorAsync(id_operador.ToString()) ?? await EquipamentoBLL.GetByIdAsync(id_equipamento);

        if(getEquip == null)
        {
            return false;
        }

        using var conexao = new SqlConnection(Global.Conexao);
        var result = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_EQUIPAMENTO_OPERADOR_LOGOFF, new { id_equipamento = getEquip.Codigo });

        //....sp_siag_alocacaoautomaticabilaterais
        return result > 0;
    }
}
