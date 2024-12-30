using Microsoft.AspNetCore.Mvc;
using SIAG_CRATO.BLLs.Atividade;
using SIAG_CRATO.BLLs.AtividadeTarefa;
using SIAG_CRATO.BLLs.Chamada;
using SIAG_CRATO.BLLs.ChamadaTarefa;
using SIAG_CRATO.BLLs.Equipamento;
using SIAG_CRATO.BLLs.EquipamentoEndereco;
using SIAG_CRATO.Data;
using SIAG_CRATO.DTOs.Chamada;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChamadaController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CriarChamada([FromBody] ChamadaInsertDTO chamada)
    {
        try
        {
            var codigoChamada = await ChamadaBLL.SetChamadaAsync(chamada);
            var tarefas = await AtividadeTarefaBLL.GetByAtividadeAsync(chamada.AtividadeId);

            foreach (var tarefa in tarefas.OrderBy(x => x.Sequencia))
            {
                await ChamadaTarefaBLL.SetTarefaAsync(chamada.Codigo, tarefa.AtividadeId);
            }

            await ChamadaBLL.SetStatusAsync(chamada.Codigo, StatusChamada.Aguardando);

            return Ok(chamada.Codigo);
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

            if (chamada == null || chamada.Status >= StatusChamada.Rejeitado)
            {
                return BadRequest("Chamada não encontrada!");
            }

            await ChamadaBLL.RejeitarChamadaAsync(chamada.Codigo, chamada.AtividadeRejeicaoId);

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
            var equipamento = await EquipamentoBLL.GetByIdAsync(selecao.EquipamentoId);

            if (equipamento == null)
            {
                return BadRequest("Equipamento não encontrado!");
            }

            var chamada = await ChamadaBLL.GetChamadaAbertaByOperadorAsync(selecao.OperadorId, selecao.EquipamentoId);

            if (chamada == null)
            {
                return BadRequest("Chamada não encontrada!");
            }

            var chamadasPendentes = await ChamadaBLL.GetChamadaDisponiveisAsync(equipamento.ModeloId, equipamento.SetorId ?? 0);

            var atividades = await AtividadeBLL.GetListAsync();

            atividades = atividades.Where(x => chamadasPendentes.Select(y => y.AtividadeId).Distinct().Contains(x.Codigo)).ToList();
            var equipamentosEndereco = new List<EquipamentoEnderecoModel>();

            if (atividades.Where(x => x.EvitarConflitoEndereco == ConflitoDeEnderecos.BloquearEndereco || x.EvitarConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco).Any())
            {
                var dataAtivo = DateTime.Now.AddMinutes(-120);
                var dataInativo = DateTime.Now.AddMinutes(-20);

                equipamentosEndereco = await EquipamentoEnderecoBLL.GetOutrosEquipamentosAtivosAsync(equipamento.Codigo, equipamento.ModeloId, equipamento.SetorId ?? 0, dataAtivo, dataInativo);

                chamadasPendentes = chamadasPendentes.Where(x => atividades.Where(y => y.EvitarConflitoEndereco == ConflitoDeEnderecos.RestringirPorZona ||
                                                                                       y.EvitarConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco)
                                                                           .Select(y => y.Codigo)
                                                                           .Distinct().Contains(x.AtividadeId) &&
                                                                 equipamentosEndereco.Select(y => y.EnderecoId).Distinct().Contains(x.AreaAmazenagemOrigemId))
                                                     .ToList();
            }

            if (atividades.Where(x => x.EvitarConflitoEndereco == ConflitoDeEnderecos.RestringirPorZona || x.EvitarConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco).Any())
            {
                equipamentosEndereco = await EquipamentoEnderecoBLL.GetByEquipamentoAsync(equipamento.Codigo);

                chamadasPendentes = chamadasPendentes.Where(x => atividades.Where(y => y.EvitarConflitoEndereco == ConflitoDeEnderecos.RestringirPorZona ||
                                                                                       y.EvitarConflitoEndereco == ConflitoDeEnderecos.RestringirPorZonaEEndereco)
                                                                           .Select(y => y.Codigo)
                                                                           .Distinct().Contains(x.AtividadeId) &&
                                                                 equipamentosEndereco.Select(y => y.EnderecoId).Distinct().Contains(x.AreaAmazenagemOrigemId))
                                                     .ToList();
            }

            var equipamentosPrioridade = (await EquipamentoEnderecoPrioridadeBLL.GetListAsync()).Where(x => equipamentosEndereco.Select(y => y.EquipamentoEnderecoId).Distinct().Contains(x.EnderecoId));
            var chamadasComPrioridade = new List<ChamadaDisponivelDTO>();

            if (equipamentosPrioridade.Any())
            {
                chamadasComPrioridade = chamadasPendentes.Where(x => equipamentosPrioridade.Select(y => y.Prioridade.ToString())
                                                                                               .Distinct()
                                                                                               .Contains(x.AreaAmazenagemOrigemId.ToString().Substring(4, 3)))
                                                             .ToList();
                if (chamadasComPrioridade.Count != 0)
                {
                    chamadasPendentes = chamadasComPrioridade;
                }
            }

            chamadasComPrioridade = chamadasPendentes.Where(x => x.Priorizar).ToList();

            if (chamadasComPrioridade.Count != 0)
            {
                chamadasPendentes = chamadasComPrioridade;
            }

            var chamadasParadas = chamadasPendentes.Where(x => !x.Processando).ToList();

            foreach (var chamadaParada in chamadasParadas)
            {
                var prioridade = 0;
                /**
                 * Chamadar procedure que calcula prioridade e armazena valor na variavel prioridade;
                 **/

                foreach (var chamadaUpdate in chamadasPendentes.Where(x => x.ChamadaId == chamadaParada.ChamadaId).ToList())
                {
                    chamadaUpdate.Processando = true;
                    chamadaUpdate.QuatidadePrioridade = prioridade;
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
