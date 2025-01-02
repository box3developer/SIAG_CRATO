using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Endereco;
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

        public static async Task<List<EnderecoModel>> GetBySetorStatus(int id_setortrabalho, int fg_status)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_setortrabalho = @id_setortrabalho and endereco.fg_status = @fg_status";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryAsync<EnderecoModel>(sql, new { id_setortrabalho, fg_status });

            return endereco.ToList();
        }

        public static async Task<List<EnderecoModel>> GetBySetor(int id_setortrabalho)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_setortrabalho = @id_setortrabalho";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryAsync<EnderecoModel>(sql, new { id_setortrabalho });

            return endereco.ToList();
        }

        private static EnderecoDTO ConvertToDTO(EnderecoModel endereco)
        {
            return new()
            {
                EnderecoId = endereco.Id_endereco,
                RegiaoTrabalhoId = endereco.Id_regiaotrabalho,
                SetorId = endereco.Id_setortrabalho,
                TipoEnderecoId = endereco.Id_tipoendereco,
                NomeEndereco = endereco.Nm_endereco,
                EstoqueMinimo = endereco.Qt_estoqueminimo,
                EstoqueMaximo = endereco.Qt_estoquemaximo,
                Status = endereco.Fg_status,
                TipoPreenchimento = endereco.Tp_preenchimento,
            };
        }
    }
}
