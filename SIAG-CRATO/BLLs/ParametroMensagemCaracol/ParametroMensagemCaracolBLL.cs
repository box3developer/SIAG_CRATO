using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.ParametroMensagemCaracol;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.ParametroMensagemCaracol;

public class ParametroMensagemCaracolBLL
{
    public static async Task<ParametroMensagemCaracolModel> GetByDescricao(string descricao)
    {
        var sql = $"{ParametroMensagemCaracolQuery.SELECT} WHERE descricao = @descricao";

        using var conexao = new SqlConnection(Global.Conexao);
        var parametro = await conexao.QueryFirstOrDefaultAsync<ParametroMensagemCaracolModel>(sql, new { descricao });

        return parametro ?? new ParametroMensagemCaracolModel { Descricao = descricao, Mensagem = descricao, Cor = "#dc2626" };
    }

    private static ParametroMensagemCaracolDTO ConvertToDTO(ParametroMensagemCaracolModel parametro)
    {
        return new()
        {
            IdParametroMensagemCaracol = parametro.IdParametroMensagemCaracol,
            Descricao = parametro.Descricao,
            Mensagem = parametro.Mensagem,
            Cor = parametro.Cor,
        };
    }
}
