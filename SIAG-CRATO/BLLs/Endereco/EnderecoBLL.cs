using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.DTOs.Endereco;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Endereco
{
    public class EnderecoBLL
    {
        public static async Task<List<EnderecoDTO>> GetListAsync()
        {
            using var conexao = new SqlConnection(Global.Conexao);
            var enderecos = await conexao.QueryAsync<EnderecoModel>(EnderecoQuery.SELECT);

            return enderecos.Select(ConvertToDTO).ToList();
        }

        public static async Task<EnderecoDTO?> GetByIdAsync(int id)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_endereco = @id";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryFirstOrDefaultAsync<EnderecoModel>(sql, new { id });

            if (endereco == null)
            {
                return null;
            }

            return ConvertToDTO(endereco);
        }

        public static async Task<List<EnderecoDTO>> GetBySetorStatus(int id_setortrabalho, int fg_status)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_setortrabalho = @id_setortrabalho and endereco.fg_status = @fg_status";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryAsync<EnderecoModel>(sql, new { id_setortrabalho, fg_status });

            return endereco.Select(ConvertToDTO).ToList();
        }

        public static async Task<List<EnderecoDTO>> GetBySetor(int id_setortrabalho)
        {
            var sql = $@"{EnderecoQuery.SELECT} WHERE id_setortrabalho = @id_setortrabalho";

            using var conexao = new SqlConnection(Global.Conexao);
            var endereco = await conexao.QueryAsync<EnderecoModel>(sql, new { id_setortrabalho });

            return endereco.Select(ConvertToDTO).ToList();
        }

        private static EnderecoDTO ConvertToDTO(EnderecoModel endereco)
        {
            return new()
            {
                IdEndereco = endereco.IdEndereco,
                IdRegiaoTrabalho = endereco.IdRegiaoTrabalho,
                IdSetorTrabalho = endereco.IdSetorTrabalho,
                IdTipoEndereco = endereco.IdTipoEndereco,
                NmEndereco = endereco.NmEndereco,
                QtEstoqueMinimo = endereco.QtEstoqueMinimo,
                QtEstoqueMaximo = endereco.QtEstoqueMaximo,
                FgStatus = endereco.FgStatus,
                TpPreenchimento = endereco.TpPreenchimento,
            };
        }
    }
}
