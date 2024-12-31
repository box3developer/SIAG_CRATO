using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Pedido;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await PedidoBLL.GetById(id);
            return Ok(result);
        }
    }
}
