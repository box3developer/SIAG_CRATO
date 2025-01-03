using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.StatusLeitor;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.StatusLeitor;

public class StatusLeitorBLL
{
    public static async Task<List<StatusLeitorDTO>> GetStatusByEquipamentoAsync(int idLeitor, int idEquipamento)
    {
        var sql = $"{StatusLeitorQuery.SELECT} WHERE equipamento = @idEquipamento AND leitor = @idLeitor";

        using var conexao = new SqlConnection(Global.Conexao);
        var statusLeitor = await conexao.QueryAsync<StatusLeitorModel>(sql, new { idEquipamento, idLeitor });

        return statusLeitor.Select(ConvertToDTO).ToList();
    }

    public static async Task<int> CreateStatusLeitorAsync(StatusLeitorDTO statusLeitor)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var id = await conexao.ExecuteAsync(StatusLeitorQuery.INSERT, new
        {
            equipamento = statusLeitor.Equipamento,
            leitor = statusLeitor.Leitor,
            configurado = statusLeitor.Configurado,
            conectado = statusLeitor.Conectado,
            executando = statusLeitor.Executando,
            dataStatus = statusLeitor.DtStatus,
        });

        return id;
    }

    public static async Task<int> UpdateStatusLeitorAsync(StatusLeitorDTO statusLeitor)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var id = await conexao.ExecuteAsync(StatusLeitorQuery.UPDATE, new
        {
            idEquipamento = statusLeitor.Equipamento,
            idLeitor = statusLeitor.Leitor,
            configurado = statusLeitor.Configurado,
            conectado = statusLeitor.Conectado,
            executando = statusLeitor.Executando,
            dataStatus = statusLeitor.DtStatus,
        });

        return id;
    }

    private static StatusLeitorDTO ConvertToDTO(StatusLeitorModel status)
    {
        return new()
        {
            Equipamento = status.Equipamento,
            Leitor = status.Leitor,
            Configurado = status.Configurado,
            Conectado = status.Conectado,
            Executando = status.Executando,
            DtStatus = status.DtStatus,
        };
    }
}