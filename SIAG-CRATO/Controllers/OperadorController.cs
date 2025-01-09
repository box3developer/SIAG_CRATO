using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Operador;
using SIAG_CRATO.DTOs.Operador;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperadorController : ControllerBase
{
    [HttpGet("{cracha}")]
    public async Task<ActionResult<OperadorDTO>> GetByCracha(string cracha)
    {
        var operador = await OperadorBLL.GetByCrachaAsync(cracha);
        if (operador == null)
        {
            return NotFound();
        }
        return Ok(operador);
    }

    [HttpGet("nfc/{nfc}")]
    public async Task<ActionResult<OperadorDTO>> GetByNFC(string nfc)
    {
        var operador = await OperadorBLL.GetByNFCAsync(nfc);
        if (operador == null)
        {
            return NotFound();
        }
        return Ok(operador);
    }

    [HttpGet("meta")]
    public async Task<ActionResult<int>> GetMeta()
    {
        var meta = await OperadorBLL.GetMetaAsync();
        return Ok(meta);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] OperadorLoginDTO login)
    {
        var sucesso = await OperadorBLL.Login(login.IdOperador, login.IdEquipamento);
        if (sucesso)
        {
            return Ok("Login realizado com sucesso.");
        }
        else
        {
            return Unauthorized("Credenciais inválidas ou usuário não autorizado.");
        }
    }

    [HttpPost("logoff")]
    public async Task<IActionResult> LogOff([FromBody] OperadorLoginDTO login)
    {
        var sucesso = await OperadorBLL.LogOff(login.IdOperador, login.IdEquipamento);
        if (sucesso)
        {
            return Ok("Logout realizado com sucesso.");
        }
        else
        {
            return BadRequest("Erro ao realizar o logout.");
        }
    }
}
