using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Log;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogController : ControllerBase
{
    [HttpPost("caracol")]
    public async Task<IActionResult> CreateLogCaracol(LogModel log)
    {
        var sucesso = await LogBLL.CreateLogCaracol(log);
        if (sucesso)
        {
            return Ok("Log criado com sucesso.");
        }

        return BadRequest("Erro ao criar o log.");
    }

    [HttpPost("siag")]
    public async Task<IActionResult> CreateLogSIAG(string mensagem)
    {
        var sucesso = await LogBLL.CreateLogSIAG(mensagem);
        if (sucesso)
        {
            return Ok("Log criado com sucesso.");
        }

        return BadRequest("Erro ao criar o log.");
    }
}
