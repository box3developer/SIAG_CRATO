using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadeController : BaseController<
            IBaseService<IBaseRepository<Atividade, int>, Atividade, int, AtividadeDTO>,
            IBaseRepository<Atividade, int>,
            Atividade,
            int,
            AtividadeDTO
        >
    {
        public AtividadeController(IBaseService<IBaseRepository<Atividade, int>, Atividade, int, AtividadeDTO> service) : base(service)
        {
        }
    }
}
