using Microsoft.AspNetCore.Mvc;
using SIAG.API.Utils;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TService, TDto, TKey> : ControllerBase
        where TService : class
        where TDto : class
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }

        [NonAction]
        protected ActionResult HandleException(Exception ex)
        {
            var result = new APIResultDTO
            {
                Sucesso = false,
                Dados = null,
                Mensagem = ex.Message
            };

            if (ex is ValidacaoException)
            {
                result.Tipo = "warning";
                return BadRequest(result);
            }
            else
            {
                result.Tipo = "error";
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        [NonAction]
        protected ActionResult OkResponse(object dados, string mensagem = "")
        {
            var result = new APIResultDTO
            {
                Sucesso = true,
                Dados = dados,
                Mensagem = mensagem,
                Tipo = "success"
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetItem(TKey id)
        {
            try
            {
                dynamic service = _service;
                var response = await service.GetItemAsync(id);

                return OkResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("Listagem")]
        public virtual async Task<ActionResult> GetList([FromBody] FiltroPaginacaoDTO dto)
        {
            try
            {
                dynamic service = _service;
                var response = await service.GetListAsync(dto);

                return OkResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(TKey id)
        {
            try
            {
                dynamic service = _service;
                var response = await service.DeleteAsync(id);

                return OkResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create([FromBody] TDto dto)
        {
            try
            {
                dynamic service = _service;
                var response = await service.CreateAsync(dto);

                return OkResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        public virtual async Task<ActionResult> Update([FromBody] TDto dto)
        {
            try
            {
                dynamic service = _service;
                var response = await service.UpdateAsync(dto);

                return OkResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("Select")]
        public virtual async Task<ActionResult> GetSelect([FromBody] FiltroPaginacaoDTO dto)
        {
            try
            {
                dynamic service = _service;
                var response = await service.GetSelectAsync(dto);

                return OkResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
