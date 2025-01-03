using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.LiderVirtual;
using SIAG_CRATO.DTOs.LiderVirtual;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LiderVirturalController : ControllerBase
{

    [HttpGet("operador/{cracha}")]
    public async Task<IActionResult> GetByOperador(string cracha)
    {
        var liderVirtual = await LiderVirtualBLL.GetByOperador(cracha);
        if (liderVirtual == null)
        {
            return NotFound();
        }

        return Ok(liderVirtual);
    }

    [HttpGet("destino/{idEquipamento}")]
    public async Task<IActionResult> GetByDestino(int idEquipamento)
    {
        var liderVirtual = await LiderVirtualBLL.GetByDestino(idEquipamento);
        if (liderVirtual == null)
        {
            return NotFound();
        }

        return Ok(liderVirtual);
    }


    [HttpGet("origem/{idEquipamento}")]
    public async Task<IActionResult> GetByOrigem(int idEquipamento)
    {
        var liderVirtual = await LiderVirtualBLL.GetByOrigem(idEquipamento);
        if (liderVirtual == null)
        {
            return NotFound();
        }

        return Ok(liderVirtual);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LiderVirtualDTO liderVirtual)
    {
        var novoLiderVirtual = await LiderVirtualBLL.Create(liderVirtual);
        return Ok(novoLiderVirtual);
    }

    [HttpPut]
    public async Task<IActionResult> Update(LiderVirtualDTO liderVirtual)
    {
        var sucesso = await LiderVirtualBLL.Update(liderVirtual);
        if (sucesso == 0)
        {
            return NoContent();
        }

        return BadRequest("Não foi possível atualizar o líder virtual.");

    }
}
