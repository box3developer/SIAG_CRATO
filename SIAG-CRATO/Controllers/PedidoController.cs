using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Pedido;
using SIAG_CRATO.DTOs.Caixa;

namespace SIAG_CRATO.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PedidoController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> GetPedido([FromBody] FiltroCaixaPedidoDTO dto)
    {
        try
        {
            await PedidoBLL.GetByDTO(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);

        }
    }
}
