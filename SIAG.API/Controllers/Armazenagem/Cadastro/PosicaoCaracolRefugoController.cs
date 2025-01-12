using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosicaoCaracolRefugoController : BaseController<
            IBaseService<IBaseRepository<PosicaoCaracolRefugo, int>, PosicaoCaracolRefugo, int, PosicaoCaracolRefugoDTO>,
            IBaseRepository<PosicaoCaracolRefugo, int>,
            PosicaoCaracolRefugo,
            int,
            PosicaoCaracolRefugoDTO
        >
    {
        public PosicaoCaracolRefugoController(IBaseService<IBaseRepository<PosicaoCaracolRefugo, int>, PosicaoCaracolRefugo, int, PosicaoCaracolRefugoDTO> service) : base(service)
        {
        }
    }
}
