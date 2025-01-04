using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixaController : BaseController<CaixaService, CaixaDTO, int>
    {
        public CaixaController(CaixaService service) : base(service)
        {
        }
    }
}
