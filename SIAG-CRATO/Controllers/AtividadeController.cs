using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Atividade;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAtividadeByIdAsync(int id)
        {
            var result = await AtividadeBLL.GetAtividadeByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetAtividadeByNomeAsync(string nome)
        {
            var result = await AtividadeBLL.GetAtividadeByNomeAsync(nome);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("equipamento-setor")]
        public async Task<IActionResult> GetAtividadesByEquipModeloSetorAsync(int id_equipamentomodelo, int id_setortrabalho)
        {
            var result = await AtividadeBLL.GetByEquipModeloSetor(id_equipamentomodelo, id_setortrabalho);
            if (result == null || result.Count == 0) return NotFound();
            return Ok(result);
        }
    }
}

