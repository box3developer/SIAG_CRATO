using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AtividadeRotina;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AtividadeRotinaController : ControllerCustom
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await AtividadeRotinaBLL.GetById(id);

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
