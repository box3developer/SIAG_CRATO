using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AtividadeRotina;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AtividadeRotinaController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await AtividadeRotinaBLL.GetById(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
