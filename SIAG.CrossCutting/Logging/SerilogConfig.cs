using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.CrossCutting.Logging
{
    public static class SerilogConfig
    {
        public static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Define o nível mínimo de log
                .WriteTo.Console()    // Envia logs para o console
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Gera logs diários em arquivos
                .CreateLogger();
        }
    }
}
