using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Setor;

public class SetorBLL
{
    public static async Task<SetorModel?> GetById(int id)
    {
        var sql = $"{SetorQuery.SELECT} WHERE id_setortrabalho = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var setor = await conexao.QueryFirstOrDefaultAsync<SetorModel>(sql, new { id });

        return setor;
    }
}
