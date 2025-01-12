using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametroController : BaseController<
            IBaseService<IBaseRepository<Parametro, int>, Parametro, int, ParametroDTO>,
            IBaseRepository<Parametro, int>,
            Parametro,
            int,
            ParametroDTO
        >
    {
        public ParametroController(IBaseService<IBaseRepository<Parametro, int>, Parametro, int, ParametroDTO> service) : base(service)
        {
        }
    }
}
