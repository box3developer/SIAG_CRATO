using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetorTrabalhoController : BaseController<SetorTrabalhoService, SetorTrabalhoDTO, int>
    {
        public SetorTrabalhoController(SetorTrabalhoService service) : base(service)
        {
        }
    }
}
