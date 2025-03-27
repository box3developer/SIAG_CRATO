using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Application.Armazenagem.Core.DTOs;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Core.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalletController : BaseController<
            IBaseService<IBaseRepository<Pallet, int>, Pallet, int, PalletDTO>,
            IBaseRepository<Pallet, int>,
            Pallet,
            int,
            PalletDTO
        >
    {
        public PalletController(IBaseService<IBaseRepository<Pallet, int>, Pallet, int, PalletDTO> service) : base(service)
        {
        }
    }
}
