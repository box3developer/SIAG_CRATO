using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SIAG_CRATO.DTOs.AtividadeTarefa;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.AtividadeTarefa;

public class AtividadeTarefaBLL
{
    public static async Task<List<AtividadeTarefaDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<AtividadeTarefaModel>(AtividadeTarefaQuery.SELECT);

        return tarefas.Select(ConvertToDTO).ToList();
    }

    public static async Task<List<AtividadeTarefaDTO>> GetListAsync(AtividadeTarefaFiltroDTO tarefa)
    {
        string sql = $"{AtividadeTarefaQuery.SELECT} WHERE 1=1";
        var filtros = new Dictionary<string, object>();

        if (tarefa.Codigo > 0)
        {
            sql += " AND id_tarefa = @idTarefa";
            filtros.Add("idTarefa", tarefa.Codigo);
        }

        if (tarefa.Sequencia > 0)
        {
            sql += " AND cd_sequencia = @sequencia";
            filtros.Add("sequencia", tarefa.Sequencia);
        }

        if (tarefa.Recursos > 0)
        {
            sql += " AND fg_recurso = @recursos";
            filtros.Add("recursos", tarefa.Recursos);
        }

        if (!tarefa.Descricao.IsNullOrEmpty())
        {
            sql += " AND nm_tarefa like @nomeTarefa";
            filtros.Add("nomeTarefa", tarefa.Descricao);
        }

        if (!tarefa.Mensagem.IsNullOrEmpty())
        {
            sql += " AND nm_mensagem like @nomeTarefa";
            filtros.Add("nomeTarefa", $"%{tarefa.Mensagem}%");
        }

        if (tarefa.AtividadeId > 0)
        {
            sql += " AND id_atividade = @idAtividade";
            filtros.Add("idAtividade", tarefa.AtividadeId);
        }

        if (tarefa.AtividadeRotinaId > 0)
        {
            sql += " AND id_atividaderotina = @idAtividadeRotina";
            filtros.Add("idAtividadeRotina", tarefa.AtividadeRotinaId);
        }

        sql += " ORDER BY cd_sequencia";

        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<AtividadeTarefaModel>(sql, new DynamicParameters(filtros));

        return tarefas.Select(ConvertToDTO).ToList();
    }

    public static async Task<AtividadeTarefaDTO?> GetByIdAsync(int id)
    {
        var sql = $"{AtividadeTarefaQuery.SELECT} WHERE id_tarefa = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var tarefa = await conexao.QueryFirstOrDefaultAsync<AtividadeTarefaModel>(sql, new { id });

        if (tarefa == null)
        {
            return null;
        }

        return ConvertToDTO(tarefa);
    }

    public static async Task<List<AtividadeTarefaDTO>> GetByAtividadeAsync(int idAtividade)
    {
        var sql = $"{AtividadeTarefaQuery.SELECT} WHERE id_atividade = @idAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var tarefas = await conexao.QueryAsync<AtividadeTarefaModel>(sql, new { idAtividade });

        return tarefas.Select(ConvertToDTO).ToList();
    }

    private static AtividadeTarefaDTO ConvertToDTO(AtividadeTarefaModel atividade)
    {
        return new()
        {
            IdTarefa = atividade.IdTarefa,
            NmTarefa = atividade.NmTarefa,
            NmMensagem = atividade.NmMensagem,
            IdAtividade = atividade.IdAtividade,
            CdSequencia = atividade.CdSequencia,
            FgRecurso = atividade.FgRecurso,
            IdAtividadeRotina = atividade.IdAtividadeRotina,
            QtPotenciaNormal = atividade.QtPotenciaNormal,
            QtPotenciaAumentada = atividade.QtPotenciaAumentada,
        };
    }
}
