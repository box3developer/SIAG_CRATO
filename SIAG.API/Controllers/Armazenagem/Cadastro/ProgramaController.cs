using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramaController : BaseController<
            IBaseService<IBaseRepository<Programa, int>, Programa, int, ProgramaDTO>,
            IBaseRepository<Programa, int>,
            Programa,
            int,
            ProgramaDTO
        >
    {
        public ProgramaController(IBaseService<IBaseRepository<Programa, int>, Programa, int, ProgramaDTO> service) : base(service)
        {
        }
    }
}
