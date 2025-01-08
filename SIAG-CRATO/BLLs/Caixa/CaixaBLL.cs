using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.AreaArmazenagem;
using SIAG_CRATO.BLLs.CaixaLeitura;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.BLLs.Log;
using SIAG_CRATO.BLLs.Pallet;
using SIAG_CRATO.BLLs.Pedido;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Caixa;
using SIAG_CRATO.DTOs.CaixaLeitura;
using SIAG_CRATO.DTOs.Pedido;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.BLLs.Caixa;

public class CaixaBLL
{
    public async static Task<CaixaDTO?> GetByIdAsync(string id)
    {
        var sql = $"{CaixaQuery.SELECT} WHERE id_caixa = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var caixa = await conexao.QueryFirstOrDefaultAsync<CaixaModel>(sql, new { id });

        if (caixa == null)
        {
            return null;
        }

        return ConvertToDTO(caixa);
    }

    public async static Task<List<CaixaDTO>> GetByPalletAsync(long idPallet)
    {
        var sql = $"{CaixaQuery.SELECT} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var caixas = await conexao.QueryAsync<CaixaModel>(sql, new { idPallet });

        return caixas.Select(ConvertToDTO).ToList();
    }

    public async static Task<int> GetQuantidadeByPalletAsync(int idPallet)
    {
        var sql = $"{CaixaQuery.SELECT_COUNT} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var quantidade = await conexao.QueryFirstOrDefaultAsync<int>(sql, new { idPallet });

        return quantidade;
    }

    public async static Task<int> GetQuantidadePendentesAsync(int idAgrupador)
    {
        var sql = $"{CaixaQuery.SELECT_COUNT} WHERE id_agrupador = @idAgrupador AND (fg_status < 4 OR fg_status = 8)";

        using var conexao = new SqlConnection(Global.Conexao);
        var quantidade = await conexao.QueryFirstOrDefaultAsync<int>(sql, new { idAgrupador });

        return quantidade;
    }

    public async static Task<bool> EmitirEstufamento(string identificadorCaracol, Guid? id_requisicao)
    {
        try
        {
            var log = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = identificadorCaracol,
                IdCaixa = "",
                Data = DateTime.UtcNow,
                Mensagem = $"Emitindo estufamento para caracol {identificadorCaracol}",
                Metodo = "EmitirEstufamento",
                IdOperador = "",
                Tipo = "info"

            };

            using var httpClient = new HttpClient();

            await httpClient.GetStringAsync(new Uri($"http://gra-lxsobcaracol.sob.ad-grendene.com:3000/EmitirEstufamento/{identificadorCaracol}"));

            await LogBLL.CreateLogCaracol(log);
            return true;
        }
        catch (Exception)
        {
            var logError = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = identificadorCaracol,
                IdCaixa = "",
                Data = DateTime.UtcNow,
                Mensagem = $"Erro ao emitir estufamento para caracol {identificadorCaracol}",
                Metodo = "EmitirEstufamento",
                IdOperador = "",
                Tipo = "erro"
            };

            await LogBLL.CreateLogCaracol(logError);

