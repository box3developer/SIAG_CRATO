using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Parametro;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Parametro
{
    public class ParametroBLL
    {
        public static async Task<ParametroModel?> GetParametroByParametro(string parametro)
        {
            var sql = $@"{ParametroQuery.SELECT} WHERE nm_parametro = @NmParametro";

            using var conexao = new SqlConnection(Global.Conexao);

            var equipamento = await conexao.QueryFirstOrDefaultAsync<ParametroModel>(sql, new { parametro });

            return equipamento;
        }

        public static async Task<ParametroModel?> GetParametroByTipo(string tipo)
        {
            var sql = $@"{ParametroQuery.SELECT} WHERE nm_tipo = @tipo";

            using var conexao = new SqlConnection(Global.Conexao);

            var equipamento = await conexao.QueryFirstOrDefaultAsync<ParametroModel>(sql, new { tipo });

            return equipamento;
        }

        private static ParametroDTO ConvertToDTO(ParametroModel parametro)
        {
            return new()
            {
                Id = parametro.Id,
                Parametro = parametro.Parametro,
                Valor = parametro.Valor,
                TipoParametro = parametro.TipoParametro,
                Unidade = parametro.Unidade,
                Tipo = parametro.Tipo,
                Visivel = parametro.Visivel,
                Ativo = parametro.Ativo,
            };
        }
    }
}
