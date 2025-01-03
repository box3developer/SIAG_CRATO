using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SIAG_CRATO.DTOs.AtividadeRejeicao;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.AtividadeRejeicao;

public class AtividadeRejeicaoBLL
{
    public static async Task<List<AtividadeRejeicaoModel>> GetListAsync(AtividadeRejeicaoModel? atividadeRejeicao)
    {
        var sql = $"{AtividadeRejeicaoQuery.SELECT} WHERE 1 = 1";
        object? filtro = null;

        if (atividadeRejeicao != null)
        {
            if (atividadeRejeicao.IdAtividadeRejeicao != 0)
            {
                sql += " AND id_atividaderejeicao = @codigo ";
            }
            if (!atividadeRejeicao.NmAtividadeRejeicao.IsNullOrEmpty())
            {
                sql += " AND nm_atividaderejeicao like @nome ";
            }
            if (!atividadeRejeicao.NmEmailAlerta.IsNullOrEmpty())
            {
                sql += " AND nm_email_alerta = @email ";
            }

            filtro = new
            {
                codigo = atividadeRejeicao.IdAtividadeRejeicao,
                nome = atividadeRejeicao.NmAtividadeRejeicao,
                email = atividadeRejeicao.NmEmailAlerta,
            };
        }

        using var conexao = new SqlConnection(Global.Conexao);
        var atividades = await conexao.QueryAsync<AtividadeRejeicaoModel>(sql, filtro);

        return atividades.ToList();
    }

    private static AtividadeRejeicaoDTO ConvertToDTO(AtividadeRejeicaoModel atividadeRejeicao)
    {
        return new()
        {
            IdAtividadeRejeicao = atividadeRejeicao.IdAtividadeRejeicao,
            NmAtividadeRejeicao = atividadeRejeicao.NmAtividadeRejeicao,
            NmEmailAlerta = atividadeRejeicao.NmEmailAlerta,
        };
    }
}
