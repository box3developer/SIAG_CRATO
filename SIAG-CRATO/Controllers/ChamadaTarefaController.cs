using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.ChamadaTarefa;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadaTarefaController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ChamadaTarefaModel>>> GetListAsync()
        {
            var chamadasTarefas = await ChamadaTarefaBLL.GetListAsync();
            return Ok(chamadasTarefas);
        }

        [HttpGet("{idChamada}/{idTarefa}")]
        public async Task<ActionResult<ChamadaTarefaModel>> GetByIdAsync(Guid idChamada, int idTarefa)
        {
            var chamadaTarefa = await ChamadaTarefaBLL.GetByIdAsync(idChamada, idTarefa);
            return chamadaTarefa == null ? NotFound() : Ok(chamadaTarefa);
        }

        [HttpPost]
        public async Task<IActionResult> SetTarefaAsync(ChamadaTarefaModel chamadaTarefa)
        {
            var sucesso = await ChamadaTarefaBLL.SetTarefaAsync(chamadaTarefa.IdChamada, chamadaTarefa.IdTarefa);
            return sucesso ? Ok() : BadRequest("Não foi possível associar a chamada à tarefa");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTarefaAsync(ChamadaTarefaModel chamadaTarefa)
        {
            var sucesso = await ChamadaTarefaBLL.UpdateTarefaAsync(chamadaTarefa);
            return sucesso ? Ok() : BadRequest("Não foi possível atualizar a associação");
        }
    }
}
