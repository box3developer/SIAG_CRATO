using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Turno;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TurnoController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await TurnoBLL.GetList();
        return Ok(result);
    }
}
