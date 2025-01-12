using Microsoft.AspNetCore.Mvc;
using SIAG.Application.Armazenagem.Cadastro.DTOs;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;
using SIAG.Domain.Armazenagem.Cadastro.Models;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : BaseController<
            IBaseService<IBaseRepository<Pedido, int>, Pedido, int, PedidoDTO>,
            IBaseRepository<Pedido, int>,
            Pedido,
            int,
            PedidoDTO
        >
    {
        public PedidoController(IBaseService<IBaseRepository<Pedido, int>, Pedido, int, PedidoDTO> service) : base(service)
        {
        }
    }
}
