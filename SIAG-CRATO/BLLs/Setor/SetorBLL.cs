using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Setor;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Setor;

public class SetorBLL
{
    public static async Task<SetorModel?> GetById(int id)
    {
        var sql = $"{SetorQuery.SELECT} WHERE id_setortrabalho = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var setor = await conexao.QueryFirstOrDefaultAsync<SetorModel>(sql, new { id });

        return setor;
    }

    public static async Task<List<SetorSelectDTO>> GetListSelectsAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var setores = await conexao.QueryAsync<SetorModel>(SetorQuery.SELECT);

        return setores.Select(x => new SetorSelectDTO() { Id = x.Codigo, Descricao = x.Descricao }).ToList();
    }
}
