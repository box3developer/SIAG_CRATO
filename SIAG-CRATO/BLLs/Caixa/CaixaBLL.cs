using Dapper;
using Microsoft.Data.SqlClient;
using SIAG_CRATO.BLLs.AreaArmzenagem;
using SIAG_CRATO.BLLs.CaixaLeitura;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.BLLs.Log;
using SIAG_CRATO.BLLs.Pallet;
using SIAG_CRATO.BLLs.Pedido;
using SIAG_CRATO.DTOs.Caixa;
using SIAG_CRATO.DTOs.Pedido;
using SIAG_CRATO.Models;
using System;

namespace SIAG_CRATO.BLLs.Caixa;

public class CaixaBLL
{
    public async static Task<CaixaModel?> GetByIdAsync(string id)
    {
        var sql = $"{CaixaQuery.SELECT} WHERE id_caixa = @id";

        using var conexao = new SqlConnection(Global.Conexao);
        var caixa = await conexao.QueryFirstOrDefaultAsync<CaixaModel>(sql, new { id });

        return caixa;
    }

    public async static Task<List<CaixaModel>> GetByPalletAsync(long idPallet)
    {
        var sql = $"{CaixaQuery.SELECT} WHERE id_pallet = @idPallet";

        using var conexao = new SqlConnection(Global.Conexao);
        var caixas = await conexao.QueryAsync<CaixaModel>(sql, new { idPallet });

        return caixas.ToList();
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
                NomeIdentificador = identificadorCaracol,
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
                NomeIdentificador = identificadorCaracol,
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

            var equipamento = await EquipamentoBLL.GetByidentificadorAsync(areaArmazenagem.Id_caracol ?? "") ?? throw new Exception("Equipamento não encontrado.");

            if (equipamento.LeituraPendete != null && equipamento.LeituraPendete == id_caixa)
            
                await EquipamentoBLL.SetCaixaPendente(null, equipamento.Codigo.ToString());
                
            

            var returnNovaLeitura =  await EquipamentoBLL.NovaLeitura(equipamento.Codigo, caixa.Id_caixa);

            var caixaLeitura = new CaixaLeituraModel
            {
                Id_caixa = caixa.Id_caixa,
                Dt_leitura = DateTime.Now,
                Fg_tipo = 3,
                Fg_status= 1,
                Id_operador = equipamento.OperadorId,
                Id_pallet = pallet.Id_pallet,
                Fg_cancelado = 0,
                Id_areaarmazenagem = areaArmazenagem.Id_areaarmazenagem,
                Id_caixaleitura = null,
                Id_endereco = null,
                Id_equipamento = equipamento.Codigo,
            };

            var returnCaixaLeitura = await CaixaLeituraBLL.CreateCaixaLeitura(caixaLeitura);

            return (returnNovaLeitura && returnCaixaLeitura);
        }
        catch (Exception)
        {
            throw;
        }

    }
   
    public async static Task<bool> RemoverEstufamentoCaixa(string id_caixa)
    {
        try
        {
            var caixa = await GetByIdAsync(id_caixa) ?? throw new Exception("Caixa não encotrada!");

            var pallet = await PalletBLL.GetByIdAsync(caixa.Id_pallet??0) ?? throw new Exception("Pallet não encontrado.");

            var areaArmazenagem = await AreaArmazenagemBLL.GetByIdAsync(pallet.Id_areaarmazenagem ?? 0) ?? throw new Exception("Área de armazenagem não encontrada.");

            var equipamento = await EquipamentoBLL.GetByidentificadorAsync(areaArmazenagem.Id_caracol ?? "") ?? throw new Exception("Equipamento não encontrado.");
            
            if (equipamento.LeituraPendete != null && equipamento.LeituraPendete == id_caixa)
                await EquipamentoBLL.SetCaixaPendente(null, equipamento.Codigo.ToString());

            using var conexao = new SqlConnection(Global.Conexao);

            var result = await conexao.ExecuteAsync(CaixaQuery.UPDATE_REMOVE_DT_ESTUFAMENTO, new { id_caixa });

            return result > 0;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async static Task<bool> EstufarCaixa(string id_caixa, Guid? id_requisicao)
    {
        try
        {
            var caixa = await GetByIdAsync(id_caixa);
            if (caixa == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NomeIdentificador = "",
                    IdCaixa = id_caixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Caixa {id_caixa} não encontrada",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "erro"
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Caixa não encotrada!");
            }

            var pallet = await PalletBLL.GetByIdAsync(caixa.Id_pallet??0);
            if (pallet == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NomeIdentificador = "",
                    IdCaixa = id_caixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Nenhum pallet encontrado para caixa {id_caixa}",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "erro"
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Pallet não encotrada!");
            }

            var areaArmazenagem = await AreaArmazenagemBLL.GetByIdAsync(pallet.Id_areaarmazenagem??0);
            if (areaArmazenagem == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NomeIdentificador = "",
                    IdCaixa = id_caixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Nenhum área de armazenagem encontrada para pallet {pallet.Id_pallet}",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "erro"
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Área de armazenagem não encontrada.");
            }

            var equipamento = await EquipamentoBLL.GetByCaracolAsync(areaArmazenagem.Id_caracol?? "");
            if (equipamento == null)
            {
                var logError = new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NomeIdentificador = "",
                    IdCaixa = id_caixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Equipamento {areaArmazenagem.Id_caracol} da área de armazenagem {areaArmazenagem.Id_areaarmazenagem} não identificado",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "erro"
                };

                await LogBLL.CreateLogCaracol(logError);

                throw new Exception("Equipamento não encontrado.");
            }

            if(equipamento.LeituraPendete != null && equipamento.LeituraPendete == id_caixa)
            {
                var log= new LogModel
                {
                    IdRequisicao = id_requisicao,
                    NomeIdentificador = equipamento.Identificador,
                    IdCaixa = id_caixa,
                    Data = DateTime.UtcNow,
                    Mensagem = $"Libera estufamento de caixa pendente no equipamento {equipamento.Codigo}",
                    Metodo = "EstufarCaixa",
                    IdOperador = "",
                    Tipo = "info"
                };

                await LogBLL.CreateLogCaracol(log);

                await EquipamentoBLL.SetCaixaPendente(null, equipamento.Codigo.ToString());

            }

            using var conexao = new SqlConnection(Global.Conexao);

            var resultDtEstufamento = await conexao.ExecuteAsync(CaixaQuery.UPDATE_SET_DT_ESTUFAMENTO_BY_CAIXA, new { dt_estufamento = DateTime.Now, id_caixa});

            var resultEquipamentoReset = await EquipamentoBLL.UpdateLeitura(equipamento.Codigo);
            var caixaLeitura = new CaixaLeituraModel
            {
                Id_caixa = id_caixa,
                Dt_leitura = DateTime.Now,
                Fg_tipo = 19,
                Fg_status = 1,
                Id_operador = equipamento.OperadorId,
                Id_equipamento = equipamento.Codigo,
                Id_pallet = pallet.Id_pallet,
                Id_areaarmazenagem = areaArmazenagem.Id_areaarmazenagem,
                Id_endereco = areaArmazenagem.Id_endereco,
                Fg_cancelado = 0,
                Id_caixaleitura = 0,
                Id_ordem = null
            };

            var resultCaixaLeitura = await CaixaLeituraBLL.CreateCaixaLeitura(caixaLeitura);
            var logEstufada = new LogModel
            {
                IdRequisicao = id_requisicao,
                NomeIdentificador = areaArmazenagem.Id_caracol,
                IdCaixa = id_caixa,
                Data = DateTime.UtcNow,
                Mensagem = $"Caixa {caixa.Id_caixa} estufada no equipamento {equipamento.Codigo} no pallet {pallet.Id_pallet}",
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
                NomeIdentificador = "",
                IdCaixa = id_caixa,
                Data = DateTime.UtcNow,
                Mensagem = $"Caixa {id_caixa} não encontrada",
                Metodo = "EstufarCaixa",
                IdOperador = "",
                Tipo = "erro"
            };

            await LogBLL.CreateLogCaracol(logError);

            throw new Exception("Caixa não encotrada!");
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
        var pedidos = caixas.Select(x => x.Id_pedido).Where(x => x != null).Distinct().ToList();

        var listaPedidos = new List<PedidoDTO>();
        var listaCaixas = caixas.Select(x => new CaixaPedidoDTO()
        {
            Codigo = x.Id_caixa,
            Produto = x.Cd_produto ?? "",
            Cor = x.Cd_cor ?? "",
            GradeTamanho = x.Cd_gradetamanho ?? "",
            Pares = x.Nr_pares ?? 0
        }).ToList();

        foreach (var pedido in pedidos)
        {
            var pedidoAux = await PedidoBLL.GetById(pedido ?? "");

            if (pedidoAux == null) { continue; }

            listaPedidos.Add(new()
            {
                IdPedido = pedidoAux.Id_pedido,
                CodigoPedido = pedidoAux.Cd_pedido ?? "",
                CodigoLote = pedidoAux.Cd_lote ?? "",
                Box = pedidoAux.Cd_box ?? "",
                QuantidadeCaixas = caixas.Where(x => x.Id_pedido == pedido).Count()
            });
        }

        return new ListaCaixasPedidosDTO()
        {
            Caixas = listaCaixas,
            Pedidos = listaPedidos
        };
    }
}
