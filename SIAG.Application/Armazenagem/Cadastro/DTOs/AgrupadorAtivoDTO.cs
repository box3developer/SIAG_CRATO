using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class AgrupadorAtivoDTO
    {
        public Guid AgrupadorId { get; set; }

        public int TpAgrupamento { get; set; }

        public string Codigo1 { get; set; }

        public string Codigo2 { get; set; }

        public string Codigo3 { get; set; }

        public int CdSequencia { get; set; }

        public DateTime DtAgrupador { get; set; }

        public int AreaArmazenagemId { get; set; }

        public int FgStatus { get; set; }
    }
}
