using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAG.Application.Armazenagem.Core.DTOs
{
    public class AreaArmazenagemDTO
    {
        public int? AreaArmazenagemId { get; set; }
        public int? TipoAreaId { get; set; }
        public int? EnderecoId { get; set; }
        public int? AgrupadorId { get; set; }
        public int? NrPosicaoX { get; set; }
        public int? NrPosicaoY { get; set; }
        public int? NrLado { get; set; }
        public int? FgStatus { get; set; }
        public string? CdIdentificacao { get; set; } = string.Empty;
        public int? AgrupadorReservadoId { get; set; }
    }
}
