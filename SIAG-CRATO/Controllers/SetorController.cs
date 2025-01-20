using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Setor;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SetorController : ControllerCustom
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await SetorBLL.GetById(id);

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("Select")]
    public async Task<ActionResult> GetListSelect()
    {
        try
        {
            var response = await SetorBLL.GetListSelectsAsync();

            return OkResponse(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
