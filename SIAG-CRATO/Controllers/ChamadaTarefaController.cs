using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.ChamadaTarefa;
using SIAG_CRATO.DTOs.ChamadaTarefa;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChamadaTarefaController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ChamadaTarefaDTO>>> GetListAsync()
    {
        var chamadasTarefas = await ChamadaTarefaBLL.GetListAsync();
        return Ok(chamadasTarefas);
    }

    [HttpGet("{idTarefa}")]
    public async Task<ActionResult> GetByIdAsync(int idTarefa)
    {
        try
        {
            var chamadaTarefa = await ChamadaTarefaBLL.GetByIdAsync(idTarefa);

            return Ok(chamadaTarefa);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{idChamada}/{idTarefa}")]
    public async Task<ActionResult> GetByIdAsync(Guid idChamada, int idTarefa)
    {
        try
        {
            var chamadaTarefa = await ChamadaTarefaBLL.GetAsync(idChamada, idTarefa);

            return Ok(chamadaTarefa);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> SetTarefaAsync(ChamadaTarefaDTO chamadaTarefa)
    {
        var sucesso = await ChamadaTarefaBLL.SetTarefaAsync(chamadaTarefa.IdChamada, chamadaTarefa.IdTarefa);
        return sucesso ? Ok() : BadRequest("Não foi possível associar a chamada à tarefa");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTarefaAsync(ChamadaTarefaDTO chamadaTarefa)
    {
        try
        {
            var sucesso = await ChamadaTarefaBLL.UpdateTarefaAsync(chamadaTarefa);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
