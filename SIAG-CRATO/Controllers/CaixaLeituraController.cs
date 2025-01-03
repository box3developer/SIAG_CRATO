using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.CaixaLeitura;
using SIAG_CRATO.DTOs.CaixaLeitura;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CaixaLeituraController : ControllerBase
{
    [HttpGet("{idCaixa}")]
    public async Task<ActionResult<CaixaLeituraDTO>> GetUltimaCaixaLida(string idCaixa)
    {
        var result = await CaixaLeituraBLL.GetUltimaCaixaLida(idCaixa);
        return result == null ? NotFound() : Ok(result);
    }
}