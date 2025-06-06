﻿using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.EquipamentoEndereco;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.EquipamentoEndereco;

public class EquipamentoEnderecoBLL
{
    public static async Task<List<EquipamentoEnderecoDTO>> GetList()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoEnderecoModel>(EquipamentoEnderecoPrioridadeQuery.SELECT);

        return equipamentos.Select(ConvertToDTO).ToList();
    }

    public static async Task<List<EquipamentoEnderecoDTO>> GetByEquipamentoAsync(int idEquipamento)
    {
        var sql = $"{EquipamentoQuery.SELECT} WHERE id_equipamento = @idEquipamento";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoEnderecoModel>(sql, new { idEquipamento });

        return equipamentos.Select(ConvertToDTO).ToList();
    }

    public static async Task<List<EquipamentoEnderecoDTO>> GetOutrosEquipamentosAtivosAsync(int idEquipamento, int idEquipamentoModelo, int idSetorTrabalho, DateTime dataMovimentacaoAtiva, DateTime dataMovimentacaoInativa)
    {
        var sql = $@"{EquipamentoQuery.SELECT} 
                     WHERE id_equipamentomodelo = @idEquipamentoModelo
                           AND id_setortrabalho = @idSetorTrabalho
                           AND fg_status = @status
				           AND id_equipamento <> @idEquipamento
				           AND id_endereco IS NOT NULL
				           AND ((id_operador IS NOT NULL) AND (dt_ultimaleitura > @dataMovimentacaoAtiva)) OR
						       ((id_operador IS NULL) AND (dt_ultimaleitura > @dataMovimentacaoInativa))";

        using var conexao = new SqlConnection(Global.Conexao);
        var equipamentos = await conexao.QueryAsync<EquipamentoEnderecoModel>(sql, new
        {
            idEquipamentoModelo,
            idSetorTrabalho,
            status = StatusEquipamento.Ativo,
            idEquipamento,
            dataMovimentacaoAtiva,
            dataMovimentacaoInativa
        });

        return equipamentos.Select(ConvertToDTO).ToList();
    }

    private static EquipamentoEnderecoDTO ConvertToDTO(EquipamentoEnderecoModel endereco)
    {
        return new()
        {
            IdEquipamentoEndereco = endereco.IdEquipamentoEndereco,
            IdEquipamento = endereco.IdEquipamento,
            IdEndereco = endereco.IdEndereco,
        };
    }
}
