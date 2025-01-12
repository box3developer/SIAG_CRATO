using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoModeloController : BaseController<
            IBaseService<IBaseRepository<EquipamentoModelo, int>, EquipamentoModelo, int, EquipamentoModeloDTO>,
            IBaseRepository<EquipamentoModelo, int>,
            EquipamentoModelo,
            int,
            EquipamentoModeloDTO
        >
    {
        public EquipamentoModeloController(IBaseService<IBaseRepository<EquipamentoModelo, int>, EquipamentoModelo, int, EquipamentoModeloDTO> service) : base(service)
        {
        }
    }
}
