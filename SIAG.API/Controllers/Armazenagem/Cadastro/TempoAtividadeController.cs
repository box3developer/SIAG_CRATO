using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempoAtividadeController : BaseController<
            IBaseService<IBaseRepository<TempoAtividade, int>, TempoAtividade, int, TempoAtividadeDTO>,
            IBaseRepository<TempoAtividade, int>,
            TempoAtividade,
            int,
            TempoAtividadeDTO
        >
    {
        public TempoAtividadeController(IBaseService<IBaseRepository<TempoAtividade, int>, TempoAtividade, int, TempoAtividadeDTO> service) : base(service)
        {
        }
    }
}
