using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.CaixaLeitura;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.CaixaLeitura;

public class CaixaLeituraBLL
{
    public static async Task<CaixaLeituraDTO?> GetUltimaCaixaLida(string idCaixa)
    {
        var sql = $@"{CaixaLeituraQuery.SELECT}
                     WHERE  id_caixa = @idCaixa AND fg_tipo = 19
                     ORDER BY id_caixaleitura DESC";

        using var conexao = new SqlConnection(Global.Conexao);
        var caixa = await conexao.QueryFirstOrDefaultAsync<CaixaLeituraModel>(sql, new { idCaixa });

        if (caixa == null)
        {
            return null;
        }

        return ConvertToDTO(caixa);
    }

    public static async Task<bool> CreateCaixaLeitura(CaixaLeituraModel caixaLeitura)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var result = await conexao.ExecuteAsync(CaixaLeituraQuery.INSERT, new
        {
            idCaixa = caixaLeitura.IdCaixa,
            dtLeitura = caixaLeitura.DtLeitura,
            fgTipo = caixaLeitura.FgTipo,
            fgStatus = caixaLeitura.FgStatus,
            idOperador = caixaLeitura.IdOperador,
            idEquipamento = caixaLeitura.IdEquipamento,
            idPallet = caixaLeitura.IdPallet,
            idAreaArmazenagem = caixaLeitura.IdAreaArmazenagem,
            idEndereco = caixaLeitura.IdEndereco,
            fgCancelado = caixaLeitura.FgCancelado,
            idOrdem = caixaLeitura.IdOrdem
        });

        return result > 0;
    }

    private static CaixaLeituraDTO ConvertToDTO(CaixaLeituraModel caixaLeitura)
    {
        return new()
        {
            IdCaixaLeitura = caixaLeitura.IdCaixaLeitura,
            IdCaixa = caixaLeitura.IdCaixa,
            DtLeitura = caixaLeitura.DtLeitura,
            FgTipo = caixaLeitura.FgTipo,
            FgStatus = caixaLeitura.FgStatus,
            IdOperador = caixaLeitura.IdOperador,
            IdEquipamento = caixaLeitura.IdEquipamento,
            IdPallet = caixaLeitura.IdPallet,
            IdAreaArmazenagem = caixaLeitura.IdAreaArmazenagem,
            IdEndereco = caixaLeitura.IdEndereco,
            FgCancelado = caixaLeitura.FgCancelado,
            IdOrdem = caixaLeitura.IdOrdem,
        };
    }
}
