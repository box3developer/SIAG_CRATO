using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AgrupadorAtivo;
using SIAG_CRATO.BLLs.AreaArmzenagem;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgrupadorAtivoController : ControllerBase
    {
        [HttpGet("status/{idAgrupador}")]
        public async Task<IActionResult> GetByAgrupadorAsync(Guid idAgrupador)
        {
            try
            {
                var result = await AgrupadorAtivoBLL.GetAgrupadorStatus(idAgrupador);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        [HttpPut("finaliza")]
        public async Task<IActionResult> FinalizaAgrupador(Guid idAgrupador, Guid idRequisicao)
        {
            try
            {
                var result = await AgrupadorAtivoBLL.FinalizaAgrupador(idAgrupador,idRequisicao);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        [HttpPut("libera")]
        public async Task<IActionResult> LiberaAgrupador(Guid idAgrupador, Guid idRequisicao)
        {
            try
            {
                var result = await AgrupadorAtivoBLL.LiberarAgrupador(idAgrupador, idRequisicao);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
    }
}
