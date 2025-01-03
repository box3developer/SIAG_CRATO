using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Parametro;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParametroController : ControllerBase
{
    [HttpGet("{parametro}")]
    public async Task<IActionResult> GetByValor(string parametro)
    {
        var parametroModel = await ParametroBLL.GetParametroByParametro(parametro);
        if (parametroModel == null)
        {
            return NotFound();
        }
        return Ok(parametroModel);
    }

    // Obtém parâmetros por tipo
    [HttpGet("tipo/{tipo}")]
    public async Task<IActionResult> GetByTipo(string tipo)
    {
        var parametros = await ParametroBLL.GetParametroByTipo(tipo);
        return Ok(parametros);
    }
}
