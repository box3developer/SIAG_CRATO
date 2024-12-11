using SIAG_CRATO.Data;
using SIAG_CRATO.Models;

namespace SIAG_CRATO.Models
{
    public class AgrupadorAtivoModel
    {
        public Guid Codigo { get; set; }
        public TipoAgrupamento TipoAgrupamento { get; set; }
        public string Codigo1 { get; set; }
        public string Codigo2 { get; set; }
        public string Codigo3 { get; set; }
        public Int64 Sequencia { get; set; }
        public DateTime DataAgrupador { get; set; }
        public AreaArmazenagem AreaArmazenagem { get; set; }
        public StatusAgrupador Status { get; set; }
    }
}
