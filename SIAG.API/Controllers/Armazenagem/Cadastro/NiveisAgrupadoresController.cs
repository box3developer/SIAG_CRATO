using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiveisAgrupadoresController : BaseController<
            IBaseService<IBaseRepository<NiveisAgrupadores, long>, NiveisAgrupadores, long, NiveisAgrupadoresDTO>,
            IBaseRepository<NiveisAgrupadores, long>,
            NiveisAgrupadores,
            long,
            NiveisAgrupadoresDTO
        >
    {
        public NiveisAgrupadoresController(IBaseService<IBaseRepository<NiveisAgrupadores, long>, NiveisAgrupadores, long, NiveisAgrupadoresDTO> service) : base(service)
        {
        }
    }
}
