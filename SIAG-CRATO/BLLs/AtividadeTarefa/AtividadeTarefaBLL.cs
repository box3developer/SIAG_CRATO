using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.AtividadeTarefa;

public class AtividadeTarefaBLL
{
    public static async Task<List<AtividadeTarefaModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<AtividadeTarefaModel>(AtividadeTarefaQuery.SELECT);

        return tarefas.ToList();
    }

    public static async Task<AtividadeTarefaModel?> GetByIdAsync(int id)
    {
        var sql = $"{AtividadeTarefaQuery.SELECT} WHERE id_tarefa = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var tarefa = await conexao.QueryFirstOrDefaultAsync<AtividadeTarefaModel>(sql, new { id });

        return tarefa;
    }

    public static async Task<List<AtividadeTarefaModel>> GetByAtividadeAsync(int idAtividade)
    {
        var sql = $"{AtividadeTarefaQuery.SELECT} WHERE id_atividade = @idAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<AtividadeTarefaModel>(sql, new { idAtividade });

        return tarefas.ToList();
    }


}
