using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.StatusLeitor;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.StatusLeitor;

public class StatusLeitorBLL
{
    public static async Task<List<StatusLeitorModel>> GetStatusByEquipamentoAsync(int idLeitor, int idEquipamento)
    {
        var sql = $"{StatusLeitorQuery.SELECT} WHERE equipamento = @idEquipamento AND leitor = @idLeitor";

        using var conexao = new SqlConnection(Global.Conexao);
        var statusLeitor = await conexao.QueryAsync<StatusLeitorModel>(sql, new { idEquipamento, idLeitor });

        return statusLeitor.ToList();
    }

    public static async Task<int> CreateStatusLeitorAsync(StatusLeitorModel statusLeitor)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var id = await conexao.ExecuteAsync(StatusLeitorQuery.INSERT, new
        {
            equipamento = statusLeitor.Equipamento,
            leitor = statusLeitor.Leitor,
            configurado = statusLeitor.Configurado,
            conectado = statusLeitor.Conectado,
            executando = statusLeitor.Executando,
            dataStatus = statusLeitor.DataStatus,
        });

        return id;
    }

    public static async Task<int> UpdateStatusLeitorAsync(StatusLeitorModel statusLeitor)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var id = await conexao.ExecuteAsync(StatusLeitorQuery.UPDATE, new
        {
            equipamento = statusLeitor.Equipamento,
            leitor = statusLeitor.Leitor,
            configurado = statusLeitor.Configurado,
            conectado = statusLeitor.Conectado,
            executando = statusLeitor.Executando,
            dataStatus = statusLeitor.DataStatus,
        });

        return id;
    }

    private StatusLeitorDTO ConvertToDTO(StatusLeitorModel status)
    {
        return new()
        {
            Equipamento = status.Equipamento,
            Leitor = status.Leitor,
            Configurado = status.Configurado,
            Conectado = status.Conectado,
            Executando = status.Executando,
            DataStatus = status.DataStatus,
        };
    }
}