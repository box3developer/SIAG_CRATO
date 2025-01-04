using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEnderecoController : BaseController<TipoEnderecoService, TipoEnderecoDTO, int>
    {
        public TipoEnderecoController(TipoEnderecoService service) : base(service)
        {
        }
    }
}
