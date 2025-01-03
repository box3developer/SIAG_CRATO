using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.LiderVirtual;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.LiderVirtual;

public class LiderVirtualBLL
{
    public static async Task<LiderVirtualDTO?> GetByOperador(string cracha)
    {
        var sql = $"{LiderVirtualQuery.SELECT} WHERE id_operador = @cracha ORDER BY id_lidervirtual DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var liderVirtual = await conexao.QueryFirstOrDefaultAsync<LiderVirtualModel>(sql, new { cracha });

        if (liderVirtual == null)
        {
            return null;
        }

        return ConvertToDTO(liderVirtual);
    }

    public static async Task<LiderVirtualDTO?> GetByDestino(int idEquipamento)
    {
        var sql = $"{LiderVirtualQuery.SELECT} WHERE id_equipamentodestino = @idEquipamento ORDER BY id_lidervirtual DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var liderVirtual = await conexao.QueryFirstOrDefaultAsync<LiderVirtualModel>(sql, new { idEquipamento });

        if (liderVirtual == null)
        {
            return null;
        }

        return ConvertToDTO(liderVirtual);
    }

    public static async Task<LiderVirtualDTO?> GetByOrigem(int idEquipamento)
    {
        var sql = $"{LiderVirtualQuery.SELECT} WHERE id_equipamentoOrigem = @idEquipamento ORDER BY id_lidervirtual DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var liderVirtual = await conexao.QueryFirstOrDefaultAsync<LiderVirtualModel>(sql, new { idEquipamento });

        if (liderVirtual == null)
        {
            return null;
        }

        return ConvertToDTO(liderVirtual);
    }

    public static async Task<int> Create(LiderVirtualDTO liderVirtual)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(LiderVirtualQuery.INSERT, new
        {
            idOperador = liderVirtual.IdOperador,
            idEquipamentoOrigem = liderVirtual.IdEquipamentoOrigem,
            idEquipamentoDestino = liderVirtual.IdEquipamentoDestino,
            dtLogin = liderVirtual.DtLogin,
            dtLogoff = liderVirtual.DtLogoff,
            idOperadorLogin = liderVirtual.IdOperadorLogin,
            dtLoginLimite = liderVirtual.DtLoginLimite,
        });

        return id;
    }

    public static async Task<int> Update(LiderVirtualDTO liderVirtual)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(LiderVirtualQuery.UPDATE, new
        {
            idOperador = liderVirtual.IdOperador,
            idEquipamentoOrigem = liderVirtual.IdEquipamentoOrigem,
            idEquipamentoDestino = liderVirtual.IdEquipamentoDestino,
            dtLogin = liderVirtual.DtLogin,
            dtLogoff = liderVirtual.DtLogoff,
            idOperadorLogin = liderVirtual.IdOperadorLogin,
            dtLoginLimite = liderVirtual.DtLoginLimite,
            idLiderVirtual = liderVirtual.IdLiderVirtual,
        });

        return id;
    }

    private static LiderVirtualDTO ConvertToDTO(LiderVirtualModel lider)
    {
        return new()
        {
            IdLiderVirtual = lider.IdLiderVirtual,
            IdOperador = lider.IdOperador,
            IdEquipamentoOrigem = lider.IdEquipamentoOrigem,
            IdEquipamentoDestino = lider.IdEquipamentoDestino,
            DtLogin = lider.DtLogin,
            DtLogoff = lider.DtLogoff,
            IdOperadorLogin = lider.IdOperadorLogin,
            DtLoginLimite = lider.DtLoginLimite,
        };
    }
}
