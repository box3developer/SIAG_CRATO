using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Pallet;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.AreaArmazenagem;
using SIAG_CRATO.DTOs.Pallet;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PalletController : ControllerCustom
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PalletDTO pallet)
    {
        await PalletBLL.InsertAsync(pallet);
        return CreatedAtAction("GetById", new { id = pallet.IdPallet }, pallet);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pallets = await PalletBLL.GetListAsync();
        return Ok(pallets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var pallet = await PalletBLL.GetByIdAsync(id);

            return OkResponse(pallet);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("identificador/{identificador}")]
    public async Task<IActionResult> GetByIdentificador(string identificador)
    {
        var pallet = await PalletBLL.GetByIdentificadorAsync(identificador);
        if (pallet == null)
        {
            return NotFound();
        }

        return Ok(pallet);
    }

    [HttpPost("identificador")]
    public async Task<IActionResult> GetByIdentificadorOnly([FromBody] PalletFiltroDTO filtro)
    {
        try
        {
            var pallet = await PalletBLL.GetByIdentificadorAsync(filtro.CdIdentificador, filtro.IdPallet);

            return OkResponse(pallet);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("area/{areaArmazenagem}")]
    public async Task<IActionResult> GetByAreaArmazenagem(long areaArmazenagem)
    {
        var pallets = await PalletBLL.GetByAreaArmazenagemAsync(areaArmazenagem);
        return Ok(pallets);
    }

    [HttpGet("{id}/quantidade-caixas")]
    public async Task<IActionResult> GetQuantidadeCaixas(int id)
    {
        var quantidade = await PalletBLL.GetQuantidadeCaixasAsync(id);
        return Ok(quantidade);
    }

    [HttpPut("status")]
    public async Task<IActionResult> UpdateStatus(int id, StatusPallet status)
    {
        var result = await PalletBLL.SeStatusAsync(id, status);
        if (result > 0)
        {
            return Ok(result);
        }

        return BadRequest("Erro ao atualizar base de dados");
    }

    [HttpPut("vincular-agrupador")]
    public async Task<IActionResult> VincularAgrupadorReservado(AreaArmazenagemDTO areaAtual)
    {
        var sucesso = await PalletBLL.VincularAgrupadorAreaReservadaAsync(areaAtual.IdCaracol, areaAtual);
        if (sucesso)
        {
            return Ok("Pallet vinculado com sucesso.");
        }

        return BadRequest("Erro ao vincular o pallet.");

    }

    [HttpPost("vincular-por-prioridade")]
    public async Task<IActionResult> VincularNovoPalletPorPrioridade(string identificadorCaracol, AreaArmazenagemDTO areaAtual, int nivelAgrupador)
    {
        var sucesso = await PalletBLL.VincularNovoPalletPorPrioridadeAsync(identificadorCaracol, areaAtual, nivelAgrupador);
        if (sucesso)
        {
            return Ok("Pallet vinculado com sucesso.");
        }

        return BadRequest("Erro ao vincular o pallet.");

    }

    [HttpPost("vincular-disponivel")]
    public async Task<IActionResult> VincularNovoPalletDisponivel([FromBody] AreaArmazenagemDTO areaAtual)
    {
        var sucesso = await PalletBLL.VincularNovoPalletDisponivelAsync(areaAtual.IdCaracol, areaAtual);
        if (sucesso)
        {
            return Ok("Pallet vinculado com sucesso.");
        }
        else
        {
            return BadRequest("Erro ao vincular o pallet.");
        }
    }

    [HttpPost("vincular-reservado")]
    public async Task<IActionResult> VincularNovoPalletReservado(string identificadorCaracol, AreaArmazenagemDTO areaAtual)
    {
        var sucesso = await PalletBLL.VincularNovoPalletReservadoAsync(identificadorCaracol, areaAtual);
        if (sucesso)
        {
            return Ok("Pallet vinculado com sucesso.");
        }
        else
        {
            return BadRequest("Erro ao vincular o pallet.");
        }
    }

    [HttpGet("endereco/{idEndereco}/pallet/{idPallet}")]
    public async Task<IActionResult> GetPalletsByEndereco(int idEndereco, int idPallet)
    {
        var pallets = await PalletBLL.GetQtyPallets(idEndereco, idPallet);
        return Ok(pallets);
    }

    [HttpPut("encher-pallet/pallet/{idPallet}/requisicao/{id_requisicao}")]
    public async Task<IActionResult> EncherPallet(int idPallet, Guid? id_requisicao)
    {
        try
        {
            var response = await PalletBLL.SetPalletCheio(idPallet, id_requisicao);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
