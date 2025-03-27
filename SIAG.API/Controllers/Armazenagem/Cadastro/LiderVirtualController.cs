using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiderVirtualController : BaseController<
            IBaseService<IBaseRepository<LiderVirtual, long>, LiderVirtual, long, LiderVirtualDTO>,
            IBaseRepository<LiderVirtual, long>,
            LiderVirtual,
            long,
            LiderVirtualDTO
        >
    {
        public LiderVirtualController(IBaseService<IBaseRepository<LiderVirtual, long>, LiderVirtual, long, LiderVirtualDTO> service) : base(service)
        {
        }
    }
}
