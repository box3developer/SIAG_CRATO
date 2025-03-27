using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Application.Chamada.Cadastro.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Chamada.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadePrioridadeController : BaseController<
            IBaseService<IBaseRepository<AtividadePrioridade, int>, AtividadePrioridade, int, AtividadePrioridadeDTO>,
            IBaseRepository<AtividadePrioridade, int>,
            AtividadePrioridade,
            int,
            AtividadePrioridadeDTO
        >
    {
        public AtividadePrioridadeController(IBaseService<IBaseRepository<AtividadePrioridade, int>, AtividadePrioridade, int, AtividadePrioridadeDTO> service) : base(service)
        {
        }
    }
}
