
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.EquipamentoCheckListOperador;
using SIAG_CRATO.DTOs.EquipamentoCheckListOperador;


namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipamentoChecklistOperadorController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> SetChecklistOperador(EquipamentoCheckListOperadorDTO checklistOperador)
    {
        var result = await EquipamentoCheckListOperadorBLL.SetChecklistOperador(checklistOperador);
        if (result)
        {
            return Ok(result);
        }

        return BadRequest("Não foi possível adicionar registro a base de dados.(CheckListOperador)");
    }
}
