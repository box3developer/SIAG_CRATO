using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Log;

public class LogBLL
{
    public static async Task<bool> CreateLogCaracol(LogModel log)
    {
        var sql = @"INSERT INTO logCaracol (id_requisicao, nm_identificador, id_caixa, data, mensagem, metodo, id_operador, tipo)
                    VALUES (@idRequisicao, @nomeIdentificador, @idCaixa, @data, @mensagem, @metodo, @idOperador, @tipo)";

        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(sql, new
        {
            idRequisicao = log.IdRequisicao,
            nomeIdentificador = log.NomeIdentificador,
            idCaixa = log.IdCaixa,
            data = DateTime.Now,
            mensagem = log.Mensagem,
            metodo = log.Metodo,
            idOperador = log.IdOperador,
            tipo = log.Tipo,
        });

        return true;
    }

    public static async Task<bool> CreateLogSIAG(string mensagem)
    {
        var sql = @"INSERT INTO logsiag (mensagem) VALUES (@mensagem)";

        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(sql, new { mensagem });

        return true;
    }
}
