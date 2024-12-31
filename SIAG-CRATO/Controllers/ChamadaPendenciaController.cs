using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.ChamadaPendencia;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadaPendenciaController : ControllerBase
    {

        [HttpPut("{idChamada}/pai/{idChamadaPai}")]
        public async Task<IActionResult> SetChamadaPai(Guid idChamada, Guid idChamadaPai)
        {
            var sucesso = await ChamadaPendenciaBLL.SetChamadaPai(idChamada, idChamadaPai);
            return sucesso ? Ok() : BadRequest("Não foi possível estabelecer a relação pai-filho");
        }

        [HttpPut("{idChamada}/origem/{idChamadaOrigem}")]
        public async Task<IActionResult> SetChamadaOrigem(Guid idChamada, Guid idChamadaOrigem)
        {
            var sucesso = await ChamadaPendenciaBLL.SetChamadaOrigem(idChamada, idChamadaOrigem);
            return sucesso ? Ok() : BadRequest("Não foi possível estabelecer a relação de origem");
        }
    }
}
