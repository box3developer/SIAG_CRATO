using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Setor;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers
{
[Route("api/[controller]")]
[ApiController]
    public class SetorController : ControllerBase
{
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await SetorBLL.GetById(id);

            return Ok(result);
        }
    }
}
