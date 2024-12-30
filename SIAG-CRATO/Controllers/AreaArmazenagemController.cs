using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AreaArmzenagem;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AreaArmazenagemController : ControllerCustom
{
    [HttpGet("{id}/Status")]
    public async Task<ActionResult> GetStatusCaracol(int id)
    {
        try
        {
            var response = await AreaArmazenagemBLL.GetStatusGaiolas(id);

            return OkResponse(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("Status")]
    public ActionResult GetListaTiposStatusGaiolas()
    {
        try
        {
            var response = AreaArmazenagemBLL.GetTiposStatusGaiolas();

            return OkResponse(response);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}
