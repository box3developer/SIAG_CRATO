using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Atividade;
using SIAG_CRATO.BLLs.Chamada;
using SIAG_CRATO.BLLs.Endereco;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Equipamento;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Equipamento;

public class EquipamentoBLL
{
    public static async Task<List<EquipamentoModel>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(EquipamentoQuery.SELECT);

        return equipamentos.ToList();
    }

    public static async Task<List<EquipamentoModel>> GetAllCaracois()
    {
        var sql = $"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = 1";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(sql);

        return equipamentos.ToList();
    }

    public static async Task<EquipamentoModel?> GetByIdAsync(int id)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamento = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { id });

        return equipamento;
    }

    public static async Task<List<EquipamentoModel>> GetActiveEquipByModel(int id_equipamentomodelo)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = @id_equipamentomodelo and id_operador is not null";

        using var conexao = new SqlConnection(Global.Conexao);

        var list = await conexao.QueryAsync<EquipamentoModel>(sql, new { id_equipamentomodelo });

        return list.ToList();
    }

    public static async Task<EquipamentoModel?> GetByidentificadorAsync(string nm_identificador)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE nm_identificador = @nm_identificador";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { nm_identificador });

        return equipamento;
    }

    public static async Task<EquipamentoModel?> GetByCaracolAsync(string identificadorCaracol)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE nm_identificador = @identificadorCaracol";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { identificadorCaracol });

        return equipamento;
    }

    public static async Task<EquipamentoModel?> GetByOperadorAsync(string cracha)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_operador = @cracha";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { cracha });

        return equipamento;
    }

    public static async Task<EquipamentoModel?> GetByCaixaPendenteAsync(string idCaixa)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE cd_leitura_pendente = @cdCaixaPendente";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { idCaixa });

        return equipamento;
    }

    public static async Task<List<EquipamentoModel>> GetByModeloAsync(int idModelo)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = @idModelo";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(sql, new { idModelo });

        return equipamentos.ToList();
    }

    public static async Task<bool> SetCaixaPendente(string? idCaixa, string idEquipamento)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_PENDENTE, new { idCaixa, idEquipamento });

        return id > 0;
    }

    public static async Task<bool> SetEquipamentoOperador(int id_operador, int id_equipamento)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var result = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_EQUIPAMENTO_OPERADOR, new { id_operador, id_equipamento });

        return result > 0;
    }

    public static async Task<bool> UpdateLeitura(int idEquipamento)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_LEITURA, new { idEquipamento });

        return id > 0;
    }

    public static async Task<int> UpdateEquipamento(int id_equipamento, int? id_endereco)
    {
        if (id_endereco.HasValue && id_endereco > 0)
        {
            var sqlEndereco = $@"{EquipamentoQuery.UPDATE_ENDERECO} where id_equipamento = @id_equipamento";
            using var conexaoEndereco = new SqlConnection(Global.Conexao);
            var returnEndereco = await conexaoEndereco.ExecuteAsync(sqlEndereco, new { id_equipamento, id_endereco });

            return returnEndereco;
        }

        var sqlEquipamento = $@"{EquipamentoQuery.UPDATE_DATE} where id_equipamento = @id_equipamento";
        using var conexao = new SqlConnection(Global.Conexao);
        var returnEquipamento = await conexao.ExecuteAsync(sqlEquipamento, new { id_equipamento });

        return returnEquipamento;

    }

    public static async Task<bool> AlocacaoAutomaticaBilateral()
    {

        var atividadeList = await AtividadeBLL.GetByEquipModeloSetor(Constants.EQUIPAMENTO_EMPILHADEIRABILATERAL, Constants.SETOR_PORTAPALLET);

        var chamadalist = await ChamadaBLL.GetByStatus(Constants.STATUS_CHAMADAREJEITADA);

        var atividadesId = atividadeList.Select(a => a.Codigo).ToList();

        var areaChamadas = chamadalist.Where(c => atividadesId.Contains(c.AtividadeId));

        var bilateraisAtivas = await GetActiveEquipByModel(Constants.EQUIPAMENTO_EMPILHADEIRABILATERAL);

        using var conexao = new SqlConnection(Global.Conexao);

        var enderecoList = EnderecoBLL.GetBySetorStatus(Constants.SETOR_PORTAPALLET, Constants.ENDERECO_ATIVO);

        return true;
    }

    private static EquipamentoDTO ConvertToDTO(EquipamentoModel equipamento)
    {
        return new()
        {
            Codigo = equipamento.Codigo,
            ModeloId = equipamento.ModeloId,
            SetorId = equipamento.SetorId,
            OperadorId = equipamento.OperadorId,
            Descricao = equipamento.Descricao,
            DescricaoAbreviada = equipamento.DescricaoAbreviada,
            Identificador = equipamento.Identificador,
            Status = equipamento.Status,
            DataInclusao = equipamento.DataInclusao,
            DataManutencao = equipamento.DataManutencao,
            DataUltimaLeitura = equipamento.DataUltimaLeitura,
            EnderecoTrabalho = equipamento.EnderecoTrabalho,
            IP = equipamento.IP,
            StatusTrocaCaracol = equipamento.StatusTrocaCaracol,
            LeituraPendete = equipamento.LeituraPendete,
            UltimaLeitura = equipamento.UltimaLeitura,
            Observacao = equipamento.Observacao,
        };
    }
}

