using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAreaController : BaseController<
            IBaseService<IBaseRepository<TipoArea, int>, TipoArea, int, TipoAreaDTO>,
            IBaseRepository<TipoArea, int>,
            TipoArea,
            int,
            TipoAreaDTO
        >
    {
        public TipoAreaController(IBaseService<IBaseRepository<TipoArea, int>, TipoArea, int, TipoAreaDTO> service) : base(service)
        {
        }
    }
}
