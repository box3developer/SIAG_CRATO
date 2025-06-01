using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.AtividadeTarefa;
using SIAG_CRATO.BLLs.Chamada;
using SIAG_CRATO.BLLs.ChamadaTarefa;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Chamada;
using SIAG_CRATO.Repositories.Interfaces;
using SIAG_CRATO.Services;
using SIAG_CRATO.Util;

namespace SIAG_CRATO.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChamadaController : ControllerCustom
{
    private readonly IChamadaRepository _chamadaRepository;

    public ChamadaController(IChamadaRepository chamadaRepository)
    {
        _chamadaRepository = chamadaRepository;
    }

    [HttpPost]
    public async Task<ActionResult> CriarChamada([FromBody] ChamadaInsertDTO chamada)
    {
        try
        {
            var codigoChamada = await ChamadaBLL.SetChamadaAsync(chamada);
            var tarefas = await AtividadeTarefaBLL.GetByAtividadeAsync(chamada.IdAtividade);

            foreach (var tarefa in tarefas.OrderBy(x => x.CdSequencia))
            {
                await ChamadaTarefaBLL.SetTarefaAsync(chamada.IdChamada, tarefa.IdAtividade);
            }

            await ChamadaBLL.SetStatusAsync(chamada.IdChamada, StatusChamada.Aguardando);

            return Ok(chamada.IdChamada);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("listar")]
    public async Task<ActionResult> ListaChamadas([FromBody] ChamadaFiltroDTO filtro)
    {
        try
        {
            var lista = await ChamadaBLL.GetListAsync(filtro);
            return Ok(lista);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("atualizar-leitura")]
    public async Task<ActionResult> AtualizarLeituraChamada([FromBody] ChamadaLeituraDTO leitura)
    {
        try
        {
            await ChamadaBLL.AtualizarLeitura(leitura.IdChamada, leitura.IdAreaArmazenagem, leitura.IdPallet);
            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetChamadaById(Guid id)
    {
        try
        {
            var chamada = await ChamadaBLL.GetByIdAsync(id);
            return OkResponse(chamada);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/reiniciar")]
    public async Task<ActionResult> ReiniciarChamada(Guid id)
    {
        try
        {
            await ChamadaBLL.ReiniciarChamadaAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/rejeitar")]
    public async Task<ActionResult> RejeitarChamada(Guid id)
    {
        try
        {
            var chamada = await ChamadaBLL.GetByIdAsync(id);

            if (chamada == null)
                throw new ValidacaoException("Tarefa não encontrada");

            if (chamada.FgStatus >= StatusChamada.Rejeitado)
                throw new ValidacaoException("Tarefa já rejeitada");

            await ChamadaBLL.RejeitarChamadaAsync(chamada.IdChamada, chamada.IdAtividadeRejeicao);

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/finalizar")]
    public async Task<ActionResult> FinalizarChamada(Guid id)
    {
        try
        {
            var chamada = await ChamadaBLL.GetByIdAsync(id);

            if (chamada == null || chamada.FgStatus >= StatusChamada.Rejeitado)
            {
                return BadRequest("Chamada não encontrada!");
            }

            await ChamadaBLL.FinalizarChamadaAsync(chamada.IdChamada);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/status/{status}")]
    public async Task<ActionResult> RejeitarChamada(Guid id, StatusChamada status)
    {
        try
        {
            var chamada = await ChamadaBLL.GetByIdAsync(id);

            if (chamada == null || chamada.FgStatus >= StatusChamada.Rejeitado)
            {
                return BadRequest("Chamada não encontrada!");
            }

            await ChamadaBLL.SetStatusAsync(chamada.IdChamada, status);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("atribuir")]
    public async Task<ActionResult> AtribuirChamada([FromBody] ChamadaDTO chamada)
    {
        try
        {
            await ChamadaBLL.AtribuirChamadaAsync(chamada);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("selecionar")]
    public async Task<ActionResult> SelecionarChamada([FromBody] ChamadaSelecionarDTO selecao)
    {
        try
        {
            var id = await ChamadaBLL.Selecionar(selecao);

            return OkResponse(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("valida-leitura")]
    public async Task<ActionResult> ValidaLeituraChamada([FromBody] ValidaLeituraChamadaDTO filtro)
    {
        try
        {
            var result = await ChamadaBLL.ValidarLeituraChamada(filtro);

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("criar")]
    public async Task<ActionResult> CriarChamadaAsync([FromBody] CriarChamadaDTO dto)
    {
        try
        {
            var result = await _chamadaRepository.CriarChamadaAsync(dto);

            return OkResponse(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
