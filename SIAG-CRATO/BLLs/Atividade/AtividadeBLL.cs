using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Atividade;

public class AtividadeBLL
{
    public static async Task<AtividadeModel?> GetAtividadeByIdAsync(int id)
    {
        var sql = $"{AtividadeQuery.SELECT} WHERE id_atividade = @idAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var atividade = await conexao.QueryFirstOrDefaultAsync<AtividadeModel>(sql, new { idAtividade = id });

        return atividade;
    }

    public static async Task<AtividadeModel?> GetAtividadeByNomeAsync(string nome)
    {
        var sql = $"{AtividadeQuery.SELECT} WHERE nm_atividade = @nomeAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var atividade = await conexao.QueryFirstOrDefaultAsync<AtividadeModel>(sql, new { nomeAtividade = nome });

        return atividade;
    }
}
