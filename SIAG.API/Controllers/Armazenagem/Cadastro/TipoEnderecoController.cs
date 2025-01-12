using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEnderecoController : BaseController<
            IBaseService<IBaseRepository<TipoEndereco, int>, TipoEndereco, int, TipoEnderecoDTO>,
            IBaseRepository<TipoEndereco, int>,
            TipoEndereco,
            int,
            TipoEnderecoDTO
        >
    {
        public TipoEnderecoController(IBaseService<IBaseRepository<TipoEndereco, int>, TipoEndereco, int, TipoEnderecoDTO> service) : base(service)
        {
        }
    }
}
