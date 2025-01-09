using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.DTOs.Equipamento;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipamentoController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EquipamentoDTO>>> GetListAsync()
    {
        var equipamentos = await EquipamentoBLL.GetListAsync();
        return Ok(equipamentos);
    }

    [HttpGet("caracois")]
    public async Task<ActionResult<IEnumerable<EquipamentoDTO>>> GetAllCaracois()
    {
        var caracols = await EquipamentoBLL.GetAllCaracois();
        return Ok(caracols);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var equipamento = await EquipamentoBLL.GetByIdAsync(id);
        if (equipamento == null)
        {
            return NotFound();
        }

        return Ok(equipamento);
    }

    [HttpGet("modelo-ativo/{modeloId}")]
    public async Task<ActionResult> GetActiveEquipByModel(int modeloId)
    {
        var equipamentos = await EquipamentoBLL.GetActiveEquipByModel(modeloId);
        return Ok(equipamentos);
    }

    [HttpGet("identificador/{identificador}")]
    public async Task<ActionResult> GetByIdentificadorAsync(string identificador)
    {
        var equipamento = await EquipamentoBLL.GetByidentificadorAsync(identificador);
        if (equipamento == null)
        {
            return NotFound();
        }

        return Ok(equipamento);
    }

    [HttpGet("caracol/{identificadorCaracol}")]
    public async Task<ActionResult> GetByCaracolAsync(string identificadorCaracol)
    {
        var equipamento = await EquipamentoBLL.GetByCaracolAsync(identificadorCaracol);
        if (equipamento == null)
        {
            return NotFound();
        }

        return Ok(equipamento);
    }

    [HttpGet("operador/{cracha}")]
    public async Task<ActionResult> GetByOperadorAsync(string cracha)
    {
        var equipamento = await EquipamentoBLL.GetByOperadorAsync(cracha);
        if (equipamento == null)
        {
            return NotFound();
        }

        return Ok(equipamento);
    }

    [HttpGet("caixa-pendente/{idCaixa}")]
    public async Task<ActionResult> GetByCaixaPendenteAsync(string idCaixa)
    {
        var equipamento = await EquipamentoBLL.GetByCaixaPendenteAsync(idCaixa);
        if (equipamento == null)
        {
            return NotFound();
        }

        return Ok(equipamento);
    }

    [HttpGet("modelo/{modeloId}")]
    public async Task<ActionResult<IEnumerable<EquipamentoDTO>>> GetByModeloAsync(int modeloId)
    {
        var equipamentos = await EquipamentoBLL.GetByModeloAsync(modeloId);
        return Ok(equipamentos);
    }

    [HttpPut("caixa-pendente")]
    public async Task<IActionResult> SetCaixaPendente(string idEquipamento, string idCaixa)
    {
        var sucesso = await EquipamentoBLL.SetCaixaPendente(idCaixa, idEquipamento);
        if (sucesso)
        {
            return Ok();
        }

        return BadRequest("Não foi possível atualizar a caixa pendente.");

    }

    [HttpPut("{id}/operador")]
    public async Task<IActionResult> SetEquipamentoOperador(int id, int idOperador)
    {
        var sucesso = await EquipamentoBLL.SetEquipamentoOperador(idOperador, id);
        if (sucesso)
        {
            return Ok();
        }

        return BadRequest("Não foi possível atualizar o operador do equipamento.");

    }

    [HttpPut("{id}/leitura")]
    public async Task<IActionResult> UpdateLeitura(int id)
    {
        var sucesso = await EquipamentoBLL.UpdateLeitura(id);
        if (sucesso)
        {
            return Ok(sucesso);
        }

        return BadRequest("Nenhum registro foi alterado");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEquipamento([FromBody] EquipamentoUpdateDTO update)
    {

        var sucesso = await EquipamentoBLL.UpdateEquipamento(update.IdEquipamento, update.IdEndereco);
        if (sucesso > 0)
        {
            return Ok(sucesso);
        }

        return BadRequest("Nenhum registro foi alterado");
    }
}
