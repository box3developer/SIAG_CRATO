using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : BaseController<
            IBaseService<IBaseRepository<Turno, int>, Turno, int, TurnoDTO>,
            IBaseRepository<Turno, int>,
            Turno,
            int,
            TurnoDTO
        >
    {
        public TurnoController(IBaseService<IBaseRepository<Turno, int>, Turno, int, TurnoDTO> service) : base(service)
        {
        }
    }
}
