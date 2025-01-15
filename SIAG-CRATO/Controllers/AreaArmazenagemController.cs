using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AreaArmazenagem;
using SIAG_CRATO.Data;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AreaArmazenagemController : ControllerCustom
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var result = await AreaArmazenagemBLL.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}/chamada")]
    public async Task<IActionResult> GetByIdToChamadaAsync(int id)
    {
        try
        {
            var result = await AreaArmazenagemBLL.GetByIdAsync(id);

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("identificador/{identificador}")]
    public async Task<IActionResult> GetByIdentificadorAsync(string identificador)
    {
        try
        {
            var result = await AreaArmazenagemBLL.GetByIdentificadorAsync(identificador);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("identificador/{identificador}/chamada")]
    public async Task<IActionResult> GetByIdentificadorToChamadaAsync(string identificador)
    {
        try
        {
            var result = await AreaArmazenagemBLL.GetByIdentificadorAsync(identificador);

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}/status")]
    public async Task<ActionResult> GetStatusCaracol(int id)
    {
        try
        {
            var response = await AreaArmazenagemBLL.GetStatusGaiolas(id);

            return OkResponse(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("status")]
    public ActionResult GetListaTiposStatusGaiolas()
    {
        try
        {
            var response = AreaArmazenagemBLL.GetTiposStatusGaiolas();

            return OkResponse(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("agrupador/{idAgrupador}")]
    public async Task<IActionResult> GetByAgrupadorAsync(int idAgrupador)
    {
        var result = await AreaArmazenagemBLL.GetByAgrupadorAsync(idAgrupador);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("posicao")]
    public async Task<IActionResult> GetByPosicaoAsync(string identificadorCaracol, int posicaoY)
    {
        var result = await AreaArmazenagemBLL.GetByPosicaoAsync(identificadorCaracol, posicaoY);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("caracol/{identificadorCaracol}")]
    public async Task<IActionResult> GetByIdentificadorCaracolAsync(string identificadorCaracol)
    {
        var result = await AreaArmazenagemBLL.GetByIdentificadorCaracolAsync(identificadorCaracol);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("set-status")]
    public async Task<IActionResult> SetStatusAsync(long id, [FromBody] StatusAreaArmazenagem status)
    {
        var result = await AreaArmazenagemBLL.SetStatusAsync(id, status);
        if (result == 0)
        {
            return BadRequest("Status não pôde ser alterado.");
        }

        return Ok("Status alterado com sucesso.");
    }

    [HttpGet("stagein/{idEndereco}")]
    public async Task<IActionResult> GetStageInLivreAsync(int idEndereco)
    {
        var result = await AreaArmazenagemBLL.GetStageInLivreAsync(idEndereco);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("liberar/agrupador/{idAgrupador}/requisição/{id_requisicao}")]
    public async Task<IActionResult> LiberarAreaArmazenagemAsync(Guid idAgrupador, Guid? id_requisicao)
    {
        try
        {
            var result = await AreaArmazenagemBLL.LiberarAreaArmazenagem(idAgrupador, id_requisicao);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }



    }

}
