using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.EquipamentoManutencao
{
    public class EquipamentoManutencaoBLL
    {
        public static async Task<List<EquipamentoManutencaoModel>> GetListAsync()
        {
            using var conexao = new SqlConnection(Global.Conexao);
            var equipamentos = await conexao.QueryAsync<EquipamentoManutencaoModel>(EquipamentoManutencaoQuery.SELECT);

            return equipamentos.ToList();
        }

        public static async Task<EquipamentoManutencaoModel?> GetByIdAsync(int id)
        {
            var sql = $@"{EquipamentoManutencaoQuery.SELECT} WHERE id_equipamento_manutencao = @id";

            using var conexao = new SqlConnection(Global.Conexao);
            var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoManutencaoModel>(sql, new { id });

            return equipamento;
        }

        public static async Task<bool> SetDtFimByEquipamento(int id_equipamento)
        {
            using var conexao = new SqlConnection(Global.Conexao);
            var result = await conexao.ExecuteAsync(EquipamentoManutencaoQuery.UPDATE_DTFIM_BY_EQUIPAMENTO, new { id_equipamento });

            return result > 0;
        }

    }
}
