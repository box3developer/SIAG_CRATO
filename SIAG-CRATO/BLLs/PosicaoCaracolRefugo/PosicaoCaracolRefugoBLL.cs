using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.PosicaoCaracolRefugo;

public class PosicaoCaracolRefugoBLL
{
    public static async Task<PosicaoCaracolRefugoModel?> GetByPosicao(int posicao)
    {
        var sql = $"{PosicaoCaracolRefugoQuery.SELECT} WHERE posicao = @posicao";

        using var conexao = new SqlConnection(Global.Conexao);
        var posicaoCaracol = await conexao.QueryFirstOrDefaultAsync<PosicaoCaracolRefugoModel>(sql, new { posicao });

        return posicaoCaracol;
    }

    public static async Task<PosicaoCaracolRefugoModel?> GetByTipo(string tipo, string? fabrica)
    {
        var sql = $"{PosicaoCaracolRefugoQuery.SELECT} WHERE tipo = @tipo";

        if (fabrica == null || fabrica.IsNullOrEmpty())
        {
            sql = $"{sql} AND fabrica IS NULL";
        }
        else
        {
            sql = $"{sql} AND fabrica = @fabrica";
        }

        using var conexao = new SqlConnection(Global.Conexao);
        var posicaoCaracol = await conexao.QueryFirstOrDefaultAsync<PosicaoCaracolRefugoModel>(sql, new { tipo, fabrica });

        return posicaoCaracol;
    }
}
