﻿using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AtividadeTarefa;
using SIAG_CRATO.DTOs.AtividadeTarefa;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AtividadeTarefaController : ControllerCustom
{
    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        try
        {
            var atividades = await AtividadeTarefaBLL.GetListAsync();

            return Ok(atividades);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("filtro")]
    public async Task<IActionResult> GetListAsync([FromBody] AtividadeTarefaFiltroDTO filtro)
    {
        try
        {
            var result = await AtividadeTarefaBLL.GetListAsync(filtro);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var result = await AtividadeTarefaBLL.GetByIdAsync(id);

            return OkResponse(result);

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
