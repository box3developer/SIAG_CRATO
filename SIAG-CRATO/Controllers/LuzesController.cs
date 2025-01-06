using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.DTOs.Luzes;
using SIAG_CRATO.Integration;

namespace SIAG_CRATO.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LuzesController : ControllerBase
{
    [HttpGet("sincronizar")]
    public async Task<IActionResult> Sincronizar()
    {
        try
        {
            var result = await NodeRedIntegration.Sincronizar();

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost("luzVerde/ligar")]
    public async Task<IActionResult> PostLuzVerdeLigar([FromBody] LuzesFiltroDTO filtro)
    {
        try
        {
            var result = await NodeRedIntegration.AcenderLuzVerde(filtro.Caracol, filtro.Gaiola);

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost("luzVermelha/ligar")]
    public async Task<IActionResult> PostLuzVermelhaLigar([FromBody] LuzesFiltroDTO filtro)
    {
        try
        {
            var result = await NodeRedIntegration.AcenderLuzVermelha(filtro.Caracol, filtro.Gaiola);

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost("luzVermelha/desligar")]
    public async Task<IActionResult> PostLuzVermelhaDesligar([FromBody] LuzesFiltroDTO filtro)
    {
        try
        {
            var result = await NodeRedIntegration.ApagarLuzVermelha(filtro.Caracol, filtro.Gaiola);

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("luzVerde/desligar")]
    public async Task<IActionResult> GetLuzVerdeDesligar()
    {
        try
        {
            var result = await NodeRedIntegration.DesligarTodasLuzVerdes();

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("luzVermelha/ligar")]
    public async Task<IActionResult> GetLuzVermelhaLigar()
    {
        try
        {
            var result = await NodeRedIntegration.LigarTodasLuzVermelha();

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("luzVermelha/desligar")]
    public async Task<IActionResult> GetLuzVermelhaDesligar()
    {
        try
        {
            var result = await NodeRedIntegration.DesligarTodasLuzVermelha();

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
