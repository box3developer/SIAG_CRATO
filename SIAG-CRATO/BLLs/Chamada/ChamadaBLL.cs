using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Atividade;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.BLLs.EquipamentoEndereco;
using SIAG_CRATO.BLLs.EquipamentoEnderecoPrioridade;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Chamada;
using SIAG_CRATO.DTOs.EquipamentoEndereco;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Chamada;

public class ChamadaBLL
{
    public static async Task<ChamadaDTO?> GetByIdAsync(Guid id)
    {
        var sql = $"{ChamadaQuery.SELECT} WHERE id_chamada = @idChamada";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idChamada = id });

        if (chamada == null)
        {
            return null;
        }

        return ConvertToDTO(chamada);
    }

    public static async Task<ChamadaDTO?> GetChamadaAbertaAsync(int idPallet, long idAreaArmazenagem, int idAtividade)
    {
        var sql = $@"{ChamadaQuery.SELECT} 
                     WHERE id_palletorigem = @idPallet
                           AND fg_status < 4
                           AND id_areaarmazenagemorigem = @idAreaArmazenagem
                           AND id_atividade = @idAtividade";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idPallet, idAreaArmazenagem, idAtividade });

        if (chamada == null)
        {
            return null;
        }

        return ConvertToDTO(chamada);
    }

    public static async Task<List<ChamadaDisponivelDTO>> GetChamadaDisponiveisAsync(int idEquipamentoModelo, int idSetorTrabalho)
    {
        var sql = $@"{ChamadaQuery.SELECT_DISPONIVEL} 
                     WHERE chamada.fg_status = @status
	                       AND chamada.id_operador IS NULL
	                       AND chamada.id_equipamento IS NULL
	                       AND chamada.dt_finalizada IS NULL
	                       AND atividade.id_equipamentomodelo = @idEquipamentoModelo
	                       AND atividade.id_setortrabalho = @idSetorTrabalho";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamadas = await conexao.QueryAsync<ChamadaDisponivelDTO>(sql, new
        {
            status = StatusChamada.Andamento,
            idEquipamentoModelo,
            idSetorTrabalho
        });

        return chamadas.ToList();
    }

    public static async Task<ChamadaDTO?> GetChamadaAbertaByOperadorAsync(int idOperador, int idEquipamento)
    {
        var sql = $@"{ChamadaQuery.SELECT} 
                     WHERE (id_operador = @idOperador or id_equipamento = @idEquipamento)
                           AND fg_status > @status
                     ORDER BY fg_status desc, dt_chamada desc";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idOperador, idEquipamento, StatusChamada.Andamento });

        if (chamada == null)
        {
            return null;
        }

        return ConvertToDTO(chamada);
    }

    public static async Task<Guid> SetChamadaAsync(ChamadaInsertDTO chamada)
    {
        chamada.IdChamada = Guid.NewGuid();
        chamada.FgStatus = StatusChamada.Dependente;

        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(ChamadaQuery.INSERT, new
        {
            idChamada = chamada.IdChamada,
            idPalletOrigem = chamada.IdPalletOrigem,
            idPalletDestino = chamada.IdPalletDestino,
            idAreaArmazenagemOrigem = chamada.IdAreaArmazenagemOrigem,
            idAreaArmazenagemDestino = chamada.IdAreaArmazenagemDestino,
            idAtividade = chamada.IdAtividade,
            statusChamada = chamada.FgStatus,
            priorizar = chamada.FgPriorizar,
        });

        return chamada.IdChamada;
    }

    public static async Task<bool> SetStatusAsync(Guid idChamada, StatusChamada status)
    {
        string sql = @"UPDATE chamada SET fg_status = @status";

        switch (status)
        {
            case StatusChamada.Recebido:
                sql = $"{sql}, dt_recebida = GETDATE()";
                break;
            case StatusChamada.Andamento:
                sql = $"{sql}, dt_atendida = GETDATE()";
                break;
        }

        sql = $"{sql} WHERE id_chamada = @idChamada";

        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(sql, new { status = (int)status, idChamada });

        return id > 0;
    }

    public static async Task<bool> SetChamadaOrigem(Guid idChamada, Guid idChamadaOrigem)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var id = await conexao.ExecuteAsync(ChamadaQuery.UPDATE_CHAMADA_ORIGEM, new { idChamadaOrigem, idChamada });

        return id > 0;
    }

    public static async Task<bool> FinalizarChamadaAsync(Guid idChamada)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(ChamadaQuery.UPDATE_FINALIZAR, new
        {
            status = StatusChamada.Finalizado,
            idChamada
        });

        return true;
    }

    public static async Task<bool> ReiniciarChamadaAsync(Guid idChamada)
    {
        var sql = $"{ChamadaQuery.UPDATE} AND fg_status < {StatusChamada.Rejeitado}";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.ExecuteAsync(sql, new
        {
            status = (int)StatusChamada.Aguardando,
            idEquipamento = DBNull.Value,
            dataRecebida = DBNull.Value,
            dataAtendida = DBNull.Value,
            dataFinalizada = DBNull.Value,
            idChamada
        });

        return chamada > 0;
    }

    public static async Task<bool> AtribuirChamadaAsync(ChamadaDTO chamada)
    {
        var sql = $"{ChamadaQuery.UPDATE_ATRIBUIR} AND fg_status < {StatusChamada.Rejeitado}";

        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(sql, new
        {
            status = chamada.FgStatus,
            idOperador = chamada.IdOperador,
            idEquipamento = chamada.IdEquipamento
        });

        return true;
    }

    public static async Task<bool> RejeitarChamadaAsync(Guid idChamada, int idAtividadeRejeicao)
    {
        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(ChamadaQuery.UPDATE_REJEITAR, new
        {
            statusRejeicao = (int)StatusChamada.Rejeitado,
            idAtividadeRejeicao,
            idChamada,
        });

        return true;
    }

    public static async Task<List<ChamadaDTO>> GetByStatus(int fg_status)
    {
        var sql = $@"{ChamadaQuery.SELECT} WHERE fg_status < @fg_status and dt_chamada >= dateadd(day,-1,getdate())";

        using var conexao = new SqlConnection(Global.Conexao);

        var lista = await conexao.QueryAsync<ChamadaModel>(sql, new { fg_status });

        return lista.Select(ConvertToDTO).ToList();
    }

    public static async Task<Guid> Selecionar(ChamadaSelecionarDTO selecao)
    {
        var equipamento = await EquipamentoBLL.GetByIdAsync(selecao.EquipamentoId);

        if (equipamento == null)
        {
            throw new Exception("Equipamento não encontrado!");
        }

        var chamada = await GetChamadaAbertaByOperadorAsync(selecao.OperadorId, selecao.EquipamentoId);

        if (chamada == null)
        {
            throw new Exception("Chamada não encontrada!");
        }

        var chamadasPendentes = await GetChamadaDisponiveisAsync(equipamento.IdEquipamentoModelo, equipamento.IdSetorTrabalho ?? 0);

        var atividades = await AtividadeBLL.GetListAsync();

        atividades = atividades.Where(x => chamadasPendentes.Select(y => y.AtividadeId).Distinct().Contains(x.IdAtividade)).ToList();
        var equipamentosEndereco = new List<EquipamentoEnderecoDTO>();

        if (atividades.Where(x => x.FgEvitaConflitoEndereco == ConflitoDeEnderecos.BloquearEndereco || x.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco).Any())
        {
            var dataAtivo = DateTime.Now.AddMinutes(-120);
            var dataInativo = DateTime.Now.AddMinutes(-20);

            equipamentosEndereco = await EquipamentoEnderecoBLL.GetOutrosEquipamentosAtivosAsync(equipamento.IdEquipamento, equipamento.IdEquipamentoModelo, equipamento.IdSetorTrabalho ?? 0, dataAtivo, dataInativo);

            chamadasPendentes = chamadasPendentes.Where(x => atividades.Where(y => y.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZona ||
                                                                                   y.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco)
                                                                       .Select(y => y.IdAtividade)
                                                                       .Distinct().Contains(x.AtividadeId) &&
                                                             equipamentosEndereco.Select(y => y.IdEndereco).Distinct().Contains(x.AreaAmazenagemOrigemId))
                                                 .ToList();
        }

        if (atividades.Where(x => x.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZona || x.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco).Any())
        {
            equipamentosEndereco = await EquipamentoEnderecoBLL.GetByEquipamentoAsync(equipamento.IdEquipamento);

            chamadasPendentes = chamadasPendentes.Where(x => atividades.Where(y => y.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZona ||
                                                                                   y.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco)
                                                                       .Select(y => y.IdAtividade)
                                                                       .Distinct().Contains(x.AtividadeId) &&
                                                             equipamentosEndereco.Select(y => y.IdEndereco).Distinct().Contains(x.AreaAmazenagemOrigemId))
                                                 .ToList();
        }

        var equipamentosPrioridade = (await EquipamentoEnderecoPrioridadeBLL.GetListAsync()).Where(x => equipamentosEndereco.Select(y => y.IdEquipamentoEndereco).Distinct().Contains(x.IdEquipamentoEndereco));
        var chamadasComPrioridade = new List<ChamadaDisponivelDTO>();

        if (equipamentosPrioridade.Any())
        {
            chamadasComPrioridade = chamadasPendentes.Where(x => equipamentosPrioridade.Select(y => y.Prioridade.ToString())
                                                                                       .Distinct()
                                                                                       .Contains(x.AreaAmazenagemOrigemId.ToString().Substring(4, 3)))
                                                      .ToList();

            if (chamadasComPrioridade.Count != 0)
            {
                chamadasPendentes = chamadasComPrioridade;
            }
        }

        chamadasComPrioridade = chamadasPendentes.Where(x => x.Priorizar).ToList();

        if (chamadasComPrioridade.Count != 0)
        {
            chamadasPendentes = chamadasComPrioridade;
        }

        var chamadasParadas = chamadasPendentes.Where(x => !x.Processando).ToList();

        foreach (var chamadaParada in chamadasParadas)
        {
            var prioridade = await GetPrioridadeAsync(chamadaParada.ChamadaId, selecao.EquipamentoId);

            foreach (var chamadaUpdate in chamadasPendentes.Where(x => x.ChamadaId == chamadaParada.ChamadaId).ToList())
            {
                chamadaUpdate.Processando = true;
                chamadaUpdate.QuatidadePrioridade = prioridade;
            }
        }

        return chamada.IdChamada;
    }

    public static async Task<int> GetPrioridadeAsync(Guid idChamada, int idEquipamento)
    {
        try
        {
            var chamada = await GetByIdAsync(idChamada);

            if (chamada == null || chamada.IdAtividade == 0)
            {
                return 0;
            }

            using var conexao = new SqlConnection(Global.Conexao);

            var listaPrioridade = await AtividadeBLL.GetListAtividadePrioridade(chamada.IdAtividade);
            var valorPrioridade = 0;


            foreach (var prioridade in listaPrioridade)
            {
                try
                {
                    string sql = $"EXEC @rc = {prioridade.NmProcedure} @idChamada, @idEquipamento";

                    var refPrioridade = await conexao.ExecuteScalarAsync<int>(sql, new
                    {
                        idChamada,
                        idEquipamento,
                    });

                    if (refPrioridade == 0)
                    {
                        prioridade.QtPontuacao = 0;
                    }
                }
                catch
                {
                    prioridade.QtPontuacao = 0;
                }

                valorPrioridade += prioridade.QtPontuacao;
            }

            return 1;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static async Task<bool> AtualizarLeitura(Guid idChamada, long? idAreaArmazenagem, int? idPallet)
    {
        if (idAreaArmazenagem == null && idPallet == null)
        {
            throw new Exception("Nenhum parametro informado!");
        }

        var parametros = new List<string>();

        if (idAreaArmazenagem != null)
        {
            parametros.Add("id_areaarmazenagemleitura = @idAreaArmazenagem");
        }

        if (idPallet != null)
        {
            parametros.Add("id_palletleitura = @idPallet");
        }

        var sql = $"UPDATE chamada SET {string.Join(',', parametros)} WHERE id_chamada = @idChamada";

        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(sql, new { idAreaArmazenagem, idPallet, idChamada });

        return true;
    }

    public static async Task<List<ChamadaDTO>> GetListAsync(ChamadaFiltroDTO filtro)
    {
        string sql = $"{ChamadaQuery.SELECT} WHERE 1=1";

        if (filtro.Chamada != null)
        {
            if (filtro.Chamada.IdChamada == Guid.Empty)
            {
                sql = $"{sql} AND id_chamada = @idChamada";
            }

            if (filtro.Chamada.IdChamadaSuspensa == Guid.Empty)
            {
                sql = $"{sql} AND id_chamadasuspensa = @idChamadaSuspensa";
            }

            if (filtro.Chamada.IdPalletOrigem != 0)
            {
                sql = $"{sql} AND id_palletorigem = @idPalletOrigem";
            }

            if (filtro.Chamada.IdPalletDestino != 0)
            {
                sql = $"{sql} AND id_palletdestino = @idPalletDestino";
            }

            if (filtro.Chamada.IdPalletLeitura != 0)
            {
                sql = $"{sql} AND id_palletleitura = @idPalletLeitura";
            }

            if (filtro.Chamada.IdAreaArmazenagemOrigem != 0)
            {
                sql = $"{sql} AND id_areaarmazenagemorigem = @idAreaArmazenagemOrigem";
            }

            if (filtro.Chamada.IdAreaArmazenagemDestino != 0)
            {
                sql = $"{sql} AND id_areaarmazenagemdestino = @idAreaArmazenagemDestino";
            }

            if (filtro.Chamada.IdAreaArmazenagemLeitura != 0)
            {
                sql = $"{sql} AND id_areaarmazenagemleitura = @idAreaArmazenagemLeitura";
            }

            if (filtro.Chamada.IdOperador != 0)
            {
                sql = $"{sql} AND id_operador = @idOperador";
            }

            if (filtro.Chamada.IdEquipamento != 0)
            {
                sql = $"{sql} AND id_equipamento = @idEquipamento";
            }

            if (filtro.Chamada.IdAtividadeRejeicao != 0)
            {
                sql = $"{sql} AND id_atividaderejeicao = @idAtividadeRejeicao";
            }

            if (filtro.Chamada.IdAtividade != 0)
            {
                sql = $"{sql} AND id_atividade = @idAtividade";
            }
        }

        if (filtro.ListaStatusChamada.Count > 0)
        {
            sql = $"{sql} AND fg_status IN ({string.Join(",", filtro.ListaStatusChamada.Select(x => (int)x).ToArray())})";
        }

        var conexao = new SqlConnection(Global.Conexao);
        var chamadas = await conexao.QueryAsync<ChamadaModel>(sql, new
        {
            idChamada = filtro.Chamada?.IdChamada,
            idChamadaSuspensa = filtro.Chamada?.IdChamadaSuspensa,
            idPalletOrigem = filtro.Chamada?.IdPalletOrigem,
            idPalletDestino = filtro.Chamada?.IdPalletDestino,
            idPalletLeitura = filtro.Chamada?.IdPalletLeitura,
            idAreaArmazenagemOrigem = filtro.Chamada?.IdAreaArmazenagemOrigem,
            idAreaArmazenagemDestino = filtro.Chamada?.IdAreaArmazenagemDestino,
            idAreaArmazenagemLeitura = filtro.Chamada?.IdAreaArmazenagemLeitura,
            idOperador = filtro.Chamada?.IdOperador,
            idEquipamento = filtro.Chamada?.IdEquipamento,
            idAtividadeRejeicao = filtro.Chamada?.IdAtividadeRejeicao,
            idAtividade = filtro.Chamada?.IdAtividade,
        });

        return chamadas.Select(ConvertToDTO).ToList();
    }

    private static ChamadaDTO ConvertToDTO(ChamadaModel chamada)
    {
        return new()
        {
            IdChamada = chamada.IdChamada,
            IdPalletOrigem = chamada.IdPalletOrigem,
            IdPalletLeitura = chamada.IdPalletLeitura,
            IdPalletDestino = chamada.IdPalletDestino,
            IdAreaArmazenagemOrigem = chamada.IdAreaArmazenagemOrigem,
            IdAreaArmazenagemDestino = chamada.IdAreaArmazenagemDestino,
            IdAreaArmazenagemLeitura = chamada.IdAreaArmazenagemLeitura,
            IdOperador = chamada.IdOperador,
            IdEquipamento = chamada.IdEquipamento,
            IdAtividadeRejeicao = chamada.IdAtividadeRejeicao,
            IdAtividade = chamada.IdAtividade,
            FgStatus = chamada.FgStatus,
            DtChamada = chamada.DtChamada,
            DtRecebida = chamada.DtRecebida,
            DtAtendida = chamada.DtAtendida,
            DtFinalizada = chamada.DtFinalizada,
            DtRejeitada = chamada.DtRejeitada,
            DtSuspensa = chamada.DtSuspensa,
            IdChamadaSuspensa = chamada.IdChamadaSuspensa,
        };
    }
}
