using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusLeitorController : BaseController<
            IBaseService<IBaseRepository<StatusLeitor, int>, StatusLeitor, int, StatusLeitorDTO>,
            IBaseRepository<StatusLeitor, int>,
            StatusLeitor,
            int,
            StatusLeitorDTO
        >
    {
        public StatusLeitorController(IBaseService<IBaseRepository<StatusLeitor, int>, StatusLeitor, int, StatusLeitorDTO> service) : base(service)
        {
        }
    }
}
