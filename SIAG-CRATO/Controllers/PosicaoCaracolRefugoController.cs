using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.PosicaoCaracolRefugo;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PosicaoCaracolRefugoController : ControllerBase
{
    [HttpGet("posicao/{posicao}")]
    public async Task<IActionResult> GetByPosicao(int posicao)
    {
        var result = await PosicaoCaracolRefugoBLL.GetByPosicao(posicao);

        return Ok(result);
    }

    [HttpGet("tipo-fabrica")]
    public async Task<IActionResult> GetByTipo(string tipo, string? fabrica)
    {
        var response = await PosicaoCaracolRefugoBLL.GetByTipo(tipo, fabrica);

        return Ok(response);
    }
}
