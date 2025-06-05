using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.DTOs.Luzes;
using SIAG_CRATO.Integration;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LuzesController : ControllerCustom
{
    [HttpGet("sincronizar")]
    public async Task<IActionResult> Sincronizar()
    {
        try
        {
            var result = await NodeRedIntegration.Sincronizar();

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("luzVerde")]
    public async Task<IActionResult> GetLuzesVerdes()
    {
        try
        {
            var result = await NodeRedIntegration.GetAllLuzesVerdes();

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("luzVermelha")]
    public async Task<IActionResult> GetLuzesVermelhas()
    {
        try
        {
            var result = await NodeRedIntegration.GetAllLuzesVermelhas();

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("luzVerde/ligar")]
    public async Task<IActionResult> PostLuzVerdeLigar([FromBody] LuzesFiltroDTO filtro)
    {
        try
        {
            var result = await NodeRedIntegration.AcenderLuzVerde(filtro.Caracol, filtro.Gaiola);

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("luzVermelha/ligar")]
    public async Task<IActionResult> PostLuzVermelhaLigar([FromBody] LuzesFiltroDTO filtro)
    {
        try
        {
            var result = await NodeRedIntegration.AcenderLuzVermelha(filtro.Caracol, filtro.Gaiola);

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("luzVermelha/desligar")]
    public async Task<IActionResult> PostLuzVermelhaDesligar([FromBody] LuzesFiltroDTO filtro)
    {
        try
        {
            var result = await NodeRedIntegration.ApagarLuzVermelha(filtro.Caracol, filtro.Gaiola);

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("luzAmarela/ligar")]
    public async Task<IActionResult> PostLuzAmarelaLigar([FromBody] LuzesFiltroDTO filtro)
    {
        try
        {
            var result = await NodeRedIntegration.AcenderLuzAmarela(filtro.Caracol, filtro.Gaiola);

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("luzAmarela/desligar")]
    public async Task<IActionResult> PostLuzAmarelaDesligar([FromBody] LuzesFiltroDTO filtro)
    {
        try
        {
            var result = await NodeRedIntegration.ApagarLuzAmarela(filtro.Caracol, filtro.Gaiola);

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }


    [HttpGet("luzVerde/desligar")]
    public async Task<IActionResult> GetLuzVerdeDesligar()
    {
        try
        {
            var result = await NodeRedIntegration.DesligarTodasLuzVerdes();

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("luzVermelha/ligar")]
    public async Task<IActionResult> GetLuzVermelhaLigar()
    {
        try
        {
            var result = await NodeRedIntegration.LigarTodasLuzVermelha();

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("luzVermelha/desligar")]
    public async Task<IActionResult> GetLuzVermelhaDesligar()
    {
        try
        {
            var result = await NodeRedIntegration.DesligarTodasLuzVermelha();

            return OkResponse(true);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
