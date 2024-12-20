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

    public static async Task<List<AtividadeModel>> GeAtividadesByEquipModeloSetor(int id_equipamentomodelo, int id_setortrabalho)
    {
        if(id_equipamentomodelo == 0 || id_setortrabalho == 0)
        {
            return [];
        }

        var sql = $@"{AtividadeQuery.SELECT} WHERE id_equipamentomodelo = @id_equipamentomodelo and id_setortrabalho = @id_setortrabalho";
        using var conexao = new SqlConnection(Global.Conexao);

        var list = await conexao.QueryAsync<AtividadeModel>(sql, new { id_equipamentomodelo, id_setortrabalho });

        return list.ToList();

    }
}
