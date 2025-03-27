using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgrupadorAtivoController : BaseController<
            IBaseService<IBaseRepository<AgrupadorAtivo, Guid>, AgrupadorAtivo, Guid, AgrupadorAtivoDTO>,
            IBaseRepository<AgrupadorAtivo, Guid>,
            AgrupadorAtivo,
            Guid,
            AgrupadorAtivoDTO
        >
    {
        public AgrupadorAtivoController(IBaseService<IBaseRepository<AgrupadorAtivo, Guid>, AgrupadorAtivo, Guid, AgrupadorAtivoDTO> service) : base(service)
        {
        }
    }
}