using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Atividade;
using SIAG_CRATO.DTOs.Chamada;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AtividadeController : ControllerCustom
{

    [HttpGet]
    public async Task<IActionResult> GetAtividadeList()
    {
        try
        {
            var result = await AtividadeBLL.GetListAsync();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAtividadeByIdAsync(int id)
    {
        try
        {
            var atividade = await AtividadeBLL.GetByIdAsync(id);

            return OkResponse(atividade);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("nome/{nome}")]
    public async Task<IActionResult> GetAtividadeByNomeAsync(string nome)
    {
        var result = await AtividadeBLL.GetByNomeAsync(nome);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("equipamento-setor")]
    public async Task<IActionResult> GetAtividadesByEquipModeloSetorAsync(int id_equipamentomodelo, int id_setortrabalho)
    {
        var result = await AtividadeBLL.GetByEquipModeloSetor(id_equipamentomodelo, id_setortrabalho);
        if (result == null || result.Count == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("anterior/{idAtividadeAnterior}")]
    public async Task<IActionResult> GetAtividadesByAtividadeAnteriorAsync(int idAtividadeAnterior)
    {
        var result = await AtividadeBLL.GetAtividadesByAtividadeAnteriorAsync(idAtividadeAnterior);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}

