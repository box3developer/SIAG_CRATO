using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Caixa;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<CaixaModel>> GetByIdAsync(string id)
        {
            var caixa = await CaixaBLL.GetByIdAsync(id);
            return caixa == null ? NotFound() : Ok(caixa);
        }

        [HttpGet("pallet/{idPallet}")]
        public async Task<ActionResult<List<CaixaModel>>> GetByPalletAsync(int idPallet)
        {
            var caixas = await CaixaBLL.GetByPalletAsync(idPallet);
            return Ok(caixas);
        }

        // ... outros endpoints similares para os demais métodos

        [HttpGet("pendentes")]
        public async Task<ActionResult<Dictionary<string, int>>> GetPendentesAsync()
        {
            var pendentes = await CaixaBLL.GetPendentesAsync();
            return Ok(pendentes);
        }

        [HttpGet("pendentes-lider")]
        public async Task<ActionResult<Dictionary<string, int>>> GetPendentesByLiderAsync()
        {
            var pendentes = await CaixaBLL.GetPendentesByLiderAsync();
            return Ok(pendentes);
        }
    }
}
