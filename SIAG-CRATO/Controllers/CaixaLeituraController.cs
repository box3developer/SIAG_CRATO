using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.CaixaLeitura;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixaLeituraController : ControllerBase
    {
        [HttpGet("{idCaixa}")]
        public async Task<ActionResult<CaixaLeituraModel>> GetUltimaCaixaLida(string idCaixa)
        {
            var result = await CaixaLeituraBLL.GetUltimaCaixaLida(idCaixa);
            return result == null ? NotFound() : Ok(result);
    }
}
