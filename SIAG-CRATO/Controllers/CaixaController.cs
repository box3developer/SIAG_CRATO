using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Caixa;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixaController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var caixa = await CaixaBLL.GetByIdAsync(id);
            return caixa == null ? NotFound() : Ok(caixa);
        }

        [HttpGet("pallet/{idPallet}")]
        public async Task<IActionResult> GetByPalletAsync(int idPallet)
        {
            var caixas = await CaixaBLL.GetByPalletAsync(idPallet);
            return Ok(caixas);
        }

        [HttpGet("quantidade-pallet/{idPallet}")]
        public async Task<IActionResult> GetQuantidadeByPalletAsync(int idPallet)
        {
            var caixas = await CaixaBLL.GetQuantidadeByPalletAsync(idPallet);
            return Ok(caixas);
        }

        [HttpGet("quantidade-pendentes/{idAgrupador}")]
        public async Task<IActionResult> GetQuantidadePendentesAsync(int idAgrupador)
        {
            var caixas = await CaixaBLL.GetQuantidadePendentesAsync(idAgrupador);
            return Ok(caixas);
        }

        [HttpGet("fabricante-caixa/{idCaixa}")]
        public async Task<IActionResult> GetFabricaAsync(string idCaixa)
        {
            var caixas = await CaixaBLL.GetFabricaAsync(idCaixa);
            return Ok(caixas);
        }

        [HttpGet("quantidade-pedido")]
        public async Task<IActionResult> GetFabricaAsync(int idPedido, long codigoPedido, long idPallet)
        {
            var caixas = await CaixaBLL.GetQuantidadeByPedido(idPedido, codigoPedido, idPallet);
            return Ok(caixas);
        }

        [HttpGet("caixa-pedido/{idPallet}")]
        public async Task<IActionResult> GetCaixasPedidos(long idPallet)
        {
            var caixas = await CaixaBLL.GetCaixasPedidos(idPallet);
            return Ok(caixas);
        }

        [HttpGet("pendentes")]
        public async Task<IActionResult> GetPendentesAsync()
        {
            var pendentes = await CaixaBLL.GetPendentesAsync();
            return Ok(pendentes);
        }

        [HttpGet("pendentes-lider")]
        public async Task<IActionResult> GetPendentesByLiderAsync()
        {
            var pendentes = await CaixaBLL.GetPendentesByLiderAsync();
            return Ok(pendentes);
        }
    }
}
