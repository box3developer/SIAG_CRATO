using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.EquipamentoChecklist;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoChecklistController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetListAsync()
        {
            var checklists = await EquipamentoChecklistBLL.GetListAsync();
            return Ok(checklists);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoChecklistModel>> GetByIdAsync(int id)
        {
            var checklist = await EquipamentoChecklistBLL.GetByIdAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }
            return Ok(checklist);
        }

        [HttpGet("modelo/{modeloId}")]
        public async Task<ActionResult> GetByModeloAsync(int modeloId)
        {
            var checklists = await EquipamentoChecklistBLL.GetByModeloAsync(modeloId);
            return Ok(checklists);
        }


        [HttpGet("identificador/{identificador}")]
        public async Task<ActionResult> GetChecklistEquipamentoByIdentificador(string identificador)
        {
            var checklists = await EquipamentoChecklistBLL.GetChecklistEquipamentoByIdentificador(identificador);
            return Ok(checklists);
        }
    }
}
