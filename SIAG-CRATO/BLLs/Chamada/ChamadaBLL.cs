using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.Atividade;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.BLLs.EquipamentoEndereco;
using SIAG_CRATO.BLLs.EquipamentoEnderecoPrioridade;
using SIAG_CRATO.BLLs.Operador;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Chamada;
using SIAG_CRATO.DTOs.EquipamentoEndereco;
using SIAG_CRATO.Models;
using SIAG_CRATO.Util;

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
            status = StatusChamada.Aguardando,
            idEquipamentoModelo,
            idSetorTrabalho
        });

        return chamadas.ToList();
    }

    public static async Task<ChamadaDTO?> GetChamadaAbertaByOperadorAsync(long idOperador, int idEquipamento)
    {
        var sql = $@"{ChamadaQuery.SELECT} 
                     WHERE (id_operador = @idOperador or id_equipamento = @idEquipamento)
                           AND fg_status < @status
                     ORDER BY fg_status desc, dt_chamada desc";

        using var conexao = new SqlConnection(Global.Conexao);
        var chamada = await conexao.QueryFirstOrDefaultAsync<ChamadaModel>(sql, new { idOperador, idEquipamento, status = StatusChamada.Rejeitado });

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
        var sql = $"{ChamadaQuery.UPDATE_ATRIBUIR} AND fg_status < {(int)StatusChamada.Rejeitado}";

        using var conexao = new SqlConnection(Global.Conexao);
        await conexao.ExecuteAsync(sql, new
        {
            status = (int)chamada.FgStatus,
            idOperador = chamada.IdOperador,
            idEquipamento = chamada.IdEquipamento,
            idChamada = chamada.IdChamada
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
        var equipamento = await EquipamentoBLL.GetByIdAsync(selecao.IdEquipamento);

        if (equipamento == null)
            throw new ValidacaoException("Equipamento não encontrado!");

        var operador = await OperadorBLL.GetByCrachaAsync(selecao.IdOperador);

        if (operador == null)
            throw new ValidacaoException("Operador não encontrado!");

        var chamada = await GetChamadaAbertaByOperadorAsync(selecao.IdOperador, selecao.IdEquipamento);

        if (chamada != null)
            return chamada.IdChamada;

        var chamadasPendentes = await GetChamadaDisponiveisAsync(equipamento.IdEquipamentoModelo, equipamento.IdSetorTrabalho ?? 0);

        var atividades = await AtividadeBLL.GetListAsync();

        var atividadesChamadasPendentes = chamadasPendentes.Select(y => y.IdAtividade).Distinct().ToList();
        atividades = atividades.Where(x => atividadesChamadasPendentes.Contains(x.IdAtividade)).ToList();

        var equipamentosEndereco = new List<EquipamentoEnderecoDTO>();

        var atividadesComConflitoDeEndereco = atividades
                                                .Where(x => x.FgEvitaConflitoEndereco == ConflitoDeEnderecos.BloquearEndereco ||
                                                            x.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco)
                                                .Select(x => x.IdAtividade)
                                                .ToList();

        if (atividadesComConflitoDeEndereco.Count != 0)
        {
            var dataAtivo = DateTime.Now.AddMinutes(-120);
            var dataInativo = DateTime.Now.AddMinutes(-20);

            equipamentosEndereco = await EquipamentoEnderecoBLL.GetOutrosEquipamentosAtivosAsync(equipamento.IdEquipamento, equipamento.IdEquipamentoModelo, equipamento.IdSetorTrabalho ?? 0, dataAtivo, dataInativo);

            var idsEnderecosOcupados = equipamentosEndereco.Select(x => x.IdEndereco).Distinct().ToList();

            chamadasPendentes = chamadasPendentes.Where(x => !(atividadesComConflitoDeEndereco.Contains(x.IdAtividade) &&
                                                                idsEnderecosOcupados.Contains(x.IdEnderecoOrigem)))
                                                 .ToList();
        }

        var atividadesDeOutraZona = atividades
                                        .Where(x => x.FgEvitaConflitoEndereco == ConflitoDeEnderecos.BloquearEndereco ||
                                                    x.FgEvitaConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco)
                                        .Select(x => x.IdAtividade)
                                        .ToList();

        if (atividadesDeOutraZona.Count != 0)
        {
            equipamentosEndereco = await EquipamentoEnderecoBLL.GetByEquipamentoAsync(equipamento.IdEquipamento);

            var idsEnderecos = equipamentosEndereco.Select(x => x.IdEndereco).ToList();

            chamadasPendentes = chamadasPendentes.Where(x =>
                                                    !(atividadesDeOutraZona.Contains(x.IdAtividade) &&
                                                    !idsEnderecos.Contains(x.IdEnderecoOrigem)))
                                                 .ToList();
        }

        var equipamentosPrioridade = (await EquipamentoEnderecoPrioridadeBLL.GetListAsync())
                                        .Where(x => equipamentosEndereco
                                        .Select(y => y.IdEquipamentoEndereco)
                                        .Distinct()
                                        .ToList()
                                        .Contains(x.IdEquipamentoEndereco));

        var chamadasComPrioridade = new List<ChamadaDisponivelDTO>();

        // Além da prirização pela propriedade definida na atividade, verifica se o equipamento tem prioridade definida
        if (equipamentosPrioridade.Any())
        {
            var listaPrioridade = equipamentosPrioridade.Select(y => y.Prioridade.ToString()).Distinct().ToList();

            chamadasComPrioridade = chamadasPendentes.Where(x => listaPrioridade.Contains(x.IdAreaAmazenagemOrigem.ToString().Substring(4, 3)))
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
            var prioridade = await GetPrioridadeAsync(chamadaParada.IdChamada, selecao.IdEquipamento);

            foreach (var chamadaUpdate in chamadasPendentes.Where(x => x.IdChamada == chamadaParada.IdChamada).ToList())
            {
                chamadaUpdate.Processando = true;
                chamadaUpdate.QuatidadePrioridade = prioridade;
            }
        }

        var chamadaSelecioanda = chamadasParadas
                                    .Where(x => x.QuatidadePrioridade >= 0)
                                    .OrderBy(x => x)
                                    .OrderByDescending(x => x.QuatidadePrioridade)
                                    .OrderBy(x => x.DataChamada)
                                    .Select(x => x.IdChamada)
                                    .FirstOrDefault();

        return chamadaSelecioanda;
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

    public static async Task<ValidaLeituraChamadaRetornoDTO> ValidarLeituraChamada(ValidaLeituraChamadaDTO filtro)
    {
        if (filtro.AtividadeRotina == null)
        {
            return new()
            {
                Valido = false,
                Mensagem = "Deve ser informado a atividade referente."
            };
        }

        if (filtro.AtividadeRotina.FgTipo == TipoRotina.MetodoClasse)
        {
            return new()
            {
                Valido = false
            };
        }
        else
        {
            if (filtro.Chamada == null)
            {
                throw new Exception("Deve ser informado a chamada referente.");
            }

            var filtros = new Dictionary<string, object>
            {
                { "@id_chamada", filtro.Chamada.IdChamada }
            };

            var parametros = new DynamicParameters(filtros);
            parametros.Add("@mensagem", dbType: DbType.String, direction: ParameterDirection.Output, size: 1000);
            parametros.Add("@RetVal", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            using var conexao = new SqlConnection(Global.Conexao);

            var linhas = await conexao.ExecuteAsync(filtro.AtividadeRotina.NmProcedure, parametros, commandType: CommandType.StoredProcedure);

            var outputMsg = parametros.Get<string?>("@mensagem");
            var outputIdParam = parametros.Get<int?>("@RetVal");

            return new()
            {
                Valido = (outputIdParam ?? 0) > 0,
                Mensagem = outputMsg ?? ""
            };
        }
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
