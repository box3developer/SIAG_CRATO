using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogCaracolController : BaseController<
            IBaseService<IBaseRepository<LogCaracol, long>, LogCaracol, long, LogCaracolDTO>,
            IBaseRepository<LogCaracol, long>,
            LogCaracol,
            long,
            LogCaracolDTO
        >
    {
        public LogCaracolController(IBaseService<IBaseRepository<LogCaracol, long>, LogCaracol, long, LogCaracolDTO> service) : base(service)
        {
        }
    }
}
