using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoController : BaseController<
            IBaseService<IBaseRepository<Equipamento, int>, Equipamento, int, EquipamentoDTO>,
            IBaseRepository<Equipamento, int>,
            Equipamento,
            int,
            EquipamentoDTO
        >
    {
        public EquipamentoController(IBaseService<IBaseRepository<Equipamento, int>, Equipamento, int, EquipamentoDTO> service) : base(service)
        {
        }
    }
}
