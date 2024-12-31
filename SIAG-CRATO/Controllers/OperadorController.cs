using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Operador;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadorController : ControllerBase
    {
        [HttpGet("{cracha}")]
        public async Task<ActionResult<OperadorModel>> GetByCracha(string cracha)
        {
            var operador = await OperadorBLL.GetByCrachaAsync(cracha);
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
        public async Task<IActionResult> Login(int idOperador, int idEquipamento)
        {
            var sucesso = await OperadorBLL.Login(idOperador, idEquipamento);
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
        public async Task<IActionResult> LogOff(int idOperador, int idEquipamento)
        {
            var sucesso = await OperadorBLL.LogOff(idOperador, idEquipamento);
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
}
