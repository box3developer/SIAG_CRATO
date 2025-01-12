using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadaController : BaseController<
            IBaseService<IBaseRepository<Chamada, Guid>, Chamada, Guid, ChamadaDTO>,
            IBaseRepository<Chamada, Guid>,
            Chamada,
            Guid,
            ChamadaDTO
        >
    {
        public ChamadaController(IBaseService<IBaseRepository<Chamada, Guid>, Chamada, Guid, ChamadaDTO> service) : base(service)
        {
        }
    }
}
