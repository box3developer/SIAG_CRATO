using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Atividade;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Atividade;

public class AtividadeBLL
{
    public static async Task<List<AtividadeModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var atividades = await conexao.QueryAsync<AtividadeModel>(AtividadeQuery.SELECT);

        return atividades.ToList();
    }

    public static async Task<AtividadeModel?> GetByIdAsync(int id)
    {
        var sql = $"{AtividadeQuery.SELECT} WHERE id_atividade = @idAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var atividade = await conexao.QueryFirstOrDefaultAsync<AtividadeModel>(sql, new { idAtividade = id });

        return atividade;
    }

    public static async Task<AtividadeModel?> GetByNomeAsync(string nome)
    {
        var sql = $"{AtividadeQuery.SELECT} WHERE nm_atividade = @nomeAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var atividade = await conexao.QueryFirstOrDefaultAsync<AtividadeModel>(sql, new { nomeAtividade = nome });

        return atividade;
    }

    public static async Task<List<AtividadeModel>> GetByEquipModeloSetor(int id_equipamentomodelo, int id_setortrabalho)
    {
        if (id_equipamentomodelo == 0 || id_setortrabalho == 0)
        {
            return [];
        }

        var sql = $@"{AtividadeQuery.SELECT} WHERE id_equipamentomodelo = @id_equipamentomodelo and id_setortrabalho = @id_setortrabalho";
        using var conexao = new SqlConnection(Global.Conexao);

        var list = await conexao.QueryAsync<AtividadeModel>(sql, new { id_equipamentomodelo, id_setortrabalho });

        return list.ToList();

    }

    private static AtividadeDTO ConvertToDTO(AtividadeModel atividade)
    {
        return new()
        {
            Codigo = atividade.Codigo,
            Descricao = atividade.Descricao,
            EquipamentoModeloId = atividade.EquipamentoModeloId,
            AtividadeRotinaValidacaoId = atividade.AtividadeRotinaValidacaoId,
            AtividadeAnteriorId = atividade.AtividadeAnteriorId,
            SetorTrabalhoId = atividade.SetorTrabalhoId,
            PermiteRejeitar = atividade.PermiteRejeitar,
            TipoAtribuicaoAutomatica = atividade.TipoAtribuicaoAutomatica,
            EvitarConflitoEndereco = atividade.EvitarConflitoEndereco,
        };
    }
}
