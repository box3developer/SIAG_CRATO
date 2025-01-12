using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesempenhoController : BaseController<
            IBaseService<IBaseRepository<Desempenho, long>, Desempenho, long, DesempenhoDTO>,
            IBaseRepository<Desempenho, long>,
            Desempenho,
            long,
            DesempenhoDTO
        >
    {
        public DesempenhoController(IBaseService<IBaseRepository<Desempenho, long>, Desempenho, long, DesempenhoDTO> service) : base(service)
        {
        }
    }
}
