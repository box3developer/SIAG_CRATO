using Dapper;
using Microsoft.Data.SqlClient;
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
}
