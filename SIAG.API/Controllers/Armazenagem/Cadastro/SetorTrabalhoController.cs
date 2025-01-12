using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetorTrabalhoController : BaseController<
            IBaseService<IBaseRepository<SetorTrabalho, int>, SetorTrabalho, int, SetorTrabalhoDTO>,
            IBaseRepository<SetorTrabalho, int>,
            SetorTrabalho,
            int,
            SetorTrabalhoDTO
        >
    {
        public SetorTrabalhoController(IBaseService<IBaseRepository<SetorTrabalho, int>, SetorTrabalho, int, SetorTrabalhoDTO> service) : base(service)
        {
        }
    }
}
