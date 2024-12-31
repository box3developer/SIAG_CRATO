using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.EquipamentoManutencao;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoManutencaoController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var manutencoes = await EquipamentoManutencaoBLL.GetListAsync();
            return Ok(manutencoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var manutencao = await EquipamentoManutencaoBLL.GetByIdAsync(id);
            if (manutencao == null)
                return NotFound();
        
            return Ok(manutencao);
        }
    }
}
