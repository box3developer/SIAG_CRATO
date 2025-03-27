using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaArmazenagemController : BaseController<
            IBaseService<IBaseRepository<AreaArmazenagem, long>, AreaArmazenagem, long, AreaArmazenagemDTO>,
            IBaseRepository<AreaArmazenagem, long>,
            AreaArmazenagem,
            long,
            AreaArmazenagemDTO
        >
    {
        public AreaArmazenagemController(IBaseService<IBaseRepository<AreaArmazenagem, long>, AreaArmazenagem, long, AreaArmazenagemDTO> service) : base(service)
        {
        }
    }
}
