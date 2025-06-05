using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Atividade;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Atividade;

public class AtividadeBLL
{
    public static async Task<List<AtividadeDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var atividades = await conexao.QueryAsync<AtividadeModel>(AtividadeQuery.SELECT);

        return atividades.Select(ConvertToDTO).ToList();
    }

    public static async Task<AtividadeDTO?> GetByIdAsync(int id)
    {
        var sql = $"{AtividadeQuery.SELECT} WHERE id_atividade = @idAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var atividade = await conexao.QueryFirstOrDefaultAsync<AtividadeModel>(sql, new { idAtividade = id });

        if (atividade == null)
        {
            return null;
        }

        return ConvertToDTO(atividade);
    }

    public static async Task<AtividadeDTO?> GetByNomeAsync(string nome)
    {
        var sql = $"{AtividadeQuery.SELECT} WHERE nm_atividade = @nomeAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var atividade = await conexao.QueryFirstOrDefaultAsync<AtividadeModel>(sql, new { nomeAtividade = nome });

        if (atividade == null)
        {
            return null;
        }

        return ConvertToDTO(atividade);
    }

    public static async Task<List<AtividadeDTO>> GetByEquipModeloSetor(int id_equipamentomodelo, int id_setortrabalho)
    {
        if (id_equipamentomodelo == 0 || id_setortrabalho == 0)
        {
            return [];
        }

        var sql = $@"{AtividadeQuery.SELECT} WHERE id_equipamentomodelo = @id_equipamentomodelo and id_setortrabalho = @id_setortrabalho";

        using var conexao = new SqlConnection(Global.Conexao);
        var lista = await conexao.QueryAsync<AtividadeModel>(sql, new { id_equipamentomodelo, id_setortrabalho });

        return lista.Select(ConvertToDTO).ToList();
    }

    public static async Task<List<AtividadePrioridadeModel>> GetListAtividadePrioridade(int idAtividade)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var lista = await conexao.QueryAsync<AtividadePrioridadeModel>(AtividadeQuery.SELECT_PRIORIDADE, new { idAtividade });

        return lista.ToList();
    }

    private static AtividadeDTO ConvertToDTO(AtividadeModel atividade)
    {
        return new()
        {
            IdAtividade = atividade.IdAtividade,
            NmAtividade = atividade.NmAtividade,
            IdEquipamentoModelo = atividade.IdEquipamentoModelo,
            IdAtividadeRotinaValidacao = atividade.IdAtividadeRotinaValidacao,
            IdAtividadeAnterior = atividade.IdAtividadeAnterior,
            IdSetorTrabalho = atividade.IdSetorTrabalho,
            FgPermiteRejeitar = atividade.FgPermiteRejeitar,
            FgTipoAtribuicaoAutomatica = atividade.FgTipoAtribuicaoAutomatica,
            FgEvitaConflitoEndereco = atividade.FgEvitaConflitoEndereco,
            FgTipoAtividade = atividade.FgTipoAtividade,
            Duracao = atividade.Duracao,
        };
    }
}