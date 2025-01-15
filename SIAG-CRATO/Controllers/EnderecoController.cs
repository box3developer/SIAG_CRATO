using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Endereco;
using SIAG_CRATO.DTOs.Endereco;
using SIAG_CRATO.Models;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnderecoController : ControllerCustom
{
    [HttpGet]
    public async Task<ActionResult<List<EnderecoDTO>>> GetListAsync()
    {
        var enderecos = await EnderecoBLL.GetListAsync();
        return Ok(enderecos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EnderecoDTO>> GetByIdAsync(int id)
    {
        try
        {
            var endereco = await EnderecoBLL.GetByIdAsync(id);

            return OkResponse(endereco);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("setor-status")]
    public async Task<ActionResult<List<EnderecoModel>>> GetBySetorStatus(int idSetorTrabalho, int fgStatus)
    {
        var enderecos = await EnderecoBLL.GetBySetorStatus(idSetorTrabalho, fgStatus);
        return Ok(enderecos);
    }
}
