using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaixaLeituraController : BaseController<
            IBaseService<IBaseRepository<CaixaLeitura, long>, CaixaLeitura, long, CaixaLeituraDTO>,
            IBaseRepository<CaixaLeitura, long>,
            CaixaLeitura,
            long,
            CaixaLeituraDTO
        >
    {
        public CaixaLeituraController(IBaseService<IBaseRepository<CaixaLeitura, long>, CaixaLeitura, long, CaixaLeituraDTO> service) : base(service)
        {
        }
    }
}
