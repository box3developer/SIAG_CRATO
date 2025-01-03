using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.AtividadeRotina;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.AtividadeRotina;

public class AtividadeRotinaBLL
{

    public static async Task<AtividadeRotinaModel?> GetById(int id)
    {
        var sql = $"{AtividadeRotinaQuery.SELECT} WHERE id_atividadeRotina = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var atividade = await conexao.QueryFirstOrDefaultAsync<AtividadeRotinaModel>(sql, new { id });

        return atividade;
    }

    private static AtividadeRotinaDTO ConvertToDTO(AtividadeRotinaModel atividade)
    {
        return new()
        {
            IdAtividadeRotina = atividade.IdAtividadeRotina,
            NmAtividadeRotina = atividade.NmAtividadeRotina,
            NmProcedure = atividade.NmProcedure,
            FgTipo = atividade.FgTipo,
        };
    }
}
