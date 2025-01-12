using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : BaseController<
            IBaseService<IBaseRepository<Deposito, int>, Deposito, int, DepositoDTO>,
            IBaseRepository<Deposito, int>,
            Deposito,
            int,
            DepositoDTO
        >
    {
        public DepositoController(IBaseService<IBaseRepository<Deposito, int>, Deposito, int, DepositoDTO> service) : base(service)
        {
        }
    }
}
