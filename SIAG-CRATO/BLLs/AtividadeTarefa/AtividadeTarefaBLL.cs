using Dapper;
using Microsoft.Data.SqlClient;

namespace SIAG_CRATO.BLLs.AtividadeTarefa;

public class AtividadeTarefaBLL
{
    public static async Task<List<(int idTarefa, int sequencia)>> GetListAsync(int idAtividade)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<(int idTarefa, int sequencia)>(AtividadeTarefaQuery.SELECT, new { idAtividade });

        return tarefas.ToList();
    }

    public static async Task<List<(int idTarefa, int sequencia)>> GetByAtividadeAsync(int idAtividade)
    {
        var sql = $"{AtividadeTarefaQuery.SELECT} WHERE id_atividade = @IdAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<(int idTarefa, int sequencia)>(sql, new { idAtividade });

        return tarefas.ToList();
    }
}
