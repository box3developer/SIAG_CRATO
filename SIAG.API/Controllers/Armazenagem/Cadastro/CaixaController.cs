using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixaController : BaseController<
            IBaseService<IBaseRepository<Caixa, string>, Caixa, string, CaixaDTO>,
            IBaseRepository<Caixa, string>,
            Caixa,
            string,
            CaixaDTO
        >
    {
        public CaixaController(IBaseService<IBaseRepository<Caixa, string>, Caixa, string, CaixaDTO> service) : base(service)
        {
        }
    }
}
