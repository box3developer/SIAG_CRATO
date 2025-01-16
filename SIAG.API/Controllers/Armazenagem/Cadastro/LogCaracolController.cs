using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

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
