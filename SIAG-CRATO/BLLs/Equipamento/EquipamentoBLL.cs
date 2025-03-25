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
    public static async Task<List<EquipamentoDTO>> GetListAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(EquipamentoQuery.SELECT);

        return equipamentos.Select(ConvertToDTO).ToList();
    }

    public static async Task<List<EquipamentoDTO>> GetAllCaracois()
    {
        var sql = $"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = 1";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(sql);

        return equipamentos.Select(ConvertToDTO).ToList();
    }

    public static async Task<EquipamentoDTO?> GetByIdAsync(int id)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamento = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { id });

        if (equipamento == null)
        {
            return null;
        }

        return ConvertToDTO(equipamento);
    }

    public static async Task<List<EquipamentoDTO>> GetActiveEquipByModel(int id_equipamentomodelo)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = @id_equipamentomodelo and id_operador is not null";

        using var conexao = new SqlConnection(Global.Conexao);

        var lista = await conexao.QueryAsync<EquipamentoModel>(sql, new { id_equipamentomodelo });

        return lista.Select(ConvertToDTO).ToList();
    }

    public static async Task<EquipamentoDTO?> GetByidentificadorAsync(string nm_identificador)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE nm_identificador = @nm_identificador";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { nm_identificador });

        if (equipamento == null)
        {
            return null;
        }

        return ConvertToDTO(equipamento);
    }

    public static async Task<EquipamentoDTO?> GetByCaracolAsync(string identificadorCaracol)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE nm_identificador = @identificadorCaracol";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { identificadorCaracol });

        if (equipamento == null)
        {
            return null;
        }

        return ConvertToDTO(equipamento);
    }

    public static async Task<EquipamentoDTO?> GetByOperadorAsync(string cracha)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_operador = @cracha";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { cracha });

        if (equipamento == null)
        {
            return null;
        }

        return ConvertToDTO(equipamento);
    }

    public static async Task<EquipamentoDTO?> GetByCaixaPendenteAsync(string idCaixa)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE cd_leitura_pendente = @cdCaixaPendente";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamento = await conexao.QueryFirstOrDefaultAsync<EquipamentoModel>(sql, new { idCaixa });

        if (equipamento == null)
        {
            return null;
        }

        return ConvertToDTO(equipamento);
    }

    public static async Task<List<EquipamentoDTO>> GetByModeloAsync(int idModelo)
    {
        var sql = $@"{EquipamentoQuery.SELECT} WHERE id_equipamentomodelo = @idModelo";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoModel>(sql, new { idModelo });

        return equipamentos.Select(ConvertToDTO).ToList();
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

    public static async Task<bool> NovaLeitura(int id_equipamento, string id_caixa)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_NOVA_LEITURA, new { id_equipamento, id_caixa, dt_ultimaleitura = DateTime.Now });

        return id > 0;
    }

    public static async Task<int> UpdateEnderecoEquipamento(int id_equipamento, int? id_endereco)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        if (id_endereco.HasValue && id_endereco > 0)
        {
            var returnEndereco = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_ENDERECO, new { id_equipamento, id_endereco });

            return returnEndereco;
        }

        var returnEquipamento = await conexao.ExecuteAsync(EquipamentoQuery.UPDATE_DATE, new { id_equipamento });

        return returnEquipamento;
    }

    public static async Task<bool> UpdateEquipamento(EquipamentoDTO equipamento)
    {
        using var conexaoEndereco = new SqlConnection(Global.Conexao);
        var returnEndereco = await conexaoEndereco.ExecuteAsync(EquipamentoQuery.UPDATE, new
        {
            setor = equipamento.IdSetorTrabalho,
            descricao = equipamento.NmEquipamento,
            modelo = equipamento.IdEquipamentoModelo,
            status = (int)equipamento.FgStatus,
            dataInclusao = equipamento.DtInclusao,
            dataManutencao = equipamento.DtManutencao,
            identificador = equipamento.NmIdentificador,
            ip = equipamento.NmIP,
            descricaoAbreviada = equipamento.NmAbreviadoEquipamento,
            idEquipamento = equipamento.IdEquipamento,
        });

        return returnEndereco > 0;
    }

    public static async Task<bool> AlocacaoAutomaticaBilateral()
    {

        var atividadeList = await AtividadeBLL.GetByEquipModeloSetor(Constants.EQUIPAMENTO_EMPILHADEIRABILATERAL, Constants.SETOR_PORTAPALLET);

        var chamadalist = await ChamadaBLL.GetByStatus(Constants.STATUS_CHAMADAREJEITADA);

        var atividadesId = atividadeList.Select(a => a.IdAtividade).ToList();

        var areaChamadas = chamadalist.Where(c => atividadesId.Contains(c.IdAtividade));

        var bilateraisAtivas = await GetActiveEquipByModel(Constants.EQUIPAMENTO_EMPILHADEIRABILATERAL);

        using var conexao = new SqlConnection(Global.Conexao);

        var enderecoList = EnderecoBLL.GetBySetorStatus(Constants.SETOR_PORTAPALLET, Constants.ENDERECO_ATIVO);

        return true;
    }

    private static EquipamentoDTO ConvertToDTO(EquipamentoModel equipamento)
    {
        return new()
        {
            IdEquipamento = equipamento.IdEquipamento,
            IdEquipamentoModelo = equipamento.IdEquipamentoModelo,
            IdSetorTrabalho = equipamento.IdSetorTrabalho,
            IdOperador = equipamento.IdOperador,
            NmEquipamento = equipamento.NmEquipamento,
            NmAbreviadoEquipamento = equipamento.NmAbreviadoEquipamento,
            NmIdentificador = equipamento.NmIdentificador,
            FgStatus = equipamento.FgStatus,
            DtInclusao = equipamento.DtInclusao,
            DtManutencao = equipamento.DtManutencao,
            DtUltimaLeitura = equipamento.DtUltimaLeitura,
            IdEndereco = equipamento.IdEndereco,
            NmIP = equipamento.NmIP,
            FgStatusTrocaCaracol = equipamento.FgStatusTrocaCaracol,
            CdLeituraPendente = equipamento.CdLeituraPendente,
            CdUltimaLeitura = equipamento.CdUltimaLeitura,
            NmObservacao = equipamento.NmObservacao,
        };
    }
}

