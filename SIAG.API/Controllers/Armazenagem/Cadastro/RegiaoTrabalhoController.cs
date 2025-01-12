using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegiaoTrabalhoController : BaseController<
            IBaseService<IBaseRepository<RegiaoTrabalho, int>, RegiaoTrabalho, int, RegiaoTrabalhoDTO>,
            IBaseRepository<RegiaoTrabalho, int>,
            RegiaoTrabalho,
            int,
            RegiaoTrabalhoDTO
        >
    {
        public RegiaoTrabalhoController(IBaseService<IBaseRepository<RegiaoTrabalho, int>, RegiaoTrabalho, int, RegiaoTrabalhoDTO> service) : base(service)
        {
        }
    }
}
