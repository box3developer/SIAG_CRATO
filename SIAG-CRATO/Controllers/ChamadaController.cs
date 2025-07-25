﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SIAG_CRATO.BLLs.AreaArmazenagem;
using SIAG_CRATO.BLLs.Atividade;
using SIAG_CRATO.BLLs.AtividadeTarefa;
using SIAG_CRATO.BLLs.Chamada;
using SIAG_CRATO.BLLs.ChamadaTarefa;
using SIAG_CRATO.BLLs.Endereco;
using SIAG_CRATO.BLLs.Pallet;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Chamada;
using SIAG_CRATO.Repositories.Interfaces;
using SIAG_CRATO.Services;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChamadaController : ControllerCustom
{
    private readonly IChamadaRepository _chamadaRepository;

    public ChamadaController(IChamadaRepository chamadaRepository)
    {
        _chamadaRepository = chamadaRepository;
    }

    [HttpPost]
    public async Task<ActionResult> CriarChamada([FromBody] ChamadaInsertDTO chamada)
    {
        try
        {
            var codigoChamada = await ChamadaBLL.SetChamadaAsync(chamada);
            var tarefas = await AtividadeTarefaBLL.GetByAtividadeAsync(chamada.IdAtividade);

            foreach (var tarefa in tarefas.OrderBy(x => x.CdSequencia))
            {
                await ChamadaTarefaBLL.SetTarefaAsync(chamada.IdChamada, tarefa.IdAtividade);
            }

            await ChamadaBLL.SetStatusAsync(chamada.IdChamada, StatusChamada.Aguardando);

            return Ok(chamada.IdChamada);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("listar")]
    public async Task<ActionResult> ListaChamadas([FromBody] ChamadaFiltroDTO filtro)
    {
        try
        {
            var lista = await ChamadaBLL.GetListAsync(filtro);
            return Ok(lista);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("atualizar-leitura")]
    public async Task<ActionResult> AtualizarLeituraChamada([FromBody] ChamadaLeituraDTO leitura)
    {
        try
        {
            await ChamadaBLL.AtualizarLeitura(leitura.IdChamada, leitura.IdAreaArmazenagem, leitura.IdPallet);
            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetChamadaById(Guid id)
    {
        try
        {
            var chamada = await ChamadaBLL.GetByIdAsync(id);
            return OkResponse(chamada);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/reiniciar")]
    public async Task<ActionResult> ReiniciarChamada(Guid id)
    {
        try
        {
            await ChamadaBLL.ReiniciarChamadaAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("rejeitar/{id}")]
    public async Task<ActionResult> RejeitarChamada(Guid id)
    {
        try
        {
            var chamada = await ChamadaBLL.GetByIdAsync(id);

            if (chamada == null)
                throw new ValidacaoException("Tarefa não encontrada");

            if (chamada.FgStatus >= StatusChamada.Rejeitado)
                throw new ValidacaoException("Tarefa já rejeitada");

            await ChamadaBLL.RejeitarChamadaAsync(chamada.IdChamada);

            var result = await _chamadaRepository.CriarChamadaAsync(new CriarChamadaDTO
            {
                IdAtividade = chamada.IdAtividade,
                IdPalletOrigem = chamada.IdPalletOrigem,
                IdAreaArmazenagemOrigem = chamada.IdAreaArmazenagemOrigem,
                IdPalletDestino = chamada.IdPalletDestino,
                IdAreaArmazenagemDestino = chamada.IdAreaArmazenagemDestino,
                IdOperador = null,
                IdEquipamento = null
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/finalizar")]
    public async Task<ActionResult> FinalizarChamada(Guid id)
    {
        try
        {
            var enderecoBLL = new EnderecoBLL();

            var chamada = await ChamadaBLL.GetByIdAsync(id);

            if (chamada == null || chamada.FgStatus >= StatusChamada.Rejeitado)
                return BadRequest("Chamada não encontrada");

            var atividade = await AtividadeBLL.GetByIdAsync(chamada.IdAtividade);

            if (atividade == null)
                return BadRequest("Atividade não encontrada");

            var desalocou = atividade.FgTipoAtividade == TipoAtividade.Desalocar;

            long idAreaArmazenagemDestinoChamadaFinalizada = chamada.IdAreaArmazenagemLeitura != 0
                ? chamada.IdAreaArmazenagemLeitura
                : chamada.IdAreaArmazenagemDestino;

            long idAreaArmazenagemOrigemChamadaFinalizada = chamada.IdAreaArmazenagemLeitura != 0
                ? chamada.IdAreaArmazenagemLeitura
                : chamada.IdAreaArmazenagemOrigem;

            int idPalletChamadaFinalizada = chamada.IdPalletLeitura != 0
                ? chamada.IdPalletLeitura
                : chamada.IdPalletDestino;

            long idOperadorChamadaAnterior = chamada.IdOperador;
            int idEquipamentoChamadaAnterior = chamada.IdEquipamento;

            var listaAtividades = await AtividadeBLL.GetAtividadesByAtividadeAnteriorAsync(chamada.IdAtividade);

            var proximasChamadas = new List<CriarChamadaDTO>();

            foreach (var proximaAtividade in listaAtividades)
            {
                CriarChamadaDTO novaChamada = new();
                novaChamada.IdAtividade = proximaAtividade.IdAtividade;

                var mesmoModeloEquipamento = proximaAtividade.IdEquipamentoModelo == atividade.IdEquipamentoModelo;
                var mesmoSetorTrabalho = proximaAtividade.IdSetorTrabalho == atividade.IdSetorTrabalho;
                var desalocar = proximaAtividade.FgTipoAtividade == TipoAtividade.Desalocar;
                var movimentar = proximaAtividade.FgTipoAtividade == TipoAtividade.Movimentar;

                var areaarmazenagemAtual = await AreaArmazenagemBLL.GetByIdAsync(idAreaArmazenagemDestinoChamadaFinalizada);

                if (desalocar)
                {
                    novaChamada.IdPalletOrigem = idPalletChamadaFinalizada;
                    novaChamada.IdPalletDestino = idPalletChamadaFinalizada;
                    novaChamada.IdAreaArmazenagemOrigem = idAreaArmazenagemDestinoChamadaFinalizada;
                    novaChamada.IdAreaArmazenagemDestino = idAreaArmazenagemDestinoChamadaFinalizada;
                    novaChamada.IdOperador = null;
                    novaChamada.IdEquipamento = null;
                }
                else if (movimentar)
                {
                    var destinos = await enderecoBLL.ObterDestinoPalletAsync(idPalletChamadaFinalizada);

                    if (destinos.IsNullOrEmpty())
                        throw new ValidacaoException("Não foi possível encontrar um destino para o pallet.");

                    var areaArmazenagemNova = await AreaArmazenagemBLL.GetPortaPalletLivreAsync(destinos.First());

                    novaChamada.IdPalletOrigem = idPalletChamadaFinalizada;
                    novaChamada.IdPalletDestino = idPalletChamadaFinalizada;
                    novaChamada.IdAreaArmazenagemOrigem = idAreaArmazenagemDestinoChamadaFinalizada;
                    novaChamada.IdAreaArmazenagemDestino = areaArmazenagemNova?.IdAreaArmazenagem;
                    novaChamada.IdOperador = idOperadorChamadaAnterior;
                    novaChamada.IdEquipamento = idEquipamentoChamadaAnterior;
                }
                else
                {
                    if (mesmoSetorTrabalho)
                    {
                        novaChamada.IdAreaArmazenagemOrigem = idAreaArmazenagemDestinoChamadaFinalizada;
                        novaChamada.IdAreaArmazenagemDestino = idAreaArmazenagemDestinoChamadaFinalizada;
                        novaChamada.IdOperador = null;
                        novaChamada.IdEquipamento = null;
                    }
                    else
                    {
                        if (mesmoModeloEquipamento)
                        {
                            var destinos = await enderecoBLL.ObterDestinoPalletAsync(idPalletChamadaFinalizada);

                            if (destinos.IsNullOrEmpty())
                                throw new ValidacaoException("Não foi possível encontrar um destino para o pallet.");

                            var areaArmazenagemNova = await AreaArmazenagemBLL.GetStageInLivreAsync(destinos.First());

                            novaChamada.IdPalletOrigem = idPalletChamadaFinalizada;
                            novaChamada.IdPalletDestino = idPalletChamadaFinalizada;
                            novaChamada.IdAreaArmazenagemOrigem = idAreaArmazenagemDestinoChamadaFinalizada;
                            novaChamada.IdAreaArmazenagemDestino = areaArmazenagemNova.IdAreaArmazenagem;
                            novaChamada.IdOperador = idOperadorChamadaAnterior;
                            novaChamada.IdEquipamento = idEquipamentoChamadaAnterior;
                        }
                        else
                        {
                            var destinos = await enderecoBLL.ObterDestinoPalletAsync(idPalletChamadaFinalizada);

                            if (destinos.IsNullOrEmpty())
                                throw new ValidacaoException("Não foi possível encontrar um destino para o pallet.");

                            var areaArmazenagemNova = await AreaArmazenagemBLL.GetStageOutLivreAsync(destinos.First());

                            novaChamada.IdPalletOrigem = idPalletChamadaFinalizada;
                            novaChamada.IdPalletDestino = idPalletChamadaFinalizada;
                            novaChamada.IdAreaArmazenagemOrigem = idAreaArmazenagemDestinoChamadaFinalizada;
                            novaChamada.IdAreaArmazenagemDestino = areaArmazenagemNova.IdAreaArmazenagem;
                            novaChamada.IdOperador = idOperadorChamadaAnterior;
                            novaChamada.IdEquipamento = idEquipamentoChamadaAnterior;
                        }
                    }
                }

                novaChamada.Priorizar = false;

                proximasChamadas.Add(novaChamada);
            }

            foreach (var novaChamada in proximasChamadas)
            {
                await _chamadaRepository.CriarChamadaAsync(novaChamada);
                
                var novaAtividade = await AtividadeBLL.GetByIdAsync(novaChamada.IdAtividade.Value);

                if (novaAtividade == null)
                    throw new ValidacaoException("Atividade não encontrada");

                if (novaAtividade.FgTipoAtividade != TipoAtividade.Desalocar)
                    await AreaArmazenagemBLL.SetStatusAsync(novaChamada.IdAreaArmazenagemDestino.Value, StatusAreaArmazenagem.Reservado);
            }

            await ChamadaBLL.FinalizarChamadaAsync(chamada.IdChamada);

            if (desalocou)
            {
                await PalletBLL.SetAreaArmazenagem(idPalletChamadaFinalizada, null);

                var areaArmazenagem = await AreaArmazenagemBLL.GetByIdAsync(idAreaArmazenagemOrigemChamadaFinalizada);

                if (areaArmazenagem == null)
                    throw new ValidacaoException("Área de armazenagem não encontrada");

                bool semAgrupador = areaArmazenagem?.IdAgrupador == null || areaArmazenagem?.IdAgrupador == Guid.Empty;
                bool semAgrupadorReservado = areaArmazenagem?.IdAgrupadorReservado == null || areaArmazenagem?.IdAgrupadorReservado == Guid.Empty;
                
                if (semAgrupador && semAgrupadorReservado)
                    await AreaArmazenagemBLL.SetStatusAsync(areaArmazenagem.IdAreaArmazenagem, StatusAreaArmazenagem.Livre);
            }
            else
            {
                await PalletBLL.SetAreaArmazenagem(idPalletChamadaFinalizada, idAreaArmazenagemDestinoChamadaFinalizada);
                await AreaArmazenagemBLL.SetStatusAsync(idAreaArmazenagemDestinoChamadaFinalizada, StatusAreaArmazenagem.Ocupado);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/status/{status}")]
    public async Task<ActionResult> RejeitarChamada(Guid id, StatusChamada status)
    {
        try
        {
            var chamada = await ChamadaBLL.GetByIdAsync(id);

            if (chamada == null || chamada.FgStatus >= StatusChamada.Rejeitado)
            {
                return BadRequest("Chamada não encontrada!");
            }

            await ChamadaBLL.SetStatusAsync(chamada.IdChamada, status);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("atribuir")]
    public async Task<ActionResult> AtribuirChamada([FromBody] ChamadaDTO chamada)
    {
        try
        {
            await ChamadaBLL.AtribuirChamadaAsync(chamada);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("selecionar")]
    public async Task<ActionResult> SelecionarChamada([FromBody] ChamadaSelecionarDTO selecao)
    {
        try
        {
            var id = await ChamadaBLL.Selecionar(selecao);

            return OkResponse(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("valida-leitura")]
    public async Task<ActionResult> ValidaLeituraChamada([FromBody] ValidaLeituraChamadaDTO filtro)
    {
        try
        {

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("criar")]
    public async Task<ActionResult> CriarChamadaAsync([FromBody] CriarChamadaDTO dto)
    {
        try
        {
            var result = await _chamadaRepository.CriarChamadaAsync(dto);

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("PosicoesPortaPallet/{idPallet}")]
    public async Task<ActionResult> GetPosicoesPortaPallet(int idPallet)
    {
        var enderecoBLL = new EnderecoBLL();
        var destinos = await enderecoBLL.ObterDestinoPalletAsync(idPallet);

        return OkResponse(destinos);
    }
}
