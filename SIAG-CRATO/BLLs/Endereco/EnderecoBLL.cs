using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Endereco
{
    public class EnderecoBLL
    {
        public static async Task<List<EnderecoModel>> GetListAsync()
        {
            using var conexao = new SqlConnection(Global.Conexao);
            var enderecoList = await conexao.QueryAsync<EnderecoModel>(EnderecoQuery.SELECT);

            return enderecoList.ToList();
        }

        public static async Task<EnderecoModel?> GetByIdAsync(int id)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_endereco = @id";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryFirstOrDefaultAsync<EnderecoModel>(sql, new { id });

            return endereco;
        }
    }
}
