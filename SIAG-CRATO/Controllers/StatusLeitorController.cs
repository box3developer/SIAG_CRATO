using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.StatusLeitor;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusLeitorController : ControllerBase
    {
        [HttpGet("{idLeitor}/equipamento/{idEquipamento}")]
        public async Task<IActionResult> GetStatusByEquipamento(int idLeitor, int idEquipamento)
        {
            var statusLeitor = await StatusLeitorBLL.GetStatusByEquipamentoAsync(idLeitor, idEquipamento);
            return Ok(statusLeitor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatusLeitor(StatusLeitorModel statusLeitor)
        {
            var result = await StatusLeitorBLL.CreateStatusLeitorAsync(statusLeitor);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatusLeitor(StatusLeitorModel statusLeitor)
        {
            var result = await StatusLeitorBLL.UpdateStatusLeitorAsync(statusLeitor);
            return Ok(result);
        }
    }
}
