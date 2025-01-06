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
    
    [HttpPost]
    public async Task<IActionResult> CreateCaixaLeitura(CaixaLeituraDTO caixaLeiura)
    {
        var result = await CaixaLeituraBLL.CreateCaixaLeitura(caixaLeiura);
        return result ? BadRequest("Não foi possivel criar CaixaLeitura") : Ok(result);
    }

    [HttpGet("/ultima-leitura")]
    public async Task<ActionResult<CaixaLeituraDTO>> GetUltimaLeituraByIdStatusTypeAsync(int idEquipamento, int fgStatus, int fgTipo)
    {
        var result = await CaixaLeituraBLL.GetUltimaLeituraByIdStatusTypeAsync(idEquipamento, fgStatus,fgTipo);
        return result == null ? NotFound() : Ok(result);
    }
}