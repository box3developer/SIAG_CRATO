using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Setor;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Setor;

public class SetorBLL
{
    public static async Task<SetorDTO?> GetById(int id)
    {
        var sql = $"{SetorQuery.SELECT} WHERE id_setortrabalho = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var setor = await conexao.QueryFirstOrDefaultAsync<SetorModel>(sql, new { id });

        if (setor == null)
        {
            return null;
        }

        return ConvertToDTO(setor);
    }

    public static async Task<List<SetorSelectDTO>> GetListSelectsAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var setores = await conexao.QueryAsync<SetorModel>(SetorQuery.SELECT);

        return setores.Select(x => new SetorSelectDTO() { Id = x.IdSetorTrabalho, Descricao = x.NmSetorTrabalho }).ToList();
    }

    private static SetorDTO ConvertToDTO(SetorModel setor)
    {
        return new()
        {
            IdSetorTrabalho = setor.IdSetorTrabalho,
            IdDeposito = setor.IdDeposito,
            NmSetorTrabalho = setor.NmSetorTrabalho,
        };
    }
}
