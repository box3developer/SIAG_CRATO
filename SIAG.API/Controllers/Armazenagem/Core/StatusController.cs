using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Implementations;
using SIAG.CrossCutting.DTOs;

namespace SIAG.API.Controllers.Armazenagem.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : BaseController<PedidoService, PedidoDTO, int>
    {
        public StatusController(PedidoService service) : base(service)
        {
        }

        [HttpGet("{nome}")]
        public ActionResult<List<StatusDTO>> Get(string nome)
        {
            try
            {
                var statusList = StatusDynamicService.ObterStatusPorNome(nome);
                return Ok(statusList);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Mensagem = ex.Message });
            }
        }
    }
}
