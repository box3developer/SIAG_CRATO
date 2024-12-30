using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Caixa;
using SIAG_CRATO.DTOs.Caixa;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CaixaController : ControllerCustom
{
    [HttpPost("pedido")]
    public async Task<ActionResult> GetListaCaixasPedidos([FromBody] FiltroCaixaPedidoDTO filtro)
    {
        try
        {
            var response = await CaixaBLL.GetCaixasPedidos(filtro.IdPallet);

            return OkResponse(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
