using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadorController : BaseController<
            IBaseService<IBaseRepository<Operador, long>, Operador, long, OperadorDTO>,
            IBaseRepository<Operador, long>,
            Operador,
            long,
            OperadorDTO
        >
    {
        public OperadorController(IBaseService<IBaseRepository<Operador, long>, Operador, long, OperadorDTO> service) : base(service)
        {
        }
    }
}
