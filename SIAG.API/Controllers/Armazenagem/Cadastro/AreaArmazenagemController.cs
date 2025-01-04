using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Logging;
using SIAG.CrossCutting.Utils;
using SIAG.Infrastructure.Configuracao;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaArmazenagemController : BaseController<AreaArmazenagemService, AreaArmazenagemDTO, int>
    {
        public AreaArmazenagemController(AreaArmazenagemService service) : base(service)
        {
        }
    }
}
