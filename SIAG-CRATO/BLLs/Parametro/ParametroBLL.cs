using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Parametro;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Parametro;

public class ParametroBLL
{
    public static async Task<ParametroDTO?> GetParametroByParametro(string nomeParametro)
    {
        var sql = $@"{ParametroQuery.SELECT} WHERE nm_parametro = @nmParametro";

        using var conexao = new SqlConnection(Global.Conexao);

        var parametro = await conexao.QueryFirstOrDefaultAsync<ParametroModel>(sql, new { nomeParametro });

        if (parametro == null)
        {
            return null;
        }

        return ConvertToDTO(parametro);
    }

    public static async Task<double> GetParametroValorByParametro(string nomeParametro)
    {
        var sql = $@"{ParametroQuery.SELECT} WHERE nm_parametro = @nomeParametro";

        using var conexao = new SqlConnection(Global.Conexao);

        var parametro = await conexao.QueryFirstOrDefaultAsync<ParametroModel>(sql, new { nomeParametro });

        if (parametro == null)
        {
            return 0.0;
        }

        _ = double.TryParse(parametro.NmValor?.Replace(',', '.'), out double valor);

        return valor;
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
