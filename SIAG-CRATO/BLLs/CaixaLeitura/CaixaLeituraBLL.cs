﻿using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.CaixaLeitura;
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

    private static CaixaLeituraDTO ConvertToDTO(CaixaLeituraModel caixaLeitura)
    {
        return new()
        {
            CaixaLeituraId = caixaLeitura.IdCaixaLeitura,
            CaixaId = caixaLeitura.IdCaixa,
            DataLeitura = caixaLeitura.DtLeitura,
            Tipo = caixaLeitura.FgTipo,
            Status = caixaLeitura.FgStatus,
            OperadorId = caixaLeitura.IdOperador,
            EquipamentoId = caixaLeitura.IdEquipamento,
            PalletId = caixaLeitura.IdPallet,
            AreaArmazenagemId = caixaLeitura.IdAreaArmazenagem,
            EnderecoId = caixaLeitura.IdEndereco,
            Cancelado = caixaLeitura.FgCancelado,
            Ordem = caixaLeitura.IdOrdem,
        };
    }
}
