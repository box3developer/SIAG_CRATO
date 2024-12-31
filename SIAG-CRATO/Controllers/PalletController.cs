using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Pallet;
using SIAG_CRATO.Data;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalletController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(PalletModel pallet)
        {
            await PalletBLL.InsertAsync(pallet);
            return CreatedAtAction("GetById", new { id = pallet.Codigo }, pallet);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pallets = await PalletBLL.GetListAsync();
            return Ok(pallets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pallet = await PalletBLL.GetByIdAsync(id);
            if (pallet == null)
                return NotFound();

            return Ok(pallet);
        }

        [HttpGet("identificador/{identificador}")]
        public async Task<IActionResult> GetByIdentificador(string identificador)
        {
            var pallet = await PalletBLL.GetByIdentificadorAsync(identificador);
            if (pallet == null)
                return NotFound();
            
            return Ok(pallet);
        }

        [HttpGet("area/{areaArmazenagem}")]
        public async Task<IActionResult> GetByAreaArmazenagem(string areaArmazenagem)
        {
            var pallets = await PalletBLL.GetByAreaArmazenagemAsync(areaArmazenagem);
            return Ok(pallets);
        }

        [HttpGet("{id}/quantidade-caixas")]
        public async Task<IActionResult> GetQuantidadeCaixas(int id)
        {
            var quantidade = await PalletBLL.GetQuantidadeCaixasAsync(id);
            return Ok(quantidade);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatus(int id, StatusPallet status)
        {
            var result = await PalletBLL.SeStatusAsync(id, status);
            if (result > 0)
                return Ok(result);

            return BadRequest("Erro ao atualizar base de dados");
        }

        [HttpPut("vincular-agrupador")]
        public async Task<IActionResult> VincularAgrupadorReservado(AreaArmazenagemModel areaAtual)
        {
            var sucesso = await PalletBLL.VincularAgrupadorAreaReservadaAsync(areaAtual.IdentificadorCaracol, areaAtual);
            if (sucesso)
                return Ok("Pallet vinculado com sucesso.");
          
            return BadRequest("Erro ao vincular o pallet.");
          
        }

        [HttpPost("vincular-por-prioridade")]
        public async Task<IActionResult> VincularNovoPalletPorPrioridade(string identificadorCaracol, AreaArmazenagemModel areaAtual, int nivelAgrupador)
        {
            var sucesso = await PalletBLL.VincularNovoPalletPorPrioridadeAsync(identificadorCaracol, areaAtual, nivelAgrupador);
            if (sucesso)
                return Ok("Pallet vinculado com sucesso.");
         
   
            return BadRequest("Erro ao vincular o pallet.");
            
        }

        [HttpPost("vincular-disponivel")]
        public async Task<IActionResult> VincularNovoPalletDisponivel([FromBody] AreaArmazenagemModel areaAtual)
        {
            var sucesso = await PalletBLL.VincularNovoPalletDisponivelAsync(areaAtual.IdentificadorCaracol, areaAtual);
            if (sucesso)
            {
                return Ok("Pallet vinculado com sucesso.");
            }
            else
            {
                return BadRequest("Erro ao vincular o pallet.");
            }
        }

        [HttpPost("vincular-reservado")]
        public async Task<IActionResult> VincularNovoPalletReservado(string identificadorCaracol, AreaArmazenagemModel areaAtual)
        {
            var sucesso = await PalletBLL.VincularNovoPalletReservadoAsync(identificadorCaracol, areaAtual);
            if (sucesso)
            {
                return Ok("Pallet vinculado com sucesso.");
            }
            else
            {
                return BadRequest("Erro ao vincular o pallet.");
            }
        }

        [HttpGet("endereco/{idEndereco}/pallet/{idPallet}")]
        public async Task<IActionResult> GetPalletsByEndereco(int idEndereco, int idPallet)
        {
            var pallets = await PalletBLL.GetQtyPallets(idEndereco, idPallet);
            return Ok(pallets);
        }


    }
}
