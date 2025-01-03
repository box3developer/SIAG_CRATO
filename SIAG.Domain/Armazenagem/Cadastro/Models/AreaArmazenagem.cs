using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class AreaArmazenagem
    {
        public int AreaArmazenagemId { get; set; }

        public int TipoAreaId { get; set; }
        public TipoArea TipoArea { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        public int AgrupadorId { get; set; }
        public AgrupadorAtivo Agrupador { get; set; }

        public int NrPosicaoX { get; set; }

        public int NrPosicaoY { get; set; }

        public int NrLado { get; set; }

        public int FgStatus { get; set; }

        public string CdIdentificacao { get; set; }

        public int AgrupadorReservadoId { get; set; }
        public AgrupadorAtivo AgrupadorReservado { get; set; }
    }
}
