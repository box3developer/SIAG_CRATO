using SIAG.CrossCutting.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.Application.Armazenagem.Cadastro.Services
{
    public class AreaArmazenagemService
    {
        private readonly ILogService _logService;

        public AreaArmazenagemService(ILogService logService)
        {
            _logService = logService;
        }

        public void ProcessarAreas()
        {
            _logService.LogDebug("Iniciando processamento de áreas...");

            try
            {
                // Simulação de lógica
                _logService.LogInfo("Processamento concluído com sucesso.");
            }
            catch (Exception ex)
            {
                _logService.LogError("Erro durante o processamento de áreas.", ex);
            }
        }
    }
}
