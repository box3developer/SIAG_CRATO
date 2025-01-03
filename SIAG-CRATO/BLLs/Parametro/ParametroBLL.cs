using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Parametro;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Parametro;

public class ParametroBLL
{
    public static async Task<ParametroDTO?> GetParametroByParametro(string parametro)
    {
        var sql = $@"{ParametroQuery.SELECT} WHERE nm_parametro = @NmParametro";

        using var conexao = new SqlConnection(Global.Conexao);

        var equipamento = await conexao.QueryFirstOrDefaultAsync<ParametroModel>(sql, new { parametro });

        if (equipamento == null)
        {
            return null;
        }

        return ConvertToDTO(equipamento);
    }

    public static async Task<ParametroDTO?> GetParametroByTipo(string tipo)
    {
        var sql = $@"{ParametroQuery.SELECT} WHERE nm_tipo = @tipo";

        using var conexao = new SqlConnection(Global.Conexao);

        var equipamento = await conexao.QueryFirstOrDefaultAsync<ParametroModel>(sql, new { tipo });

        if (equipamento == null)
        {
            return null;
        }

        return ConvertToDTO(equipamento);
    }

    private static ParametroDTO ConvertToDTO(ParametroModel parametro)
    {
        return new()
        {
            IdParametro = parametro.IdParametro,
            NmParametro = parametro.NmParametro,
            NmValor = parametro.NmValor,
            FgTipoParametro = parametro.FgTipoParametro,
            NmUnidadeMedida = parametro.NmUnidadeMedida,
            NmTipo = parametro.NmTipo,
            FgVisivel = parametro.FgVisivel,
            FgAtivo = parametro.FgAtivo,
        };
    }
}
