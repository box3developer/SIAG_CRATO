using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AtividadeRejeicao;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeRejeicaoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromBody] AtividadeRejeicaoModel? atividadeRejeicao)
        {
            try
            {
                var result = await AtividadeRejeicaoBLL.GetListAsync(atividadeRejeicao);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