            throw;
        }

    }

    public async static Task<bool> GravarLeitura(string id_caixa, int id_areaarmazenagem, int id_pallet)
    {
        try
        {
            var caixa = await GetByIdAsync(id_caixa) ?? throw new Exception("Caixa não encotrada!");

            var pallet = await PalletBLL.GetByIdAsync(id_pallet) ?? throw new Exception("Pallet não encontrado.");

            var areaArmazenagem = await AreaArmazenagemBLL.GetByIdAsync(id_areaarmazenagem) ?? throw new Exception("Área de armazenagem não encontrada.");

            var equipamento = await EquipamentoBLL.GetByidentificadorAsync(areaArmazenagem.IdCaracol ?? "") ?? throw new Exception("Equipamento não encontrado.");

            if (equipamento.CdLeituraPendente != null && equipamento.CdLeituraPendente == id_caixa)
            {
                await EquipamentoBLL.SetCaixaPendente(null, equipamento.IdEquipamento.ToString());
            }

            var returnNovaLeitura = await EquipamentoBLL.NovaLeitura(equipamento.IdEquipamento, caixa.IdCaixa);

            var caixaLeitura = new CaixaLeituraDTO
            {
                IdCaixa = caixa.IdCaixa,
                DtLeitura = DateTime.Now,
                FgTipo = 3,
                FgStatus = 1,
                IdOperador = equipamento.IdOperador,
                IdPallet = pallet.IdPallet,
                FgCancelado = 0,
                IdAreaArmazenagem = areaArmazenagem.IdAreaArmazenagem,
                IdCaixaLeitura = null,
                IdEndereco = null,
                IdEquipamento = equipamento.IdEquipamento,
            };

            var returnCaixaLeitura = await CaixaLeituraBLL.CreateCaixaLeitura(caixaLeitura);

            return (returnNovaLeitura && returnCaixaLeitura);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
   
    public async static Task<bool> RemoverEstufamentoCaixa(string id_caixa)
    {
        try
        {
            var caixa = await GetByIdAsync(id_caixa) ?? throw new Exception("Caixa não encotrada!");

            var pallet = await PalletBLL.GetByIdAsync(caixa.IdPallet ?? 0) ?? throw new Exception("Pallet não encontrado.");

            var areaArmazenagem = await AreaArmazenagemBLL.GetByIdAsync(pallet.IdAreaArmazenagem ?? 0) ?? throw new Exception("Área de armazenagem não encontrada.");

            var equipamento = await EquipamentoBLL.GetByidentificadorAsync(areaArmazenagem.IdCaracol ?? "") ?? throw new Exception("Equipamento não encontrado.");

            if (equipamento.CdLeituraPendente != null && equipamento.CdLeituraPendente == id_caixa)
            {
                await EquipamentoBLL.SetCaixaPendente(null, equipamento.IdEquipamento.ToString());
            }

            using var conexao = new SqlConnection(Global.Conexao);

            var result = await conexao.ExecuteAsync(CaixaQuery.UPDATE_REMOVE_DT_ESTUFAMENTO, new { id_caixa });

            return result > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async static Task<bool> EstufarCaixa(string idCaixa, Guid? id_requisicao)
    {
        try
        {
            var caixa = await GetByIdAsync(idCaixa);
            if (caixa == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = "",
                    IdCaixa = idCaixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Caixa {idCaixa} não encontrada",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "erro"
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Caixa não encotrada!");
            }

            var pallet = await PalletBLL.GetByIdAsync(caixa.IdPallet ?? 0);
            if (pallet == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = "",
                    IdCaixa = idCaixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Nenhum pallet encontrado para caixa {idCaixa}",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "erro"
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Pallet não encotrada!");
            }

            var areaArmazenagem = await AreaArmazenagemBLL.GetByIdAsync(pallet.IdAreaArmazenagem ?? 0);
            if (areaArmazenagem == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = "",
                    IdCaixa = idCaixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Nenhum área de armazenagem encontrada para pallet {pallet.IdPallet}",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "erro"
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Área de armazenagem não encontrada.");
            }

            var equipamento = await EquipamentoBLL.GetByCaracolAsync(areaArmazenagem.IdCaracol ?? "");
            if (equipamento == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = "",
                    IdCaixa = idCaixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Equipamento {areaArmazenagem.IdCaracol} da área de armazenagem {areaArmazenagem.IdAreaArmazenagem} não identificado",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "erro"
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Equipamento não encontrado.");
            }

            if (equipamento.CdLeituraPendente != null && equipamento.CdLeituraPendente == idCaixa)
            {
                var log = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = equipamento.NmIdentificador,
                    IdCaixa = idCaixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Libera estufamento de caixa pendente no equipamento {equipamento.IdEquipamento}",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "info"
                };

                await LogBLL.CreateLogCaracol(log);

                await EquipamentoBLL.SetCaixaPendente(null, equipamento.IdEquipamento.ToString());

            }

            using var conexao = new SqlConnection(Global.Conexao);

            var resultDtEstufamento = await conexao.ExecuteAsync(CaixaQuery.UPDATE_SET_DT_ESTUFAMENTO_BY_CAIXA, new { dt_estufamento = DateTime.Now, idCaixa });

            var resultEquipamentoReset = await EquipamentoBLL.UpdateLeitura(equipamento.IdEquipamento);
            var caixaLeitura = new CaixaLeituraDTO
            {
                IdCaixa = idCaixa,
                DtLeitura = DateTime.Now,
                FgTipo = 19,
                FgStatus = 1,
                IdOperador = equipamento.IdOperador,
                IdEquipamento = equipamento.IdEquipamento,
                IdPallet = pallet.IdPallet,
                IdAreaArmazenagem = areaArmazenagem.IdAreaArmazenagem,
                IdEndereco = areaArmazenagem.IdEndereco,
                FgCancelado = 0,
                IdCaixaLeitura = 0,
                IdOrdem = null
            };

            var resultCaixaLeitura = await CaixaLeituraBLL.CreateCaixaLeitura(caixaLeitura);
            var logEstufada = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = areaArmazenagem.IdCaracol,
                IdCaixa = idCaixa,
                Data = DateTime.UtcNow,
                Mensagem = $"Caixa {caixa.IdCaixa} estufada no equipamento {equipamento.IdEquipamento} no pallet {pallet.IdPallet}",
                Metodo = "EstufarCaixa",
                IdOperador = "",
                Tipo = "info"
            };
            await LogBLL.CreateLogCaracol(logEstufada);

            return resultDtEstufamento > 0 && resultEquipamentoReset && resultCaixaLeitura;
        }
        catch (Exception)
        {
            var logError = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = "",
                IdCaixa = idCaixa,
                Data = DateTime.UtcNow,
                Mensagem = $"Caixa {idCaixa} não encontrada",
                Metodo = "EstufarCaixa",
                IdOperador = "",
                Tipo = "erro"
            };

            await LogBLL.CreateLogCaracol(logError);

            throw new Exception("Caixa não encotrada!");
        }
    }
    
    public async static Task<bool> TemCaixaPendente(Guid idAgrupador)
    {
        using var conexao = new SqlConnection(Global.Conexao);

        var result = await conexao.ExecuteScalarAsync<bool>(CaixaQuery.COUNT_PENDENTES, new { idAgrupador });

        return result;
    }

    public async static Task<bool> VinculaCaixaPallet(string identificadorCaracol, int posicaoY, string idCaixa, Guid idAgrupador, Guid? id_requisicao)
    {
        try
        {
            var logInitial = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = identificadorCaracol,
                IdCaixa = idCaixa,
                Data = DateTime.Now,
                Mensagem = $"Inciando vínculo da caixa {idCaixa} com agrupador {idAgrupador} na posicção {posicaoY} do equipamento {identificadorCaracol}",
                Metodo = "VinculaCaixaPallet",
                IdOperador = "",
                Tipo = "info",
            };

            await LogBLL.CreateLogCaracol(logInitial);

            var areaArmazenagem = await AreaArmazenagemBLL.GetByPosicaoAsync(identificadorCaracol, posicaoY);
            if(areaArmazenagem == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = identificadorCaracol,
                    IdCaixa = idCaixa,
                    Data = DateTime.Now,
                    Mensagem = $"Não foi possível identificar área de armazenagem na posição {posicaoY} do equipamento {identificadorCaracol}",
                    Metodo = "VincularCaixaPallet",
                    IdOperador = "",
                    Tipo = "erro",
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Área de armazenagem não encontrada.");
            }

            var pallet = await PalletBLL.GetByAreaArmazenagemAsync(areaArmazenagem.IdAreaArmazenagem);
            if (pallet == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = identificadorCaracol,
                    IdCaixa = idCaixa,
                    Data = DateTime.Now,
                    Mensagem = $"Nenhum pallet encontrado para a área de armazenagem {areaArmazenagem.IdAreaArmazenagem}",
                    Metodo = "VincularCaixaPallet",
                    IdOperador = "",
                    Tipo = "erro",
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Pallet não encontrado.");
            }

            if(pallet.IdAgrupador == Guid.Empty)
            {

                var logIdAgrupadorVazio = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = identificadorCaracol,
                    IdCaixa = idCaixa,
                    Data = DateTime.Now,
                    Mensagem = $"Vincula agrupador {idAgrupador} no pallet {pallet.IdPallet} e altera status do pallet para 2",
                    Metodo = "VincularCaixaPallet",
                    IdOperador = "",
                    Tipo = "info",
                };

                var logInfo = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = identificadorCaracol,
                    IdCaixa = idCaixa,
                    Data = DateTime.Now,
                    Mensagem = $"Vincula agrupador {idAgrupador} no pallet {pallet.IdPallet} e altera status do pallet para 2",
                    Metodo = "VincularCaixaPallet",
                    IdOperador = "",
                    Tipo = "info",
                };

                await LogBLL.CreateLogCaracol(logIdAgrupadorVazio);

                await LogBLL.CreateLogCaracol(logInfo);

                await PalletBLL.SetStatusAndAgrupadorById(pallet.IdPallet, StatusPallet.Ocupado, idAgrupador);

                await AreaArmazenagemBLL.SetStatusAsync(areaArmazenagem.IdAreaArmazenagem, StatusAreaArmazenagem.Ocupado);

            }

            using var conexao = new SqlConnection(Global.Conexao);

            var result = await conexao.ExecuteAsync(CaixaQuery.UPDATE_PALLET_STATUS, new { idPallet = pallet.IdPallet, status = StatusCaixa.Armazenada, idCaixa });
            
            if(result <= 0)
            {
                var logFinishError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NmIdentificador = identificadorCaracol,
                    IdCaixa = idCaixa,
                    Data = DateTime.Now,
                    Mensagem = $"Erro ao finalizar vínculo da caixa {idCaixa} com agrupador {idAgrupador} na posicção {posicaoY} do equipamento {identificadorCaracol}",
                    Metodo = "VincularCaixaPallet",
                    IdOperador = "",
                    Tipo = "erro",
                };

                await LogBLL.CreateLogCaracol(logFinishError);

                throw new Exception("A atribuição Pallet - Caixa com Status '4 = Armazenada' não foi bem sucedido. Linhas atualizadas: " + result);
            }

            var logFinish = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = identificadorCaracol,
                IdCaixa = idCaixa,
                Data = DateTime.Now,
                Mensagem = $"Finalizado vínculo da caixa {idCaixa} com agrupador {idAgrupador} na posicção {posicaoY} do equipamento {identificadorCaracol}",
                Metodo = "VincularCaixaPallet",
                IdOperador = "",
                Tipo = "info",
            };

            await LogBLL.CreateLogCaracol(logFinish);

            return result > 0;

        }
        catch (Exception ex)
        {
            var logError = new LogModel
            {
                IdRequisicao = id_requisicao,
                NmIdentificador = identificadorCaracol,
                IdCaixa = idCaixa,
                Data = DateTime.Now,
                Mensagem = $"Erro ao vincular caixa {idCaixa} com agrupador {idAgrupador} na posicção {posicaoY} do equipamento {identificadorCaracol}",
                Metodo = "VincularCaixaPallet",
                IdOperador = "",
                Tipo = "erro",
            };

            await LogBLL.CreateLogCaracol(logError);

            throw new Exception(ex.Message, ex);
        }
    }

    public async static Task<bool> DesvinculaCaixaPallet(string identificadorCaracol, int posicaoY, string idCaixa, Guid idAgrupador, Guid? id_requisicao)
    {
        try
        {
            var areaArmazenagem = await AreaArmazenagemBLL.GetByPosicaoAsync(identificadorCaracol, posicaoY)
                ??
                throw new Exception("Área de armazenagem não encontrada.");

            var pallet = await PalletBLL.GetByAreaArmazenagemAsync(areaArmazenagem.IdAreaArmazenagem)
                ?? throw new Exception("Pallet não encontrado.");

            using var conexao = new SqlConnection(Global.Conexao);

            var result = await conexao.ExecuteAsync(CaixaQuery.UPDATE_PALLET_STATUS, new { idPallet = (int?)null, status= StatusCaixa.Embalada, idCaixa });

            return result > 0;

        }
        catch (Exception ex) 
        {

            throw new Exception(ex.Message, ex);
        }
    }

    public async static Task<Dictionary<string, int>> GetPendentesAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var caixasPendentes = await conexao.QueryAsync<Tuple<string, int>>(CaixaQuery.SELECT_COUNT_PENDENTES);

        return caixasPendentes.ToDictionary(x => x.Item1, x => x.Item2);
    }

    public async static Task<Dictionary<string, int>> GetPendentesByLiderAsync()
    {
        using var conexao = new SqlConnection(Global.Conexao);
        var caixasPendentes = await conexao.QueryAsync<Tuple<string, int>>(CaixaQuery.SELECT_PENDENTES_LIDER);

        return caixasPendentes.ToDictionary(x => x.Item1, x => x.Item2);
    }

    public async static Task<string> GetFabricaAsync(string idCaixa)
    {
        var sqlCaixa = "SELECT id_programa FROM caixa WITH(NOLOCK) WHERE id_caixa = @idCaixa";
        var sqlPrograma = "SELECT cd_fabrica FROM programa WITH(NOLOCK) WHERE id_programa = @idPrograma";

        using var conexao = new SqlConnection(Global.Conexao);
        var idPrograma = await conexao.QueryFirstOrDefaultAsync<string>(sqlCaixa, new { idCaixa });
        var fabrica = await conexao.QueryFirstOrDefaultAsync(sqlPrograma, new { idPrograma });

        return fabrica ?? "";
    }

    public async static Task<int> GetQuantidadeByPedido(int idPedido, long codigoPedido, long idPallet)
    {
        var sql = $"{CaixaQuery.SELECT_COUNT} WHERE id_pedido = @idPedido AND cd_pedido = @codigoPedido AND id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var quantidade = await conexao.QueryFirstOrDefaultAsync<int>(sql, new { idPedido, codigoPedido, idPallet });

        return quantidade;
    }

    public async static Task<ListaCaixasPedidosDTO> GetCaixasPedidos(long idPallet)
    {
        var caixas = await GetByPalletAsync(idPallet);
        var pedidos = caixas.Select(x => x.IdPedido).Where(x => x != null).Distinct().ToList();

        var listaPedidos = new List<PedidoDTO>();
        var listaCaixas = caixas.Select(x => new CaixaPedidoDTO()
        {
            Codigo = x.IdCaixa,
            Produto = x.CdProduto ?? "",
            Cor = x.CdCor ?? "",
            GradeTamanho = x.CdGradeTamanho ?? "",
            Pares = x.NrPares ?? 0
        }).ToList();

        foreach (var pedido in pedidos)
        {
            var pedidoAux = await PedidoBLL.GetById(pedido ?? "");

            if (pedidoAux == null) { continue; }

            listaPedidos.Add(new()
            {
                IdPedido = pedidoAux.IdPedido,
                CdPedido = pedidoAux.CdPedido,
                CdLote = pedidoAux.CdLote ,
                CdBox = pedidoAux.CdBox,
                NrCaixas = caixas.Where(x => x.IdPedido == pedido).Count()
            });
        }

        return new ListaCaixasPedidosDTO()
        {
            Caixas = listaCaixas,
            Pedidos = listaPedidos
        };
    }

    private static CaixaDTO ConvertToDTO(CaixaModel caixa)
    {
        return new()
        {
            IdCaixa = caixa.IdCaixa,
            IdAgrupador = caixa.IdAgrupador,
            IdPallet = caixa.IdPallet,
            IdPrograma = caixa.IdPrograma,
            IdPedido = caixa.IdPedido,
            CdProduto = caixa.CdProduto,
            CdCor = caixa.CdCor,
            CdGradeTamanho = caixa.CdGradeTamanho,
            NrCaixa = caixa.NrCaixa,
            NrPares = caixa.NrPares,
            FgRFID = caixa.FgRFID,
            FgStatus = caixa.FgStatus!= null ? (StatusCaixa)caixa.FgStatus : null,
            DtEmbalagem = caixa.DtEmbalagem,
            DtExpedicao = caixa.DtExpedicao,
            DtSorter = caixa.DtSorter,
            DtEstufamento = caixa.DtEstufamento,
            DtLeitura = caixa.DtLeitura,
        };
    }
}
