using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class AreaArmazenagemDTO
    {
        public int? IdAreaArmazenagem { get; set; }
        public int? IdTipoArea { get; set; }
        public int? IdEndereco { get; set; }
        public int? IdAgrupador { get; set; }
        public int? NrPosicaoX { get; set; }
        public int? NrPosicaoY { get; set; }
        public int? NrLado { get; set; }
        public int? FgStatus { get; set; }
        public string? CdIdentificacao { get; set; } = string.Empty;
        public int? IdAgrupadorReservado { get; set; }
    }
}
