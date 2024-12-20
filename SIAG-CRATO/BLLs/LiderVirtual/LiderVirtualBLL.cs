using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.LiderVirtual;

public class LiderVirtualBLL
{
    public static async Task<LiderVirtualModel?> GetByOperador(string cracha)
    {
        var sql = $"{LiderVirtualQuery.SELECT} WHERE id_operador = @cracha ORDER BY id_lidervirtual DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var liderVirtual = await conexao.QueryFirstOrDefaultAsync<LiderVirtualModel>(sql, new { cracha });

        return liderVirtual;
    }

    public static async Task<LiderVirtualModel?> GetByDestino(int idEquipamento)
    {
        var sql = $"{LiderVirtualQuery.SELECT} WHERE id_equipamentodestino = @idEquipamento ORDER BY id_lidervirtual DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var liderVirtual = await conexao.QueryFirstOrDefaultAsync<LiderVirtualModel>(sql, new { idEquipamento });

        return liderVirtual;
    }

    public static async Task<LiderVirtualModel?> GetByOrigem(int idEquipamento)
    {
        var sql = $"{LiderVirtualQuery.SELECT} WHERE id_equipamentoOrigem = @idEquipamento ORDER BY id_lidervirtual DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var liderVirtual = await conexao.QueryFirstOrDefaultAsync<LiderVirtualModel>(sql, new { idEquipamento });

        return liderVirtual;
    }

    public static async Task<int> Create(LiderVirtualModel liderVirtual)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(LiderVirtualQuery.INSERT, new
        {
            liderVirtual.IdOperador,
            liderVirtual.IdEquipamentoOrigem,
            liderVirtual.IdEquipamentoDestino,
            liderVirtual.DataLogin,
            liderVirtual.DataLogoff,
            liderVirtual.IdOperadorLogin,
            liderVirtual.DataLoginLimite,
        });

        return id;
    }

    public static async Task<int> Update(LiderVirtualModel liderVirtual)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(LiderVirtualQuery.UPDATE, new
        {
            liderVirtual.IdOperador,
            liderVirtual.IdEquipamentoOrigem,
            liderVirtual.IdEquipamentoDestino,
            liderVirtual.DataLogin,
            liderVirtual.DataLogoff,
            liderVirtual.IdOperadorLogin,
            liderVirtual.DataLoginLimite,
            liderVirtual.IdLiderVirtual,
        });

        return id;
    }
}
