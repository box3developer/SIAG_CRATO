using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIAG.Domain.Armazenagem.Cadastro.Models
{
    public class AreaArmazenagem
    {
        [Key]
        [Column("id_areaarmazenagem")]
        public int AreaArmazenagemId { get; set; }

        [ForeignKey("TipoArea")]
        [Column("id_tipoarea")]
        public int TipoAreaId { get; set; }
        public TipoArea TipoArea { get; set; }

        [ForeignKey("Endereco")]
        [Column("id_endereco")]
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

        [ForeignKey("AgrupadorAtivo")]
        [Column("id_agrupador")]
        public int AgrupadorId { get; set; }
        public AgrupadorAtivo Agrupador { get; set; }

        [Column("nr_posicaox")]
        public int NrPosicaoX { get; set; }

        [Column("nr_posicaoy")]
        public int NrPosicaoY { get; set; }

        [Column("nr_lado")]
        public int NrLado { get; set; }

        [Column("fg_status")]
        public int FgStatus { get; set; }

        [Column("cd_identificacao")]
        public string CdIdentificacao { get; set; }

        [ForeignKey("AgrupadorAtivo")]
        [Column("id_agrupador_reservado")]
        public int AgrupadorReservadoId { get; set; }
        public AgrupadorAtivo AgrupadorReservado { get; set; }
    }
}
