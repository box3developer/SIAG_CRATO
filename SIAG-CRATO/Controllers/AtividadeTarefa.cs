using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AtividadeTarefa;
using SIAG_CRATO.DTOs.AtividadeTarefa;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AtividadeTarefa : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await AtividadeTarefaBLL.GetListAsync();
        if (result == null || result.Count == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("filtro")]
    public async Task<IActionResult> GetListAsync([FromBody] AtividadeTarefaFiltroDTO filtro)
    {
        var result = await AtividadeTarefaBLL.GetListAsync(filtro);
        if (result == null || result.Count == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var result = await AtividadeTarefaBLL.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("atividade/{idAtividade}")]
    public async Task<IActionResult> GetByAtividadeAsync(int idAtividade)
    {
        var result = await AtividadeTarefaBLL.GetByAtividadeAsync(idAtividade);
        if (result == null || result.Count == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
