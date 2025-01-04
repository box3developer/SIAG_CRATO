using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class AreaArmazenagem
    {
        [Key]
        public int AreaArmazenagemId { get; set; }

        [ForeignKey("TipoAreaModel")]
        public int TipoAreaId { get; set; }
        public TipoArea TipoArea { get; set; }

        [ForeignKey("EnderecoModel")]
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        [ForeignKey("Agrupador")]
        [InverseProperty("AreaArmazenagemPrincipal")]
        public Guid AgrupadorId { get; set; }
        public AgrupadorAtivo Agrupador { get; set; }

        public int NrPosicaoX { get; set; }

        public int NrPosicaoY { get; set; }

        public int NrLado { get; set; }

        public int FgStatus { get; set; }

        public string CdIdentificacao { get; set; }

        [ForeignKey("AgrupadorReservado")]
        [InverseProperty("AreaArmazenagemReservada")]
        public Guid AgrupadorReservadoId { get; set; }
        public AgrupadorAtivo AgrupadorReservado { get; set; }
    }
}
