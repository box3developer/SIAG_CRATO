using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.ChamadaTarefa;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.ChamadaTarefa;

public class ChamadaTarefaBLL
{
    public static async Task<List<ChamadaTarefaDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<ChamadaTarefaModel>(ChamadaTarefaQuery.SELECT);

        return tarefas.Select(ConvertToDTO).ToList();
    }

    public static async Task<List<ChamadaTarefaDTO>> GetByIdAsync(Guid idChamada, int idTarefa)
    {
        string sql = $"{ChamadaTarefaQuery.SELECT} WHERE id_tarefa = @idTarefa AND id_chamada = @idChamada";

        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<ChamadaTarefaModel>(sql, new { idTarefa, idChamada });

        return tarefas.Select(ConvertToDTO).ToList();
    }

    public static async Task<bool> SetTarefaAsync(Guid idChamada, int idTarefa)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefa = await conexao.ExecuteAsync(ChamadaTarefaQuery.INSERT, new { idChamada, idTarefa });

        return tarefa > 0;
    }

    public static async Task<bool> UpdateTarefaAsync(ChamadaTarefaDTO tarefa)
    {
        string sql = $"{ChamadaTarefaQuery.UPDATE} WHERE id_tarefa = @idTarefa AND id_chamada = @idChamada";

        if (tarefa == null)
        {
            throw new Exception("Tarefa não definida");
        }

        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(sql, new
        {
            dataInicio = tarefa.DtInicio,
            dataFim = tarefa.DtFim,
            idTarefa = tarefa.IdTarefa,
            idChamada = tarefa.IdChamada,
        });

        return id > 0;
    }

    private static ChamadaTarefaDTO ConvertToDTO(ChamadaTarefaModel chamada)
    {
        return new()
        {
            IdTarefa = chamada.IdTarefa,
            IdChamada = chamada.IdChamada,
            DtInicio = chamada.DtInicio,
            DtFim = chamada.DtFim,
        };
    }
}
