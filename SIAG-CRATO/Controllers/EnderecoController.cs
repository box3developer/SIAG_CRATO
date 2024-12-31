using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Endereco;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<EnderecoModel>>> GetListAsync()
        {
            var enderecos = await EnderecoBLL.GetListAsync();
            return Ok(enderecos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoModel>> GetByIdAsync(int id)
        {
            var endereco = await EnderecoBLL.GetByIdAsync(id);
            return endereco == null ? NotFound() : Ok(endereco);
        }

        [HttpGet("setor-status")]
        public async Task<ActionResult<List<EnderecoModel>>> GetBySetorStatus(int idSetorTrabalho, int fgStatus)
        {
            var enderecos = await EnderecoBLL.GetBySetorStatus(idSetorTrabalho, fgStatus);
            return Ok(enderecos);
        }
    }
}
