using Dapper;
using Microsoft.Data.SqlClient;

namespace SIAG_CRATO.BLLs.ChamadaTarefa;

public class ChamadaTarefaBLL
{
    public static async Task<List<(int idTarefa, int idChamada)>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<(int idTarefa, int idChamada)>(ChamadaTarefaQuery.SELECT);

        return tarefas.ToList();
    }

    public static async Task<bool> SetTarefaAsync(Guid idChamada, int idTarefa)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefa = await conexao.ExecuteAsync(ChamadaTarefaQuery.INSERT, new { idChamada, idTarefa });

        return tarefa > 0;
    }
}
