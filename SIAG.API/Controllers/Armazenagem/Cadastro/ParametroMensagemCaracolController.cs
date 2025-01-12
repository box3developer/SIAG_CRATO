using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroMensagemCaracolController : BaseController<
            IBaseService<IBaseRepository<ParametroMensagemCaracol, int>, ParametroMensagemCaracol, int, ParametroMensagemCaracolDTO>,
            IBaseRepository<ParametroMensagemCaracol, int>,
            ParametroMensagemCaracol,
            int,
            ParametroMensagemCaracolDTO
        >
    {
        public ParametroMensagemCaracolController(IBaseService<IBaseRepository<ParametroMensagemCaracol, int>, ParametroMensagemCaracol, int, ParametroMensagemCaracolDTO> service) : base(service)
        {
        }
    }
}
