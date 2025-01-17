using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Operador;
using SIAG_CRATO.DTOs.Operador;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperadorController : ControllerCustom
{
    [HttpGet("{cracha}")]
    public async Task<ActionResult<OperadorDTO>> GetByCracha(string cracha)
    {
        try
        {
            var operador = await OperadorBLL.GetByCrachaAsync(cracha);

            return OkResponse(operador);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("nfc/{nfc}")]
    public async Task<ActionResult<OperadorDTO>> GetByNFC(string nfc)
    {
        try
        {
            var operador = await OperadorBLL.GetByNFCAsync(nfc);

            return OkResponse(operador);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet("meta")]
    public async Task<ActionResult<int>> GetMeta()
    {
        try
        {
            var meta = await OperadorBLL.GetMetaAsync();

            return Ok(meta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/performance")]
    public async Task<IActionResult> GetPerformance(long id)
    {
        try
        {
            var performance = await OperadorBLL.GetPerformance(id);

            return OkResponse(performance);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] OperadorLoginDTO login)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("logoff")]
    public async Task<IActionResult> LogOff([FromBody] OperadorLoginDTO login)
    {
        try
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
