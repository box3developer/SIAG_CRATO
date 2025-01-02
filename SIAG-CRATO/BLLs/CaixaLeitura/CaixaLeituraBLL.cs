using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.CaixaLeitura;

public class CaixaLeituraBLL
{
    public static async Task<CaixaLeituraModel?> GetUltimaCaixaLida(string idCaixa)
    {
        var sql = $@"{CaixaLeituraQuery.SELECT}
                     WHERE  id_caixa = @idCaixa AND fg_tipo = 19
                     ORDER BY id_caixaleitura DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var caixa = await conexao.QueryFirstOrDefaultAsync<CaixaLeituraModel>(sql, new { idCaixa });

        return caixa;
    }

    public static async Task<bool> CreateCaixaLeitura(CaixaLeituraModel caixaLeitura)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var result = await conexao.ExecuteAsync(CaixaLeituraQuery.INSERT, new
        {
            id_caixa = caixaLeitura.Id_caixa,
            dt_leitura = caixaLeitura.Dt_leitura,
            fg_tipo = caixaLeitura.Fg_tipo,
            fg_status = caixaLeitura.Fg_status,
            id_operador = caixaLeitura.Id_operador,
            id_equipamento = caixaLeitura.Id_equipamento,
            id_pallet = caixaLeitura.Id_pallet,
            id_areaarmazenagem = caixaLeitura.Id_areaarmazenagem,
            id_endereco = caixaLeitura.Id_endereco,
            fg_cancelado = caixaLeitura.Fg_cancelado,
            id_ordem = caixaLeitura.Id_ordem
        });

        return result > 0;
    }
}
