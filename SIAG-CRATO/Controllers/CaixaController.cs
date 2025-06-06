﻿using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Caixa;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CaixaController : ControllerCustom
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var caixa = await CaixaBLL.GetByIdAsync(id);
        return caixa == null ? NotFound() : Ok(caixa);
    }

    [HttpGet("pallet/{idPallet}")]
    public async Task<IActionResult> GetByPalletAsync(int idPallet)
    {
        var caixas = await CaixaBLL.GetByPalletAsync(idPallet);
        return Ok(caixas);
    }

    [HttpGet("quantidade-pallet/{idPallet}")]
    public async Task<IActionResult> GetQuantidadeByPalletAsync(int idPallet)
    {
        var caixas = await CaixaBLL.GetQuantidadeByPalletAsync(idPallet);
        return Ok(caixas);
    }

    [HttpGet("quantidade-pendentes/{idAgrupador}")]
    public async Task<IActionResult> GetQuantidadePendentesAsync(int idAgrupador)
    {
        var caixas = await CaixaBLL.GetQuantidadePendentesAsync(idAgrupador);
        return Ok(caixas);
    }

    [HttpGet("fabrica-caixa/{idCaixa}")]
    public async Task<IActionResult> GetFabricaAsync (string idCaixa)
        {
            var caixas = await CaixaBLL.GetFabricaAsync(idCaixa);
            return Ok(caixas);
        }

    [HttpGet("quantidade-pedido")]
    public async Task<IActionResult> GetQuantidadeByPedido(int idPedido, long codigoPedido, long idPallet)
    {
        var caixas = await CaixaBLL.GetQuantidadeByPedido(idPedido, codigoPedido, idPallet);
        return Ok(caixas);
    }

    [HttpGet("caixa-pedido/{idPallet}")]
    public async Task<IActionResult> GetCaixasPedidos(long idPallet)
    {
        try
        {
            var caixas = await CaixaBLL.GetCaixasPedidos(idPallet);
            return OkResponse(caixas);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("pendentes")]
    public async Task<IActionResult> GetPendentesAsync()
    {
        var pendentes = await CaixaBLL.GetPendentesAsync();
        return Ok(pendentes);
    }

    [HttpGet("existe-pendentes/{idAgrupador}")]
    public async Task<IActionResult> GetTemPendentesAsync(Guid idAgrupador)
    {
        var result = await CaixaBLL.TemCaixaPendente(idAgrupador);

        return Ok(result);
    }

    [HttpGet("pendentes-lider")]
    public async Task<IActionResult> GetPendentesByLiderAsync()
    {
        var pendentes = await CaixaBLL.GetPendentesByLiderAsync();
        return Ok(pendentes);
    }

    [HttpPatch("estufar")]
    public async Task<IActionResult> EstufaCaixaAsync(string idCaixa, Guid? id_requisicao)
    {
        var pendentes = await CaixaBLL.EstufarCaixa(idCaixa, id_requisicao);
        return Ok(pendentes);
    }

    [HttpPatch("emitir-estufamento")]
    public async Task<IActionResult> EmitirEstufamentoAsync(string identificadorCaracol, Guid? id_requisicao)
    {
        var pendentes = await CaixaBLL.EmitirEstufamento(identificadorCaracol, id_requisicao);
        return Ok(pendentes);
    }

    [HttpPatch("grava-leitura")]
    public async Task<IActionResult> GravarLeituraAsync(string idCaixa, int idArea, int idPallet)
    {
        var pendentes = await CaixaBLL.GravarLeitura(idCaixa, idArea, idPallet);
        return Ok(pendentes);
    }

    [HttpPatch("remove-estufamento/{id_caixa}")]
    public async Task<IActionResult> RemoverEstufamentoCaixaAsync(string id_caixa)
    {
        var pendentes = await CaixaBLL.RemoverEstufamentoCaixa(id_caixa);
        return Ok(pendentes);
    }

    [HttpPatch("vincula-caixa-pallet")]
    public async Task<IActionResult> VinculaCaixaPalletAsync(string identificadorCaracol, int posicaoY, string idCaixa, Guid idAgrupador, Guid? id_requisicao)
    {
        try
        {
            var response = await CaixaBLL.VinculaCaixaPallet(identificadorCaracol, posicaoY, idCaixa, idAgrupador, id_requisicao);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPatch("desvincula-caixa-pallet")]
    public async Task<IActionResult> DesvinculaCaixaPalletAsync(string identificadorCaracol, int posicaoY, string idCaixa, Guid idAgrupador, Guid? id_requisicao)
    {
        try
        {
            var response = await CaixaBLL.DesvinculaCaixaPallet(identificadorCaracol, posicaoY, idCaixa, idAgrupador, id_requisicao);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
