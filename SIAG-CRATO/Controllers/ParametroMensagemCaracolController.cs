using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.ParametroMensagemCaracol;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroMensagemCaracolController : ControllerBase
    {
        [HttpGet("{descricao}")]
        public async Task<IActionResult> GetByDescricao(string descricao)
        {
            var result = await ParametroMensagemCaracolBLL.GetByDescricao(descricao);

            return Ok(result);
        }

    }
}
