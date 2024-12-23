using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Chamada;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.ChamadaTarefa;

public class ChamadaTarefaBLL
{
    public static async Task<List<ChamadaTarefaModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<ChamadaTarefaModel>(ChamadaTarefaQuery.SELECT);

        return tarefas.ToList();
    }

    public static async Task<List<ChamadaTarefaModel>> GetByIdAsync(Guid idChamada, int idTarefa)
    {
        string sql = $"{ChamadaQuery.SELECT} WHERE id_tarefa = @idTarefa AND id_chamada = @idChamada";

        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<ChamadaTarefaModel>(sql, new { idTarefa, idChamada });

        return tarefas.ToList();
    }

    public static async Task<bool> SetTarefaAsync(Guid idChamada, int idTarefa)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefa = await conexao.ExecuteAsync(ChamadaTarefaQuery.INSERT, new { idChamada, idTarefa });

        return tarefa > 0;
    }

    public static async Task<bool> UpdateTarefaAsync(ChamadaTarefaModel tarefa)
    {
        string sql = $"{ChamadaTarefaQuery.UPDATE} WHERE id_tarefa = @idTarefa AND id_chamada = @idChamada";

        if (tarefa == null)
        {
            throw new Exception("Tarefa não definida");
        }

        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(sql, new
        {
            dataInicio = tarefa.DataInicio,
            dataFim = tarefa.DataFim,
            idTarefa = tarefa.TarefaId,
            idChamada = tarefa.ChamadaId,
        });

        return id > 0;
    }
}
