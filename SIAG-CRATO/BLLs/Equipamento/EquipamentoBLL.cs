using Dapper;
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

    public static async Task<List<EquipamentoModel>> GetAllCaracois()
    {
        var sql = $"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = 1";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(sql);

        return equipamentos.ToList();
    }

    public static async Task<EquipamentoModel?> GetByIdAsync(int id)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamento = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { id });

        return equipamento;
    }

    public static async Task<EquipamentoModel?> GetByCaracolAsync(string identificadorCaracol)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE nm_identificador = @identificadorCaracol";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { identificadorCaracol });

        return equipamento;
    }

    public static async Task<EquipamentoModel?> GetByOperadorAsync(string cracha)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_operador = @cracha";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { cracha });

        return equipamento;
    }

    public static async Task<EquipamentoModel?> GetByCaixaPendenteAsync(string idCaixa)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE cd_leitura_pendente = @cdCaixaPendente";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { idCaixa });

        return equipamento;
    }

    public static async Task<List<EquipamentoModel>> GetByModeloAsync(int idModelo)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = @idModelo";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(sql, new { idModelo });

        return equipamentos.ToList();
    }

    public static async Task<bool> SetCaixaPendente(string? idCaixa, string idEquipamento)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_PENDENTE, new { idCaixa, idEquipamento });

        return id > 0;
    }

    public static async Task<bool> UpdateLeitura(int idEquipamento)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_LEITURA, new { idEquipamento });

        return id > 0;
    }
}

