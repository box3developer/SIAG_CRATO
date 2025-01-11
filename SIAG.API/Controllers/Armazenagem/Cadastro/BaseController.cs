using Microsoft.AspNetCore.Mvc;
using SIAG.API.Utils;
using SIAG.Application.Armazenagem.Cadastro.Services.Interfaces;
using SIAG.CrossCutting.DTOs;
using SIAG.CrossCutting.Utils;
using SIAG.Domain.Armazenagem.Cadastro.Interfaces;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TService, TRepository, TEntity, TKey, TDto> : ControllerBase
        where TService : IBaseService<TRepository, TEntity, TKey, TDto>
        where TRepository : IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
        where TDto : class
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [NonAction]
        protected virtual ActionResult HandleException(Exception ex)
        {
            var result = new APIResultDTO
            {
                Sucesso = false,
                Dados = null,
                Mensagem = ex.Message,
                Tipo = ex is ValidacaoException ? "warning" : "error"
            };

            return ex is ValidacaoException
                ? BadRequest(result)
                : StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [NonAction]
        protected virtual ActionResult OkResponse(object dados, string mensagem = "")
        {
            return Ok(new APIResultDTO
            {
                Sucesso = true,
                Dados = dados,
                Mensagem = mensagem,
                Tipo = "success"
            });
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult> GetItem([FromRoute] TKey id)
        {
            try
            {
                var response = await _service.GetItemAsync(id);
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
                var response = await _service.GetListAsync(dto);
                return OkResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete([FromRoute] TKey id)
        {
            try
            {
                var response = await _service.DeleteAsync(id);
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
                var response = await _service.CreateAsync(dto);
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
                var response = await _service.UpdateAsync(dto);
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
                var response = await _service.GetSelectAsync(dto);
                return OkResponse(response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}