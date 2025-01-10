using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Application.Armazenagem.Core.Services;

namespace SIAG.API.Controllers.Armazenagem.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetorTrabalhoController : BaseController<SetorTrabalhoService, SetorTrabalhoDTO, int>
    {
        public SetorTrabalhoController(SetorTrabalhoService service) : base(service)
        {
        }

        [HttpGet("select")]
        public ActionResult<List<StatusDTO>> GetStatus()
        {
            try
            {

                var response = await _service.;

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
    }
}
