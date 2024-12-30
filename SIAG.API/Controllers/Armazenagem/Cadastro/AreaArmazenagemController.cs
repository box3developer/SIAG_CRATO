using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIAG.CrossCutting.Logging;

namespace SIAG.API.Controllers.Armazenagem.Cadastro
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaArmazenagemController : ControllerBase
    {
        private readonly ILogService _logService;

        public AreaArmazenagemController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public IActionResult ObterAreas()
        {
            _logService.LogInfo("Iniciando consulta de áreas de armazenagem.");

            try
            {
                // Simulação de operação
                var areas = new List<string> { "Área 1", "Área 2" };
                _logService.LogInfo($"Consulta finalizada com {areas.Count} áreas encontradas.");

                return Ok(areas);
            }
            catch (Exception ex)
            {
                _logService.LogError("Erro ao consultar áreas de armazenagem.", ex);
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
