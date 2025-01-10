using Microsoft.AspNetCore.Mvc;
using SIAG.API.Utils;
using SIAG.CrossCutting.Utils;

namespace SIAG.API.Controllers.Armazenagem.Core
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
    }
}
