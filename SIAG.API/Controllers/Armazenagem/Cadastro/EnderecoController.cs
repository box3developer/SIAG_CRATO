using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : BaseController<
            IBaseService<IBaseRepository<Endereco, int>, Endereco, int, EnderecoDTO>,
            IBaseRepository<Endereco, int>,
            Endereco,
            int,
            EnderecoDTO
        >
    {
        public EnderecoController(IBaseService<IBaseRepository<Endereco, int>, Endereco, int, EnderecoDTO> service) : base(service)
        {
        }
    }
}
