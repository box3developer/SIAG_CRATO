using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.Domain.Armazenagem.Core.Models;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Application.Armazenagem.Core.Services;

namespace SIAG.API.Controllers.Armazenagem.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaArmazenagemController : BaseController<AreaArmazenagemService, AreaArmazenagemDTO, int>
    {
        public AreaArmazenagemController(AreaArmazenagemService service) : base(service)
        {
        }

        [HttpGet("status")]
        public ActionResult<List<StatusDTO>> GetStatus()
        {
            try
            {
                var response = StatusUtils.GetStatusList<StatusAreaArmazenagem>();
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
    }
}
